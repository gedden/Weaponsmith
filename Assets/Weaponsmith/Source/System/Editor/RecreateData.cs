using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Tool to generate data files from the SQLite database
/// </summary>
public class RecreateData : EditorWindow
{
    public static string ReferenceFilePath  = "Assets/Weaponsmith/Source/System/Editor/Reference.txt";
    public static string GeneratedFilePath  = "Assets/Weaponsmith/Source/Data/Generated";

    // Member Variables
    List<string> CreatedFiles;

    /// <summary>
    /// Initialize the list
    /// </summary>
    public RecreateData()
    {
        CreatedFiles = new List<string>();
    }

    [MenuItem("Gedden/Regenerate")]
    // Use this for initialization
    public static void Regenerate()
    {
        RecreateData Instance = ScriptableObject.CreateInstance<RecreateData>();
        Instance.GenerateSource();
        Instance.ShowSummaryWindow();
    }

    [MenuItem("Gedden/Debug")]
    public static void ShowEnums()
    {
        foreach( var station in DatabaseUtil.QueryAll<WorkstationData>() )
        {
            Debug.Log(station.Arcana);
        }
        // WorkstationData New = new WorkstationData(1, EArcana.Divination, 100, 0, 1);

    }

    /// <summary>
    /// Reflectively get the enum type by name
    /// </summary>
    /// <param name="enumName"></param>
    /// <returns></returns>
    private static Type GetEnumType(string enumName)
    {
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            var type = assembly.GetType(enumName);
            if (type == null)
                continue;
            if (type.IsEnum)
                return type;
        }
        return null;
    }

    /// <summary>
    /// Main Entry for generateing source from the database
    /// </summary>
    private void GenerateSource()
    {
        // Open a connection
        DataTable Table = DatabaseUtil.GetDataTable("SELECT name FROM MAIN.[sqlite_master] WHERE type='table'");

        if (Table != null)
        {
            // Delete the existing files
            CleanGeneratedFiles();

            foreach (DataRow DataRow in Table.Rows)
            {
                var Name = DataRow["name"].ToString();

                // Dont process the system table
                if (Name.StartsWith("sqlite_"))
                    continue;

                GenerateSource(Name);
            }
        }
    }

    /// <summary>
    /// Generate a source file
    /// </summary>
    /// <param name="TableName"></param>
    private void GenerateSource(string TableName)
    {
        Debug.Log("Generating Source: " + TableName);
        DataTable Source = DatabaseUtil.GetDataTable("select * from " + TableName + " Limit 1");

        // Get the class name
        var ClassName = ToClassName(TableName);
        CreatedFiles.Add(TableName + " --> " + ClassName);

        // Get the content
        var Content = CreateFileText(ClassName, Source.Columns);

        // Write the File
        WriteSource(ClassName, Content);
    }

    /// <summary>
    /// Get the class name
    /// </summary>
    /// <param name="TableName"></param>
    /// <returns></returns>
    private static string ToClassName(string TableName)
    {
        return TableName + "Data";
    }

    /// <summary>
    /// Just create the text
    /// </summary>
    /// <param name="ClassName"></param>
    /// <param name="Collection"></param>
    /// <returns></returns>
    private static string CreateFileText(string ClassName, DataColumnCollection Collection)
    {
        // Create the param text
        string Params = "";
        string ConstructorParams = "";
        string ConstructorAssignments = "";
        bool IsPrimitive;
        foreach (DataColumn Col in Collection)
        {
            // Get the type name
            string TypeName = ToTypeName(Col, out IsPrimitive);

            // The ID will be inhereted
            if( !Col.ColumnName.Equals("Id") )
            {
                Params += "\tpublic " + TypeName + " " + Col.ColumnName + ";\r\n";
            }

            if (ConstructorParams.Length > 0)
                ConstructorParams += ", ";
            ConstructorParams += TypeName + " " + Col.ColumnName + "In";
            ConstructorAssignments += "\t\t" + Col.ColumnName + " = " +Col.ColumnName+ "In;\r\n";
            
        }
        
        // Update class name references
        string Content = ReferenceText.Replace("%ClassName%", ClassName);

        // Update content referneces
        Content = Content.Replace("%Params%", Params);
        Content = Content.Replace("%ConstructorParams%", ConstructorParams);
        Content = Content.Replace("%ConstructorAssignments%", ConstructorAssignments);
        // Content = Content.Replace("%ConstructorParams%", "");
        // Content = Content.Replace("%ConstructorAssignments%", "");

        // get the content in question
        return Content;
    }

    /// <summary>
    /// Get the type
    /// </summary>
    /// <param name="SystemType"></param>
    /// <returns></returns>
    private static string ToTypeName(DataColumn Column, out bool IsPrimitive)
    {
        // Get the column name
        var ColumnName = Column.ColumnName;
        Type EnumType = GetEnumType("E" + ColumnName);
        if( EnumType != null )
        {
            IsPrimitive = false;
            return "E" + ColumnName;
        }

        // Get the data type
        System.Type SystemType = Column.DataType;

        IsPrimitive = true;
        if (SystemType == typeof(long))
            return "long";

        if (SystemType == typeof(string))
            return "string";

        if (SystemType == typeof(decimal))
            return "decimal";

        IsPrimitive = false;
        return SystemType.ToString();
    }

    private static void WriteSource(string ClassName, string Content)
    {
        // Get the final path
        string FinalPath = GeneratedFilePath +"/"+ ClassName + ".cs";

        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(FinalPath, false);
        writer.WriteLine(Content);
        writer.Close();

        //Re-import the file to update the reference in the editor
        AssetDatabase.ImportAsset(FinalPath);
    }

    private static void CleanGeneratedFiles()
    {
        string[] paths = { GeneratedFilePath };
        foreach (string AssetPath in AssetDatabase.FindAssets("t:Script", paths) )
        {
            if( !AssetDatabase.DeleteAsset(AssetDatabase.GUIDToAssetPath(AssetPath)) )
                Debug.LogError("Unable to delete: " + AssetDatabase.GUIDToAssetPath(AssetPath));
            // Debug.Log(AssetDatabase.GUIDToAssetPath(AssetPath));
        }
    }

    private static string ReferenceText
    {
        get
        {
            //Read the text from directly from the test.txt file
            StreamReader reader = new StreamReader(ReferenceFilePath);
            string Content = reader.ReadToEnd();
            reader.Close();
            return Content;
        }
    }

    /// <summary>
    /// Show the summary window
    /// </summary>
    private void ShowSummaryWindow()
    {
        string Content = "";
        foreach (var Name in CreatedFiles)
        {
            Content += "\n+ " + Name;
        }

        EditorUtility.DisplayDialog(CreatedFiles.Count + "x Files Created", Content, GameUtil.GetRandomAffirmitive());
    }
}

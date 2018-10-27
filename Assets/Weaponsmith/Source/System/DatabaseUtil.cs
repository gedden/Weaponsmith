using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;
using System.Reflection;

public class DatabaseUtil
{
    public static String DatabaseName = "Weaponsmith/Weaponsmith.db";

    public static String URI
    {
        get
        {
            return "URI=file:" + Application.dataPath + "/" + DatabaseName;
        }
    }

    /// <summary>
    /// Accessor to get all the rows in a table
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static List<T> QueryAll<T>(string Additional = null) where T : GeneratedData
    {
        return InternalQuery<T>(null, Additional);
    }

    /// <summary>
    /// Get the specific row. Otherwise, nothing!
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="Id"></param>
    /// <returns></returns>
    public static T Query<T>(long Id, string Additional = null) where T : GeneratedData
    {
        var results = InternalQuery<T>(Id, Additional);

        if (results.Count > 0)
            return results[0];

        return default(T);
    }

    public static void Insert<T>(T Data) where T : GeneratedData
    {
        var ClassName = typeof(T).Name;
        var TableName = ClassName.Replace("Data", "");

        string Query = "INSERT INTO (" + TableName;
        string SetCommand = "";
        string ValueCommand = "";

        foreach (var Member in typeof(T).GetMembers())
        {
            // Only consider fields
            if (Member.MemberType != MemberTypes.Field)
                continue;

            if (SetCommand.Length > 0)
            {
                SetCommand += ", ";
                ValueCommand += ", ";
            }

            SetCommand += Member.Name;
            ValueCommand += Member.Name;
        }

        Query += "(" + SetCommand + " ) VALUES ( " + ValueCommand + ");";
        Debug.Log(Query);
    }

    /// <summary>
    /// Open connection to the database.
    /// </summary>
    /// <returns></returns>
    public static IDbConnection Open()
    {
        IDbConnection Connection = (IDbConnection)new SqliteConnection(URI);
        Connection.Open();

        return Connection;
    }

    /// <summary>
    /// Accessor to close a connection
    /// </summary>
    /// <param name="Connection"></param>
    public static void Close(IDbConnection Connection)
    {
        Connection.Close();
        Connection = null;
    }

    /// <summary>
    /// Internal global query code!
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id"></param>
    /// <returns></returns>
    private static List<T> InternalQuery<T>(long? id=null, string Additional=null) where T : GeneratedData
    {
        List<T> Results = new List<T>();

        try
        {
            var ClassName = typeof(T).Name;
            var TableName = ClassName.Replace("Data", "");

            string Query = "SELECT * FROM " + TableName;
            if( id != null )
            {
                Query += " WHERE Id = " + id;
            }

            if( Additional != null )
            {
                Query += " AND " + Additional;
            }

            SqliteConnection cnn = (SqliteConnection)Open();
            SqliteCommand mycommand = new SqliteCommand(cnn) { CommandText = Query };
            SqliteDataReader reader = mycommand.ExecuteReader();

            while (reader.Read())
            {
                // Create a new result
                T Result = (T)Activator.CreateInstance(typeof(T));

                foreach (var Member in typeof(T).GetMembers())
                {
                    // Only consider fields
                    if (Member.MemberType != MemberTypes.Field)
                        continue;


                    var Field = Result.GetType().GetField(Member.Name);

                    var value = reader[Member.Name];
                    if (value != null)
                    {
                        try
                        {
                            if(Field.FieldType.IsEnum)
                            {
                                Field.SetValue(Result, Enum.ToObject(Field.FieldType, (int)value));
                            }
                            else
                            {
                                Field.SetValue(Result, value);
                            }
                        }
                        catch( Exception e )
                        {
                            Debug.LogError(e);
                        }
                    }
                }
                Results.Add(Result);
            }

            Close(cnn);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
        }

        return Results;
    }

    /// <summary>
    /// Get a representation of a table
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public static DataTable GetDataTable(string sql)
    {
        DataTable dt = new DataTable();

        try
        {
            SqliteConnection cnn = (SqliteConnection)Open();
            SqliteCommand mycommand = new SqliteCommand(cnn) { CommandText = sql };
            SqliteDataReader reader = mycommand.ExecuteReader();

            dt.Load(reader);

            reader.Close();

            Close(cnn);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
            return null;
        }

        return dt;
    }


    // Use this for initialization
    void Do()
    {
        Debug.Log(URI);

        // Open a connection
        IDbConnection dbconn = Open();

        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT * FROM Employee";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            int value = reader.GetInt32(0);
            string name = reader.GetString(1);
            int rand = reader.GetInt32(2);

            Debug.Log("value= " + value + "  name =" + name + "  random =" + rand);
        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;

        // Close the connection
        Close(dbconn);
    }

	// Update is called once per frame
	void Update () {
		
	}
}

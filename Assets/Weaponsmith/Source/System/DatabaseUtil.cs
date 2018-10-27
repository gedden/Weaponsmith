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


    /// <summary>
    /// Insert the data into the database. Automatically updates the ID field with the new value
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="Data"></param>
    public static bool Update<T>(T Data) where T : GeneratedData
    {
        // UPDATE Workstation SET Arcana = 3 WHERE Id = 3

        var ClassName = typeof(T).Name;
        var TableName = ClassName.Replace("Data", "");

        string Query = "UPDATE " + TableName;
        string SetCommand = "";

        foreach (var Field in typeof(T).GetFields())
        {
            if (SetCommand.Length > 0)
            {
                SetCommand += ", ";
            }

            SetCommand += Field.Name + " = '";

            if (Field.FieldType.IsEnum)
            {
                var value = (int)Field.GetValue(Data);
                SetCommand += value;
            }
            else
            {
                SetCommand += Field.GetValue(Data);
            }
            SetCommand += "'";
        }

        // Finially, create the query
        Query += " SET " + SetCommand + " WHERE Id = " + Data.Id;

        // Now actually insert it!
        SqliteConnection cnn = (SqliteConnection)Open();
        SqliteCommand SqlCommand = new SqliteCommand(cnn) { CommandText = Query };
        int results = SqlCommand.ExecuteNonQuery();
        Close(cnn);
        
        return (results > 0);
    }

    /// <summary>
    /// Insert the data into the database. Automatically updates the ID field with the new value
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="Data"></param>
    public static void Insert<T>(T Data) where T : GeneratedData
    {
        // INSERT INTO Workstation (Arcana, Xp, Position, Active ) VALUES ( 1, 1000, 12, 1);

        var ClassName = typeof(T).Name;
        var TableName = ClassName.Replace("Data", "");

        string Query = "INSERT INTO " + TableName;
        string SetCommand = "";
        string ValueCommand = "";

        foreach (var Field in typeof(T).GetFields())
        {
            if (SetCommand.Length > 0)
            {
                SetCommand += ", ";
                ValueCommand += ", ";
            }

            SetCommand += Field.Name;

            if (Field.FieldType.IsEnum)
            {
                var value = (int)Field.GetValue(Data);
                ValueCommand += value;
            }
            else
            {
                ValueCommand += Field.GetValue(Data);
            }
        }

        // Finially, create the query
        Query += " (" + SetCommand + " ) VALUES ( " + ValueCommand + ");";


        // Now actually insert it!
        SqliteConnection cnn = (SqliteConnection)Open();
        SqliteCommand SqlCommand = new SqliteCommand(cnn) { CommandText = Query };
        int results = SqlCommand.ExecuteNonQuery();

        // Get the last row
        SqlCommand = new SqliteCommand(cnn) { CommandText = "SELECT last_insert_rowid()" };
        var lastId = SqlCommand.ExecuteScalar();

        if (lastId != null)
        {
            Data.Id = (long)lastId;
        }

        Close(cnn);
    }

    /// <summary>
    /// Delete from the database
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="Data"></param>
    /// <returns></returns>
    public static bool Delete<T>(T Data) where T : GeneratedData
    {
        return Delete<T>(Data.Id);
    }

    /// <summary>
    /// delete from the database by id
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="Id"></param>
    /// <returns></returns>
    public static bool Delete<T>(long Id) where T : GeneratedData
    {
        var ClassName = typeof(T).Name;
        var TableName = ClassName.Replace("Data", "");

        string Query = "DELETE FROM " + TableName + " WHERE Id = " + Id;

        // Now actually insert it!
        SqliteConnection cnn = (SqliteConnection)Open();
        SqliteCommand SqlCommand = new SqliteCommand(cnn) { CommandText = Query };
        int results = SqlCommand.ExecuteNonQuery();
        Close(cnn);

        return results > 0;
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
                                var valueSet = Enum.ToObject(Field.FieldType, value);

                                Field.SetValue(Result, valueSet);
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

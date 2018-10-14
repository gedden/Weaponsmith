using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;

public class Database : MonoBehaviour
{
    public String DatabaseName = "Weaponsmith.db";

    private String URI
    {
        get
        {
            return "URI=file:" + Application.dataPath + "/" + DatabaseName;
        }
    }

    public void Start()
    {
        // Do();
    }


    // Use this for initialization
    void Do()
    {
        /*
        Debug.Log(URI);
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(URI);
        dbconn.Open(); //Open connection to the database.
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
        dbconn.Close();
        dbconn = null;
        */
    }

	// Update is called once per frame
	void Update () {
		
	}
}

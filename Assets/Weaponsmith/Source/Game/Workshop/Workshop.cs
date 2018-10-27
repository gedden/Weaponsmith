using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workshop : MonoBehaviour
{
    [Header("Workshop Defines")]
    public List<SpawnPoint> WorkstationSpawns;
    public Workstation WorkstationTemplate;

    List<Workstation> Stations;
    List<Employee> Employees;

    /// <summary>
    /// Clear out the world
    /// </summary>
    public void Clear()
    {
        foreach( SpawnPoint Spawn in WorkstationSpawns )
        {
            GameUtil.DestroyChildren(Spawn.transform);
        }

        Stations = new List<Workstation>();
        Employees = new List<Employee>();
    }

    /// <summary>
    /// Add a workstation
    /// </summary>
    /// <param name="Station"></param>
    public void AddWorksation(Workstation Station)
    {
        if (Stations.Contains(Station))
            return;

        Stations.Add(Station);
    }

    /// <summary>
    /// Remove a workstation
    /// </summary>
    /// <param name="Station"></param>
    public void RemoveWorksation(Workstation Station)
    {
        if (!Stations.Contains(Station))
            return;

        Stations.Remove(Station);
    }

    /// <summary>
    /// Add an employee
    /// </summary>
    /// <param name="Employee"></param>
    public void AddEmployee(Employee Employee)
    {
        if (Employees.Contains(Employee))
            return;

        Employees.Add(Employee);
    }

    /// <summary>
    /// Remove an employee
    /// </summary>
    /// <param name="Employee"></param>
    public void RemoveEmployee(Employee Employee)
    {
        if (!Employees.Contains(Employee))
            return;

        Employees.Remove(Employee);
    }

    internal void Load(SessionData session)
    {
        // Clear out the world
        Clear();

        // Create new workstations
        foreach( var WorkstationData in session.Workstations)
        {
            // Get the next best spawnpoint
            var spawn = GetSpawnPoint((int)WorkstationData.Position);

            if( spawn == null )
            {
                Debug.Log("Cannot load a workstation with an invalid spawn point (" + WorkstationData.Position + ")");
                continue;
            }

            var instance = Instantiate<Workstation>(WorkstationTemplate, spawn.transform, false);
            Stations.Add(instance);
        }

        // Spawn all the employees
        foreach( var CharacterData in session.Characters )
        {
            // Create a new instance
            var spawn = Character.CreateInstance(CharacterData);

            // Just put them all at the first workstation for now
            spawn.transform.SetParent(Stations[0].transform, false);


            // Register them
        }
    }

    private SpawnPoint GetSpawnPoint(int Position)
    {
        foreach( SpawnPoint Point in WorkstationSpawns )
        {
            if (Point.Id == Position)
                return Point;
        }
        return null;
    }
}

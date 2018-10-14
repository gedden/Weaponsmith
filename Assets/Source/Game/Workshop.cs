using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workshop : MonoBehaviour
{
    List<Workstation> Stations;
    List<Employee> Employees;

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
}

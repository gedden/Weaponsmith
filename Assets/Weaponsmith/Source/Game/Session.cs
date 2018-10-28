using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Session
{
    Workshop Workshop;
    Project CurrentProject;

    // Property assignments
    public SessionData Data { get; set; }

    /// <summary>
    /// Transient data accessor
    /// </summary>
    public bool Transient { get { return Data.Transient; } }

    public EGamePhase Phase { get; private set; }

    /// <summary>
    /// Go to the next phase
    /// </summary>
    public void NextPhase()
    {
        switch (Phase)
        {
            case EGamePhase.StartOfDay:
                Phase = EGamePhase.Working;
                OnWorkDay();
                break;
            case EGamePhase.Working:
                Phase = EGamePhase.EndOfDay;
                OnEndOfDay();
                break;
            default:
            case EGamePhase.EndOfDay:
                Phase = EGamePhase.StartOfDay;
                OnStartOfDay();
                break;
        }
    }

    public void OnStartOfDay()
    {
        // Start a new project
        CurrentProject = new WeaponCraftingProject();
        CurrentProject.StartProject();
    }
    public void OnWorkDay() { }
    public void OnEndOfDay() { }


    /// <summary>
    /// Initialize this session from the data
    /// </summary>
    public void Initialize()
    {
        Phase = EGamePhase.None;
    }


    /// <summary>
    /// Clear out the session
    /// </summary>
    public void Clear()
    {
    }

    public void CreateFakeGame()
    {
        // Create new data
        Data = FakeFactory.CreateSession();
        Initialize();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// The idea behind the game instance is to be a universal, statically accessable monobehaviour. 
/// </summary>
[RequireComponent(typeof(ModalManager))]
class GameInstance : MonoBehaviour
{
    // Class properties
    public static GameInstance Instance { private set; get; }

    // Member Properties
    public MainMenu MainMenuPrefab;
    public Session Session = null;
    public Dock Dock;

    public Workshop Workshop;

    private ModalManager ModalManager;
    private bool Started = false;
    private InputController Input;
    private Workshop Shop;

    public void Start()
    {
        // Setup the singleton accessor
        Instance = this;

        // Test a dialog
        ModalManager = GetComponent<ModalManager>();
        Input = new InputController();


    }

    /// <summary>
    /// Called after start, when the game is first started. 
    /// </summary>
    public void OnFirstStart()
    {
        ShowMainMenu();
    }

    /// <summary>
    /// Show the main menu for the game
    /// </summary>
    public void ShowMainMenu()
    {
        ModalManager.CreatePopup(MainMenuPrefab, true);
    }

    public void Update()
    {
        if( !Started )
        {
            OnFirstStart();
            Started = true;
            return;
        }

        Input.CheckGlobalInput();
    }

    public void StartNewGame()
    {
        // Load Account Information

        // Create new game information
        Session = new Session();
        Session.CreateFakeGame();

        // Clear out the world
        Workshop.Clear();

        // Load the world
        Workshop.Load(Session.Data);

        // Start a new day
        Session.NextPhase();

        // Show the dock
        RefreshDock();
    }

    public void RefreshDock()
    {
        if( Session == null || Session.Phase == EGamePhase.None )
        {
            Dock.ShowDisplays(false);
            return;
        }

        // Update the dock
        Dock.ShowDisplays(true);
        Dock.SessionData = Session.Data;
    }

    public void Quit()
    {
        Application.Quit();
    }
}

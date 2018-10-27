using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class MainMenu : Popup
{
    public static MainMenu Instance { private set; get; }

    public void Start()
    {
        // Destroy the old main menu (if its there)
        if( Instance != null )
        {
            Instance.Cleanup();
        }

        Instance = this;
    }

    public override void Cleanup()
    {
        base.Cleanup();
        Instance = null;
    }


    public void OnRequestStartNewGame()
    {
        // Tell the game instance to create a new game
        var Instance = ModalManager.Instance.ShowDialog(EDialogInteractionType.OK_CANCEL, "For Real?", "You sure you want to start a new game? Your existing progress will be overwritten!");
        Instance.OkCallback = OnReallyStartNewGame;
    }
    public void OnRequestQuit()
    {
        // Tell the game instance to create a new game
        var Instance = ModalManager.Instance.ShowDialog(EDialogInteractionType.OK_CANCEL, "For Real?", "Are you sure you want to exit?");
        Instance.OkCallback = delegate ()
        {
            Cleanup();
            GameInstance.Instance.Quit();
        };
    }

    private void OnReallyStartNewGame()
    {
        // Ok, now REALLY just start a new game. 
        GameInstance.Instance.StartNewGame();
        Cleanup();
    }

    public void OnRequestContinueGame()
    {
        // Tell the game instance to create a new game

        // Finially, cleanup
        Cleanup();


    }
}

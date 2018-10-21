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
    ModalManager ModalManager;

    public void Start()
    {
        // Setup the singleton accessor
        Instance = this;

        // Test a dialog
        ModalManager = GetComponent<ModalManager>();
    }


    bool done = false;
    public void Update()
    {
        Debug.Log("Tick!" + done);
        if (!done)
        {
            done = true;
            var SystemPopup = ModalManager.ShowPopup(EDialogInteractionType.OK_CANCEL, null, null, true);

            SystemPopup.Title = "System Test";
            SystemPopup.Body = "Body Test";
            SystemPopup.Modal = true;
            

            ModalManager.ShowPopup(EDialogInteractionType.OK);
        }
    }
}

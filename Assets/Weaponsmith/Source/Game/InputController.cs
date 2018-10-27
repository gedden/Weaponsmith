using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class InputController
{
    public void CheckGlobalInput()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (MainMenu.Instance != null)
            {
                MainMenu.Instance.Cleanup();
            }
            else
            {
                // Show the main menu
                GameInstance.Instance.ShowMainMenu();
            }
        }
    }
}

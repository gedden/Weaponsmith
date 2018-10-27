using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    // Public Member Variables
    public Image ModalBackground;
    
    public bool Modal
    {
        set
        {
            if (ModalBackground != null)
            {
                ModalBackground.gameObject.SetActive(value);
            }
        }
    }

    /// <summary>
    /// Nuke this dialog
    /// </summary>
    public virtual void Cleanup()
    {
        // Nuke this dialog
        Destroy(gameObject);
    }
}

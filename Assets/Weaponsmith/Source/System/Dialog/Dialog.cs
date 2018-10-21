using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public enum EDialogInteractionType
{
    OK,
    OK_CANCEL,
    CANCEL,
}

public class Dialog : MonoBehaviour
{
    // Public Member Variables
    public Button OkButton;
    public Button CancelButton;
    public Text TitleText;
    public Text BodyText;
    public Image ModalBackground;

    // Callback Delegates
    public delegate void DialogDelegate();
    public DialogDelegate OkCallback;
    public DialogDelegate CancelCallback;

    // Propeties
    private EDialogInteractionType _DialogType;
    public EDialogInteractionType DialogType
    {
        set
        {
            _DialogType = value;
            switch (DialogType)
            {
                case EDialogInteractionType.OK:
                    OkButton.gameObject.SetActive(true);
                    CancelButton.gameObject.SetActive(false);
                    return;
                case EDialogInteractionType.OK_CANCEL:
                    OkButton.gameObject.SetActive(true);
                    CancelButton.gameObject.SetActive(true);
                    return;
                case EDialogInteractionType.CANCEL:
                    OkButton.gameObject.SetActive(true);
                    CancelButton.gameObject.SetActive(true);
                    return;
            }

        }
        get { return _DialogType; }
    }

    public string Title { set { TitleText.text = value; } }
    public string Body { set { BodyText.text = value; } }
    public bool Modal { set { ModalBackground.gameObject.SetActive(value); } }

    public void OnOk()
    {
        if(OkCallback != null)
            OkCallback();

        // Nuke this dialog
        Destroy(gameObject);
    }
    public void OnCancel()
    {
        if (CancelCallback != null)
            CancelCallback();

        // Nuke this dialog
        Destroy(gameObject);
    }
}

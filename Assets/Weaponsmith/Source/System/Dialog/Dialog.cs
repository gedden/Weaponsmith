using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public enum EDialogInteractionType
{
    NONE,
    OK,
    OK_CANCEL,
    CANCEL,
}

public class Dialog : Popup
{
    // Public Member Variables
    public Button OkButton;
    public Button CancelButton;
    public Text TitleText;
    public Text BodyText;

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

            if( OkButton != null )
            {
                // Get the text
                var OkText = OkButton.GetComponentInChildren<Text>();
                if( OkText != null )
                {
                    OkText.text = GameUtil.GetRandomAffirmitive();
                }
            }
        }
        get { return _DialogType; }
    }

    public string Title
    {
        set
        {
            if (TitleText != null)
            {
                TitleText.text = value;
            }
        }
    }
    public string Body
    {
        set
        {
            if (BodyText != null)
            {
                BodyText.text = value;
            }
        }
    }

    public void OnOk()
    {
        if(OkCallback != null)
            OkCallback();

        // Nuke this dialog
        Cleanup();
    }
    public void OnCancel()
    {
        if (CancelCallback != null)
            CancelCallback();
        Cleanup();
    }
}

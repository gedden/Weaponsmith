using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModalManager : MonoBehaviour
{
    // Member Variables
    public Dialog DialogPrefab;
    public Canvas GameCanvas;
    private Canvas SystemCanvas;

    // Class Members
    public static ModalManager Instance { private set; get; }

    /// <summary>
    /// Initialize the singleton
    /// </summary>
    public void Start()
    {
        Instance = this;

        // Add a system canvas
        var Master = new GameObject();
        Master.name = "System Canvas";
        Master.AddComponent<GraphicRaycaster>();
        SystemCanvas = Master.GetComponent<Canvas>();// Master.AddComponent<Canvas>();
        SystemCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        SystemCanvas.sortingOrder = 999;
    }


    /// <summary>
    /// Create a standard dialog box (title/message)
    /// </summary>
    /// <param name="DialogType"></param>
    /// <param name="Title"></param>
    /// <param name="Content"></param>
    /// <param name="OkCallback"></param>
    /// <param name="CancelCallback"></param>
    /// <param name="IsSystemDialog"></param>
    /// <returns></returns>
    public Dialog ShowDialog(EDialogInteractionType DialogType = EDialogInteractionType.NONE, string Title = null, string Content = null, Dialog.DialogDelegate OkCallback = null, Dialog.DialogDelegate CancelCallback = null, bool IsSystemDialog = false)
    {
        // Create the popup dialog
        var Instance = (Dialog)CreatePopup(DialogPrefab);


        if (Instance)
        {
            Instance.DialogType = DialogType;
            Instance.OkCallback = OkCallback;
            Instance.CancelCallback = CancelCallback;

            // Set the content
            Instance.Title = Title;
            Instance.Body = Content;
        }

        return Instance;
    }

    /// <summary>
    /// Create a popup of any popup type
    /// </summary>
    /// <param name="PopupPrefab"></param>
    /// <param name="IsModal"></param>
    /// <param name="IsSystemDialog"></param>
    /// <returns></returns>
    public Popup CreatePopup(Popup PopupPrefab, bool IsModal = false, bool IsSystemDialog = false)
    {
        var Instance = Instantiate<Popup>(PopupPrefab);

        if (Instance)
        {
            // Set the parent to be the main canvas
            if (IsSystemDialog)
            {
                Instance.transform.SetParent(SystemCanvas.gameObject.transform, false);
            }
            else
            {
                Instance.transform.SetParent(GameCanvas.gameObject.transform, false);
            }

            // Setup the modal settings
            Instance.Modal = IsModal;

            // Set the positioning
            RectTransform RectTransform = SystemCanvas.GetComponent<RectTransform>();
            Vector3 Centroid = new Vector3(RectTransform.rect.width / 2, RectTransform.rect.height / 2);
            Instance.transform.SetPositionAndRotation(Centroid, Quaternion.identity);
        }

        return Instance;
    }

}

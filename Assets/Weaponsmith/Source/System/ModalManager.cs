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
    /// Show a popup dialog
    /// </summary>
    /// <param name="DialogType"></param>
    /// <param name="OkCallback"></param>
    /// <param name="CancelCallback"></param>
    public Dialog ShowPopup(EDialogInteractionType DialogType, Dialog.DialogDelegate OkCallback = null, Dialog.DialogDelegate CancelCallback = null, bool IsSystemDialog = false)
    {
        var Instance = Instantiate<Dialog>(DialogPrefab);

        if (Instance)
        {
            Instance.DialogType = DialogType;
            Instance.OkCallback = OkCallback;
            Instance.CancelCallback = CancelCallback;

            // Set the parent to be the main canvas
            if(IsSystemDialog)
            {
                Instance.transform.SetParent(SystemCanvas.gameObject.transform, false);
            }
            else
            {
                Instance.transform.SetParent(GameCanvas.gameObject.transform, false);
            }
            RectTransform RectTransform = SystemCanvas.GetComponent<RectTransform>();
            Vector3 Centroid = new Vector3(RectTransform.rect.width / 2, RectTransform.rect.height / 2);
            Instance.transform.SetPositionAndRotation(Centroid, Quaternion.identity);
        }

        return Instance;
    }
}

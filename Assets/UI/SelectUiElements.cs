//Attach this script to your Canvas GameObject.
//Also attach a GraphicsRaycaster component to your canvas by clicking the Add Component button in the Inspector window.
//Also make sure you have an EventSystem in your hierarchy.

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using static UnityEngine.InputSystem.InputAction;

public interface IUiSelectable
{
    public void OnUiSelect(RaycastResult raycastResult);
}

public class SelectUiElements : MonoBehaviour
{
    [SerializeField] GraphicRaycaster _raycaster;
    [SerializeField] EventSystem _eventSystem;
    [SerializeField] ActionMapName _actionMapName;
    [SerializeField] ActionName _actionName;

    private void Awake()
    {
        CustomInputManager.SubscribeToAction(_actionMapName, _actionName, RaycastOntoCanvas);
    }

    public void OnBeforeNextSceneLoad()
    {
        CustomInputManager.UnsubscribeFromAction(_actionMapName, _actionName, RaycastOntoCanvas);
    }

    private void RaycastOntoCanvas(CallbackContext _)
    {
        // Set up the new Pointer Event
        PointerEventData pointerEventData = new PointerEventData(_eventSystem);
        pointerEventData.position = MouseUtils.GetMouseScreenPos();

        // Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        // Raycast using the Graphics Raycaster and mouse click position
        _raycaster.Raycast(pointerEventData, results);

        // For every result returned, output the name of the GameObject on the Canvas hit by the Ray
        foreach (RaycastResult result in results)
        {
            IUiSelectable selectable = result.gameObject.GetComponent<IUiSelectable>();
            if (selectable == null)
            {
                continue;
            }

            selectable.OnUiSelect(result);
        }
    }
}
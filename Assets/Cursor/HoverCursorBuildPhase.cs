using System;
using ReactiveUnity;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public interface IHoverable
{
    public void OnHoverEnter(RaycastHit hit);
    public void OnHoverExit(RaycastHit hit);
}

public interface ISelectable
{
    public void OnSelect(RaycastHit hit);
}

// TODO: jdsklfjsdf
// public interface ISelectable
// {
//     public void OnSelectionStart();
//     public void OnSelectionEnd();
// }

// Wrap a normal hashset so we can call callbacks when game objects are added and removed
// public class SelectionSet
// {
//     private HashSet<GameObject> _set;
//     private Action<GameObject> _selectCb;
//     private Action<GameObject> _deselectCb;

//     public SelectionSet(Action<GameObject> selectCb, Action<GameObject> deselectCb)
//     {
//         _set = new HashSet<GameObject>();
//         _selectCb = selectCb;
//         _deselectCb = deselectCb;
//     }

//     public bool Contains(GameObject gameObject)
//     {
//         return _set.Contains(gameObject);
//     }

//     public void Add(GameObject gameObject)
//     {
//         _selectCb(gameObject);
//         _set.Add(gameObject);
//     }

//     public void Remove(GameObject gameObject)
//     {
//         _deselectCb(gameObject);
//         _set.Remove(gameObject);
//     }

//     public void RemoveAllBut(GameObject gameObject)
//     {
//         List<GameObject> list = new List<GameObject>(_set);
//         foreach (GameObject go in list)
//         {
//             if (go != gameObject)
//             {
//                 Remove(go);
//             }
//         }
//     }

//     public void Clear()
//     {
//         foreach (GameObject gameObject in _set)
//         {
//             _deselectCb(gameObject);
//         }

//         _set.Clear();
//     }
// }

public class HoverCursorBuildPhase : MonoBehaviour
{
    // private SelectionSet _selectedObjects;

    [Header("Debug Options")]
    [SerializeField] private bool _showCursorRaycastPosition;
    [SerializeField] private GameObject _cursorRaycastMarkerPrefab;
    private GameObject _cursorRaycastMarker;

    private RaycastHit _activeRaycastHit;
    private GameObject _identityGameObject;
    private Reactive<GameObject> _hoverObject;
    private void Awake()
    {
        // TODO: ..... hmm.
        _identityGameObject = new GameObject("identity");
        _hoverObject = new Reactive<GameObject>(_identityGameObject);
        CustomInputManager.SubscribeToAction(ActionMapName.Default, ActionName.GameObjectSelect, OnGameObjectSelectInputAction);
        _hoverObject.OnChange(ProcessHoverChange);
        // _selectedObjects = new SelectionSet(TriggerSelectionStartCb, TriggerSelectionEndCb);
    }

    private void OnGameObjectSelectInputAction(CallbackContext context)
    {
        GameObject hoverObject = _hoverObject.Value;

        // If we're not hovering over anything, we definitely can't select it
        if (hoverObject == null)
        {
            return;
        }

        // If whatever we selected isn't selectable, we can't select it
        ISelectable selectable = hoverObject.GetComponent<ISelectable>();
        if (selectable == null)
        {
            return;
        }

        selectable.OnSelect(_activeRaycastHit);
    }

    private void Start()
    {
        // Debug purposes
        if (_showCursorRaycastPosition)
        {
            _cursorRaycastMarker = Instantiate(_cursorRaycastMarkerPrefab);
        }
    }

    // public void OnBeforeNextSceneSetup()
    // {
    // CustomInputManager.UnsubscribeFromAction(ActionMapName.Default, ActionName.Select, OnSelectInputAction);
    // }

    private void Update()
    {
        Vector2 rawMousePos = Mouse.current.position.ReadValue();
        Vector2 remappedMousePos = math.remap(
            Vector2.zero,
            new Vector2(Screen.width, Screen.height),
            Vector2.zero,
            ScreenUtils.ScreenResolution,
            rawMousePos
        );

        Ray ray = Camera.main.ScreenPointToRay(remappedMousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerUtils.GetMask(LayerName.BuildingBlocks)))
        {
            _activeRaycastHit = hit;
            _hoverObject.Value = hit.collider.gameObject;

            if (_showCursorRaycastPosition)
            {
                _cursorRaycastMarker.transform.position = hit.point;
            }
        }
        else
        {
            _hoverObject.Value = _identityGameObject;
        }
    }

    private void ProcessHoverChange(GameObject prev, GameObject curr)
    {
        // We need to trigger the leave callback of whatever we were hovering over before
        if (prev != null)
        {
            TriggerHoverExitCb(prev);
        }

        if (curr != null)
        {
            TriggerHoverEnterCb(curr);
        }
    }

    private void TriggerHoverEnterCb(GameObject gameObject)
    {
        IHoverable hoverable = gameObject.GetComponent<IHoverable>();
        if (hoverable == null)
        {
            return;
        }

        hoverable.OnHoverEnter(_activeRaycastHit);
    }

    private void TriggerHoverExitCb(GameObject gameObject)
    {
        IHoverable hoverable = gameObject.GetComponent<IHoverable>();
        if (hoverable == null)
        {
            return;
        }

        hoverable.OnHoverExit(_activeRaycastHit);
    }
}

// private void OnSelectInputAction(CallbackContext _)
// {
//     GameObject hoverObject = _hoverObject.Get();

//     // If we're not hovering over anything, we definitely can't select it
//     if (hoverObject == null)
//     {
//         return;
//     }

//     // If whatever we selected isn't selectable, we can deselect everything
//     ISelectable selectable = hoverObject.GetComponent<ISelectable>();
//     if (selectable == null)
//     {
//         _selectedObjects.Clear();
//         return;
//     }

//     SelectSingle(hoverObject);
// }

// private void SelectSingle(GameObject gameObject)
// {
//     if (_selectedObjects.Contains(gameObject))
//     {
//         _selectedObjects.RemoveAllBut(gameObject);
//     }
//     else
//     {
//         _selectedObjects.Clear();
//         _selectedObjects.Add(gameObject);
//     }
// }

//     private void TriggerSelectionStartCb(GameObject gameObject)
//     {
//         ISelectable selectable = gameObject.GetComponent<ISelectable>();
//         if (selectable == null)
//         {
//             return;
//         }

//         selectable.OnSelectionStart();
//     }

//     private void TriggerSelectionEndCb(GameObject gameObject)
//     {
//         ISelectable selectable = gameObject.GetComponent<ISelectable>();
//         if (selectable == null)
//         {
//             return;
//         }

//         selectable.OnSelectionEnd();
//     }
// }
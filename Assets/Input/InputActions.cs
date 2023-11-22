using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public enum ActionMapName
{
  Default,
  UI
}

public enum ActionName
{
  Select,
  UiSelect,
  RotateCameraClockwise,
  RotateCameraCounterClockwise,
  RotateHouseClockwise,
  RotateHouseCounterClockwise,
  Move,
  Proceed
}

public static class CustomInputManager
{
  private static InputActionAsset _asset;
  private static InputActionMap _currMap;

  // For starting on an arbitrary scene from the editor
  [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
  private static void BeforeSceneLoad()
  {
    // Figure out which scene is active...
    bool foundActiveScene = false;
    foreach (SceneSettingsSO sceneSettings in ResourceManager.GetAllOfType<SceneSettingsSO>())
    {
      if (!sceneSettings.IsActiveScene)
      {
        continue;
      }

      foundActiveScene = true;
      _asset = sceneSettings.InputActionAsset;
      _currMap = _asset.actionMaps[0];
      _currMap.Enable();
    }

    if (!foundActiveScene)
    {
      Debug.LogWarning("CustomInputManager: could not initialize input action asset because could not find active scene");
    }
    else
    {
      Debug.Log($"CustomInputManager: initialized active input action asset to {_asset}");
    }
  }

  public static void SetInputActionAsset(InputActionAsset asset)
  {
    _asset.Disable();
    _asset = asset;
    _currMap = _asset.actionMaps[0];
    _currMap.Enable();
  }

  public static InputAction GetAction(ActionMapName actionMapName, ActionName actionName)
  {
    string actionMapNameString = GetActionMapNameString(actionMapName);
    string actionNameString = GetActionNameString(actionName);

    return _asset.FindActionMap(actionMapNameString)[actionNameString];
  }

  public static void SetCurrActionMap(ActionMapName actionMapName)
  {
    _currMap.Disable();
    _currMap = _asset.FindActionMap(GetActionMapNameString(actionMapName));
    _currMap.Enable();
  }

  public static void DisableCurrActionMap()
  {
    _currMap.Disable();
  }

  private static string GetActionMapNameString(ActionMapName actionMapName)
  {
    return Enum.GetName(typeof(ActionMapName), actionMapName);
  }

  private static string GetActionNameString(ActionName actionName)
  {
    return Enum.GetName(typeof(ActionName), actionName);
  }

  public static void SubscribeToAction(ActionMapName actionMapName, ActionName actionName, Action<CallbackContext> cb)
  {
    GetAction(actionMapName, actionName).performed += cb;
  }

  public static void UnsubscribeFromAction(ActionMapName actionMapName, ActionName actionName, Action<CallbackContext> cb)
  {
    GetAction(actionMapName, actionName).performed -= cb;
  }
}
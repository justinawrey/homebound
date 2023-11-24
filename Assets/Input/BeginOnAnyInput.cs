using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class BeginOnAnyInput : MonoBehaviour
{
    [SerializeField] SceneSettingsSO toSceneSettingsSO;

    private void Awake()
    {
        CustomInputManager.SubscribeToAction(ActionMapName.Default, ActionName.Proceed, StartGame);
    }

    public void OnBeforeNextSceneSetup()
    {
        CustomInputManager.UnsubscribeFromAction(ActionMapName.Default, ActionName.Proceed, StartGame);
    }

    private void StartGame(CallbackContext ctx)
    {
        toSceneSettingsSO.Load();
    }
}

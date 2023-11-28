using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public struct SceneSwitchKey
{
    public KeyCode KeyCode;
    public SceneSettingsSO SceneSettingsSO;
    public UnityEvent Cb; // for debugging
}

public class DebugSceneSwitch : MonoBehaviour
{
    [SerializeField] private List<SceneSwitchKey> _sceneSwitchKeys;

    private void Update()
    {
        foreach (SceneSwitchKey sceneSwitchKey in _sceneSwitchKeys)
        {
            KeyCode code = sceneSwitchKey.KeyCode;
            SceneSettingsSO settingsSO = sceneSwitchKey.SceneSettingsSO;
            UnityEvent cb = sceneSwitchKey.Cb;

            if (Input.GetKeyDown(code))
            {
                settingsSO.Load();
                cb?.Invoke();
                break;
            }
        }
    }
}

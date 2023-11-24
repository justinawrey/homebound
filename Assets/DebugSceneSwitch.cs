using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct SceneSwitchKey
{
    public KeyCode KeyCode;
    public SceneSettingsSO SceneSettingsSO;
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

            if (Input.GetKeyDown(code))
            {
                settingsSO.Load();
                break;
            }
        }
    }
}

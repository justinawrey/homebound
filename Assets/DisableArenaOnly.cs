using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisableArenaOnly : MonoBehaviour
{
    [SerializeField] private SceneSettingsSO _arenaScene;
    [SerializeField] private List<MonoBehaviour> _arenaOnlyScripts;

    private void Awake()
    {
        bool arenaSceneActive = SceneManager.GetActiveScene().name == _arenaScene.SceneName;
        if (!arenaSceneActive)
        {
            _arenaOnlyScripts.ForEach(script => script.enabled = false);
        }
    }
}

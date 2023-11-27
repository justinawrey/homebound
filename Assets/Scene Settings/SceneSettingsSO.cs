using System.Collections;
using Eflatun.SceneReference;
using SceneTransitions;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/// <summary>
/// Holds scene-related metadata.
/// Call Load() to load the associated scene and perform any needed initialization.
/// </summary>
[CreateAssetMenu(fileName = "SceneSettingsSO", menuName = "Assets/SceneSettingsSO")]
public class SceneSettingsSO : IdentifiableSO
{
  [SerializeField] private SceneReference _sceneReference;
  [SerializeField] private InputActionAsset _inputActionAsset;

  public string SceneName
  {
    get
    {
      return _sceneReference.Name;
    }
  }

  public bool IsActiveScene
  {
    get
    {
      return SceneManager.GetActiveScene().name == _sceneReference.Name;
    }
  }

  public InputActionAsset InputActionAsset
  {
    get
    {
      return _inputActionAsset;
    }
  }

  public void Load()
  {
    SceneTransitionManager.LoadScene(_sceneReference.Name, InitializeActionMapRoutine());
  }

  private IEnumerator InitializeActionMapRoutine()
  {
    CustomInputManager.SetInputActionAsset(_inputActionAsset);
    yield return null;
  }
}
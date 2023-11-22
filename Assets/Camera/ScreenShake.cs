using UnityEngine;
using System.Collections;
using Cinemachine;

public class ScreenShake : MonoBehaviour
{
  [SerializeField] private CinemachineVirtualCamera vCam;
  [SerializeField] private NoiseSettings idleNoiseSetting;
  [SerializeField] private NoiseSettings shakingNoiseSetting;
  [SerializeField] private float noiseAmplitude = 0.3f;

  private bool shaking = false;
  private CinemachineBasicMultiChannelPerlin noiseComponent;
  private float originalAmplitude;

  private void Awake()
  {
    EventBus.OnScreenShakeFor += ScreenShakeForSeconds;
    noiseComponent = vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    originalAmplitude = noiseComponent.m_AmplitudeGain;
  }

  private void Update()
  {
    if (shaking)
    {
      SetShaking();
    }
    else
    {
      SetIdle();
    }
  }

  private void OnDisable()
  {
    EventBus.OnScreenShakeFor -= ScreenShakeForSeconds;
  }

  private void ScreenShakeForSeconds(float time)
  {
    StartCoroutine(ShakeRoutine(time));
  }

  private IEnumerator ShakeRoutine(float time)
  {
    shaking = true;
    yield return new WaitForSecondsRealtime(time);
    shaking = false;
  }

  private void SetShaking()
  {
    noiseComponent.m_AmplitudeGain = noiseAmplitude;
    noiseComponent.m_NoiseProfile = shakingNoiseSetting;
  }

  private void SetIdle()
  {
    noiseComponent.m_AmplitudeGain = originalAmplitude;
    noiseComponent.m_NoiseProfile = idleNoiseSetting;
  }
}
using UnityEngine;
using System.Collections;

public class FreezeFrame : MonoBehaviour
{
  private void Awake()
  {
    EventBus.OnFreezeFrameFor += FreezeFrameForSeconds;
  }

  private void OnDisable()
  {
    EventBus.OnFreezeFrameFor -= FreezeFrameForSeconds;
  }

  private void FreezeFrameForSeconds(float time)
  {
    StartCoroutine(FreezeRoutine(time));
  }

  private IEnumerator FreezeRoutine(float time)
  {
    Time.timeScale = 0;
    yield return new WaitForSecondsRealtime(time);
    Time.timeScale = 1;
  }
}
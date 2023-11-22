using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
  [SerializeField] private Image _completionImage;

  public void SetCompletionPercent(float percent)
  {
    _completionImage.fillAmount = percent;
  }
}
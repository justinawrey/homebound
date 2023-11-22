using UnityEngine;

public class PlayerHealthBar : MonoBehaviour
{
    private HealthBar _healthBar;

    private void Awake()
    {
        _healthBar = GetComponent<HealthBar>();
        EventBus.OnPlayerHealthChange += ChangePlayerHealthBar;
    }

    private void OnDestroy()
    {
        EventBus.OnPlayerHealthChange -= ChangePlayerHealthBar;
    }

    private void ChangePlayerHealthBar(float completionPercent)
    {
        _healthBar.SetCompletionPercent(completionPercent);
    }
}

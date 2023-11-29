using UnityEngine;

public class InitializeToPercentHealth : MonoBehaviour, IUiInitializer
{
    [SerializeField] private HealthBar _healthBar;

    public void Initialize(GameObject gameObject)
    {
        Health health = gameObject.GetComponent<Health>();
        _healthBar.SetCompletionPercent(health.GetCurrPercentHealth());
    }
}
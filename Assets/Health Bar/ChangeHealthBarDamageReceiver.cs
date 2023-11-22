using UnityEngine;

public class ChangeHealthBarDamageReceiver : MonoBehaviour, IDamageReceiver
{
    [SerializeField] private ScreenSpaceUI _screenSpaceUi;

    private HealthBar _healthBar;
    private bool _hit = false;

    // Lazy cache the health bar... this should hopefully be a special case
    public void OnReceiveDamage(float percentHealthRemaining, GameObject _, float __)
    {
        // only enable the health bar once you are hit
        if (!_hit)
        {
            _screenSpaceUi.enabled = true;
            _hit = true;
            // TODO: waddafaaaa
            return;
        }

        if (_healthBar == null)
        {
            _healthBar = _screenSpaceUi.GetUiInstance().GetComponent<HealthBar>();
        }

        // TODO: this is a bit janky do to race conditions with the screen space ui health bar initialization
        // I think the player won't notice though?
        if (_healthBar != null)
        {
            _healthBar.SetCompletionPercent(percentHealthRemaining);
        }
    }
}

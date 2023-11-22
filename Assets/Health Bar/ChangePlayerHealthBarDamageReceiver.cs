using UnityEngine;

public class ChangePlayerHealthBarDamageReceiver : MonoBehaviour, IDamageReceiver
{
  public void OnReceiveDamage(float percentHealthRemaining, GameObject damageDealer, float rawDamageDealt)
  {
    EventBus.ChangePlayerHealth(percentHealthRemaining);
  }
}
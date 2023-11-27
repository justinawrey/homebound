using UnityEngine;

public class TrackKillDeathReceiver : MonoBehaviour, IDeathReceiver
{
  [SerializeField] private PlayerStatsSO _playerStatsSO;

  public void OnDeath(GameObject deathDealer)
  {
    // You don't track kills if the cause of death was END OF DAY lol
    if (TagUtils.CompareTag(deathDealer, TagName.EnemySpawner))
    {
      return;
    }

    _playerStatsSO.TotalEnemiesSlain.Value += 1;
  }
}
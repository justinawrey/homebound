using UnityEngine;

/// <summary>
/// Holds player stats.
/// </summary>
[CreateAssetMenu(fileName = "PlayerStatsSO", menuName = "Assets/PlayerStatsSO")]
public class PlayerStatsSO : IdentifiableSO, IDamageable
{
    // Layout of the house.
    public HouseBuildSO HouseBuild;

    // Stats.
    // TODO: this save system... hmm....
    [SaveData] public PersistedReactive<float> CurrHealth = new PersistedReactive<float>(100);
    [SaveData] public PersistedReactive<float> TotalHealth = new PersistedReactive<float>(100);
    [SaveData] public PersistedReactive<int> AccruedLevels = new PersistedReactive<int>(0);
    [SaveData] public PersistedReactive<int> Level = new PersistedReactive<int>(1);
    [SaveData] public PersistedReactive<int> CurrExp = new PersistedReactive<int>(0);
    [SaveData] public PersistedReactive<int> RequiredToNextLevel = new PersistedReactive<int>(10);
    [SaveData] public PersistedReactive<int> Money = new PersistedReactive<int>(0);

    public float GetHealth()
    {
        return CurrHealth.Value;
    }

    public float GetTotalHealth()
    {
        return TotalHealth.Value;
    }

    public void SetHealth(float to)
    {
        CurrHealth.Value = to;
    }
}
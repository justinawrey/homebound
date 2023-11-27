using System;
using ReactiveUnity;
using UnityEngine;

[Serializable]
public class PlayerStat
{
    public string Name;
    public Reactive<int> Amount = new Reactive<int>(0);
}

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
    public Reactive<float> CurrHealth = new Reactive<float>(100);
    public Reactive<float> TotalHealth = new Reactive<float>(100);
    public Reactive<int> AccruedLevels = new Reactive<int>(0);
    public Reactive<int> Level = new Reactive<int>(1);
    public Reactive<int> CurrExp = new Reactive<int>(0);
    public Reactive<int> RequiredToNextLevel = new Reactive<int>(10);
    public Reactive<int> Money = new Reactive<int>(0);

    // "fighting" related stats
    public PlayerStat Damage;
    public PlayerStat Speed;
    public PlayerStat Armor;

    // "meta" related stats
    public Reactive<int> TotalMoneyAcquired = new Reactive<int>(0);
    public Reactive<int> TotalEnemiesSlain = new Reactive<int>(0);

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
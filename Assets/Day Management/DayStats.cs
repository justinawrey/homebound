using System;
using System.Collections.Generic;
using UnityEngine;

public class DayStats : MonoBehaviour
{
    [SerializeField] private PlayerStatsSO _playerStatsSO;

    private int _enemiesSlainInDay = 0;
    private int _moneyAcquiredInDay = 0;
    private int _levelsAcquiredInDay = 0;

    public int EnemiesSlainInDay
    {
        get
        {
            return _enemiesSlainInDay;
        }
    }

    public int MoneyAcquiredInDay
    {
        get
        {
            return _moneyAcquiredInDay;
        }
    }

    public int LevelsAcquiredInDay
    {
        get
        {
            return _levelsAcquiredInDay;
        }
    }

    private List<Action> _unsubCbs = new List<Action>();

    private void Start()
    {
        _unsubCbs.Add(_playerStatsSO.TotalMoneyAcquired.OnChange(TrackMoney));
        _unsubCbs.Add(_playerStatsSO.TotalEnemiesSlain.OnChange(TrackEnemiesSlain));
        _unsubCbs.Add(_playerStatsSO.Level.OnChange(TrackLevelUps));
    }

    private void OnDestroy()
    {
        _unsubCbs.ForEach(unsub => unsub());
    }

    private void TrackEnemiesSlain(int prev, int amount)
    {
        int diff = amount - prev;
        _enemiesSlainInDay += diff;
    }

    private void TrackMoney(int prev, int amount)
    {
        int diff = amount - prev;
        _moneyAcquiredInDay += diff;
    }

    private void TrackLevelUps(int prev, int amount)
    {
        int diff = amount - prev;
        _levelsAcquiredInDay += diff;
    }

    public void ResetDayStats()
    {
        _enemiesSlainInDay = 0;
        _moneyAcquiredInDay = 0;
        _levelsAcquiredInDay = 0;
    }
}

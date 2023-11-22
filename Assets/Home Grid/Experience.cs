using UnityEngine;

public class Experience : MonoBehaviour
{
    [SerializeField] private PlayerStatsSO _playerStatsSO;

    private void Start()
    {
        _playerStatsSO.Level.OnChange((prev, curr) => CalculateExpRequirementsForLevel(curr));
    }

    // TODO: this formula is rudimentary at best
    // use required exp for level up = level * 10
    private void CalculateExpRequirementsForLevel(int curr)
    {
        _playerStatsSO.RequiredToNextLevel.Value = curr * 10;
    }

    public void AddExp(int amount)
    {
        _playerStatsSO.CurrExp.Value += amount;
        if (_playerStatsSO.CurrExp.Value >= _playerStatsSO.RequiredToNextLevel.Value)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        _playerStatsSO.Level.Value += 1;
        _playerStatsSO.CurrExp.Value = 0;
    }
}
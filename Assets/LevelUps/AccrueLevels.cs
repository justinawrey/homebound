using System;
using TMPro;
using UnityEngine;

public class AccrueLevels : MonoBehaviour
{
    [SerializeField] private PlayerStatsSO _playerStatsSO;
    [SerializeField] private TextMeshProUGUI _textComponent;

    private Action _levelUnsub;
    private Action _accruedLevelsUnsub;

    private void Awake()
    {
        _levelUnsub = _playerStatsSO.Level.OnChange((_, __) => AccrueLevel());
        _accruedLevelsUnsub = _playerStatsSO.AccruedLevels.OnChange((_, curr) => SetText(curr));
    }

    private void OnDestroy()
    {
        _levelUnsub();
        _accruedLevelsUnsub();
    }

    private void Start()
    {
        SetText(_playerStatsSO.AccruedLevels.Value);
    }

    private void AccrueLevel()
    {
        _playerStatsSO.AccruedLevels.Value += 1;
    }

    public bool CanSpendLevel()
    {
        return _playerStatsSO.AccruedLevels.Value > 0;
    }

    public void SpendLevel()
    {
        _playerStatsSO.AccruedLevels.Value -= 1;
    }

    public void SetText(int to)
    {
        _textComponent.text = $"AVAILABLE LEVEL UPS: {to}";
    }
}

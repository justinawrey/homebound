using System;
using System.Reflection;
using TMPro;
using UnityEngine;

public class DisplayedStat : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textComponent;
    [SerializeField] private PlayerStatsSO _playerStatsSO;
    [SerializeField] private string _fieldName;

    private PlayerStat _stat;
    private Action _unsub;

    private void Start()
    {
        // sweet sweet reflection
        Type t = typeof(PlayerStatsSO);
        FieldInfo fieldInfo = t.GetField(_fieldName);
        _stat = (PlayerStat)fieldInfo.GetValue(_playerStatsSO);

        UpdateDisplayedStat(_stat.Amount.Value);
        _unsub = _stat.Amount.OnChange((_, curr) => UpdateDisplayedStat(curr));
    }

    private void UpdateDisplayedStat(int to)
    {
        _textComponent.text = $"{_stat.Name}: {to}";
    }

    private void OnDestroy()
    {
        _unsub();
    }
}

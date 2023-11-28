using System;
using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class DisplayCurrentDay : MonoBehaviour
{
    [SerializeField] private PlayerStatsSO _playerStatsSO;
    [SerializeField] TextMeshProUGUI _textComponent;

    private Action _unsub;

    private void Awake()
    {
        UpdateDayText(_playerStatsSO.CurrentDay.Value);
        _unsub = _playerStatsSO.CurrentDay.OnChange((prev, curr) => UpdateDayText(curr));
    }

    private void OnDestroy()
    {
        _unsub();
    }

    private void UpdateDayText(int to)
    {
        _textComponent.text = $"DAY: {to}";
    }
}
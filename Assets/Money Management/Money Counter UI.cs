using System;
using TMPro;
using UnityEngine;

public class MoneyCounterUI : MonoBehaviour
{
    [SerializeField] private PlayerStatsSO _playerStatsSO;
    private TextMeshProUGUI _textComponent;

    private Action _unsub;

    private void Awake()
    {
        _textComponent = GetComponent<TextMeshProUGUI>();
        _unsub = _playerStatsSO.Money.OnChange((prev, curr) => UpdateUi(curr));
        UpdateUi(_playerStatsSO.Money.Value);
    }

    private void OnDestroy()
    {
        _unsub();
    }

    private void UpdateUi(int curr)
    {
        _textComponent.text = $"DUBLOONS: {curr}$";
    }
}

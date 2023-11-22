using System;
using TMPro;
using UnityEngine;

public class MoneyCounterUI : MonoBehaviour
{
    [SerializeField] private PlayerStatsSO _playerStatsSO;
    private TextMeshProUGUI _textComponent;

    private void Awake()
    {
        _textComponent = GetComponent<TextMeshProUGUI>();
        _playerStatsSO.Money.OnChange((prev, curr) => UpdateUi(curr));
        UpdateUi(_playerStatsSO.Money.Value);
    }

    private void UpdateUi(int curr)
    {
        _textComponent.text = $"DUBLOONS: {curr}$";
    }
}

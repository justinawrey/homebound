using TMPro;
using UnityEngine;

public class DisplayPlayerHealth : MonoBehaviour
{
    [SerializeField] private PlayerStatsSO _playerStatsSO;
    [SerializeField] private TextMeshProUGUI _textDisplayComponent;

    private void Start()
    {
        _playerStatsSO.CurrHealth.OnChange((prev, curr) => SetHealthString(curr, _playerStatsSO.TotalHealth.Value));
        _playerStatsSO.TotalHealth.OnChange((prev, curr) => SetHealthString(_playerStatsSO.CurrHealth.Value, curr));

        SetHealthString(_playerStatsSO.CurrHealth.Value, _playerStatsSO.TotalHealth.Value);
    }

    private void SetHealthString(float curr, float total)
    {
        _textDisplayComponent.text = $"{curr}/{total}";
    }
}

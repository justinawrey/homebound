using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    [SerializeField] private PlayerStatsSO _playerStatsSO;
    [SerializeField] private TextMeshProUGUI _levelDisplayComponent;
    [SerializeField] private TextMeshProUGUI _valuesDisplayComponent;
    [SerializeField] private Image _completionImage;

    private void Start()
    {
        _playerStatsSO.CurrExp.OnChange((prev, curr) => SetExp(curr, _playerStatsSO.RequiredToNextLevel.Value));
        _playerStatsSO.RequiredToNextLevel.OnChange((prev, curr) => SetExp(_playerStatsSO.CurrExp.Value, curr));
        SetExp(_playerStatsSO.CurrExp.Value, _playerStatsSO.RequiredToNextLevel.Value);

        _playerStatsSO.Level.OnChange((prev, curr) => SetLevelString(curr));
        SetLevelString(_playerStatsSO.Level.Value);
    }

    private void SetExp(int curr, int total)
    {
        _valuesDisplayComponent.text = $"{curr}/{total}";
        _completionImage.fillAmount = (float)curr / (float)total;
    }

    private void SetLevelString(int level)
    {
        _levelDisplayComponent.text = $"LVL {level}";
    }
}

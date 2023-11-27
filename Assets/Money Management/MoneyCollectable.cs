using UnityEngine;

public class MoneyCollectable : MonoBehaviour, ICollectable
{
    [SerializeField] private PlayerStatsSO _playerStatsSO;

    private SinBounce _sinBounce;
    private MoneyValue _moneyValue;

    private void Awake()
    {
        _sinBounce = GetComponent<SinBounce>();
        _moneyValue = GetComponent<MoneyValue>();
    }

    // disable the sin bounce so not to interfere with the tween
    // also stop the expiration routine so the money doesn't expire
    // mid way through the tween.....
    public void OnCollectStart()
    {
        _sinBounce.enabled = false;
    }

    public void OnCollectEnd()
    {
        int value = _moneyValue.GetValue();
        _playerStatsSO.Money.Value += value;
        _playerStatsSO.TotalMoneyAcquired.Value += value;
    }
}
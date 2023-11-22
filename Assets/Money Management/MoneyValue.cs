using UnityEngine;

public class MoneyValue : MonoBehaviour
{
    [SerializeField] private int _moneyValue = 1;

    public int GetValue()
    {
        return _moneyValue;
    }
}

using UnityEngine;

public class DestoryAllMoney : MonoBehaviour
{
    private void Awake()
    {
        EventBus.OnDayEnd += DestroyAll;
    }

    private void DestroyAll()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}

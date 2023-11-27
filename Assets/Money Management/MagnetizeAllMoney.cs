using UnityEngine;

public class MagnetizeAllMoney : MonoBehaviour
{
    private AttractCollectables _attractCollectablesComponent;

    private void Awake()
    {
        EventBus.OnDayEnd += MagnetizeAll;
    }

    private void Start()
    {
        _attractCollectablesComponent = TagUtils.FindWithTag(TagName.MoneyMagnet).GetComponent<AttractCollectables>();
    }

    private void MagnetizeAll()
    {
        _attractCollectablesComponent.MagnetizeAllUnderTransform(transform);
    }
}

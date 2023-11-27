using System.Collections;
using UnityEngine;

public class MagnetizeAllMoney : MonoBehaviour
{
    // [SerializeField] private float _delay = 1f;

    // private void Awake()
    // {
    //     EventBus.OnDayEnd += MagnetizeAll;
    // }

    // private void OnDestroy()
    // {
    //     EventBus.OnDayEnd -= MagnetizeAll;
    // }

    private void OnEnable()
    {
        AttractCollectables attractCollectables = TagUtils.FindWithTag(TagName.MoneyMagnet).GetComponent<AttractCollectables>();
        attractCollectables.MagnetizeAllUnderTransform(transform);
    }

    // private void ExecuteMagnizeRoutine()
    // {
    //     StartCoroutine(MagnetizeRoutine());
    // }

    // private IEnumerator MagnetizeRoutine()
    // {
    //     yield return new WaitForSeconds(_delay);
    //     MagnetizeAll();
    // }
}

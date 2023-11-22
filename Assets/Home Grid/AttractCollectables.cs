using DG.Tweening;
using UnityEngine;

public interface ICollectable
{
    public void OnCollectStart();
    public void OnCollectEnd();
}

public class AttractCollectables : MonoBehaviour
{
    [SerializeField] private float _travelDuration = 0.5f;

    private void OnTriggerEnter(Collider other)
    {
        ICollectable[] collectables = other.gameObject.GetComponentsInChildren<ICollectable>();
        if (collectables.Length <= 0)
        {
            return;
        }

        Magnetize(other.gameObject, collectables);
    }

    private void Magnetize(GameObject other, ICollectable[] collectables)
    {
        foreach (ICollectable collectable in collectables)
        {
            collectable.OnCollectStart();
        }

        var t = DOTween.To(
            () => other.transform.position - transform.position, // Value getter
            x => other.transform.position = x + transform.position, // Value setter
            Vector3.zero,
            _travelDuration
        );
        t.SetTarget(other.transform);
        t.OnComplete(() => Collect(other, collectables));
    }

    // One single game object may have multiple ocllectables on it
    private void Collect(GameObject obj, ICollectable[] collectables)
    {
        foreach (ICollectable collectable in collectables)
        {
            collectable.OnCollectEnd();
        }
        Destroy(obj);
    }
}

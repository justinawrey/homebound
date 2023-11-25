using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    [SerializeField] private Transform _parentToDestroy;

    public void DestroyParent()
    {
        Destroy(_parentToDestroy.gameObject);
    }
}

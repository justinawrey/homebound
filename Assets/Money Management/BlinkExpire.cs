using System;
using System.Collections;
using UnityEngine;

public class BlinkExpire : MonoBehaviour
{
    [SerializeField] private float _expirationTime = 10f;
    [SerializeField] private float _blinkTime = 3f;
    [SerializeField] private float _blinkInterval = 0.5f;

    private MeshRenderer[] _meshRenderers;
    private Coroutine _expiryRoutine;

    private void Awake()
    {
        _meshRenderers = GetComponentsInChildren<MeshRenderer>();
    }

    public void StopExpiryRoutine()
    {
        StopCoroutine(_expiryRoutine);
    }

    private void Start()
    {
        _expiryRoutine = StartCoroutine(ExpiryRoutine());
    }

    private IEnumerator ExpiryRoutine()
    {
        // don't blink for a while
        yield return new WaitForSeconds(_expirationTime - _blinkTime);

        // start blinking
        float count = 0;
        bool visible = true;
        while (count <= _blinkTime)
        {
            visible = !visible;
            ExecuteOnMeshRenderers(meshRenderer => meshRenderer.enabled = visible);
            count += _blinkInterval;
            yield return new WaitForSeconds(_blinkInterval);
        }

        Destroy(gameObject);
    }

    private void ExecuteOnMeshRenderers(Action<MeshRenderer> cb)
    {
        foreach (MeshRenderer meshRenderer in _meshRenderers)
        {
            cb(meshRenderer);
        }
    }
}

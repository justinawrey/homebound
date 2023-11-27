using System.Collections;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    [Header("Material change options")]
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Transform _parentToDestroy;
    [SerializeField] private Material _damageMaterial;
    [SerializeField] private Material _dissolveMaterial;
    [SerializeField] private float _flashTime = 0.075f;
    [SerializeField] private float _dissolveTime = 1f;

    [Header("Particle options")]
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private int _minEmissionAmount = 3;
    [SerializeField] private int _maxEmissionAmount = 6;

    private Material _instancedDissolveMaterial;

    private IEnumerator Start()
    {
        _particleSystem.Emit(Random.Range(_minEmissionAmount, _maxEmissionAmount + 1));
        _meshRenderer.material = _damageMaterial;
        yield return new WaitForSeconds(_flashTime);

        // A terribly weird way to use a shared material
        // set the material on the meshrenderer to the dissolve material,
        // and then RE get it right away to trigger the renderer.material getter
        // which will make an instanced copy of the original
        _meshRenderer.material = _dissolveMaterial;
        _instancedDissolveMaterial = _meshRenderer.material;

        float t = 0;
        while (t <= _dissolveTime)
        {
            t += Time.deltaTime;
            float dissolveVal = Mathf.Lerp(0, 1, t / _dissolveTime);
            _instancedDissolveMaterial.SetFloat("_t", dissolveVal);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    public void DestroyParent()
    {
        Destroy(_parentToDestroy.gameObject);
    }
}

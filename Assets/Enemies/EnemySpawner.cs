using System.Collections;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private float _spawnInterval = 3f;
    [SerializeField] private float _spawnSphereMin = 5f;
    [SerializeField] private float _spawnSphereMax = 8f;
    [SerializeField] private Transform _enemiesContainer;
    [SerializeField] private float _spawnMarkerDuration = 2f;
    [SerializeField] private GameObject _spawnMarkerPrefab;

    [Header("Debug Options")]
    [SerializeField] private bool _drawGizmos = false;

    private Transform _houseTransform;

    private void Awake()
    {
        _enemiesContainer = TagUtils.FindWithTag(TagName.EnemiesContainer).transform;
        _houseTransform = TagUtils.FindWithTag(TagName.House).transform;
        EventBus.OnDayStart += StartSpawning;
        EventBus.OnDayEnd += StopSpawning;
    }

    private void OnDestroy()
    {
        EventBus.OnDayStart -= StartSpawning;
        EventBus.OnDayEnd -= StopSpawning;
    }

    private void StartSpawning()
    {
        StartCoroutine(PeriodicSpawnRoutine());
    }

    private void StopSpawning()
    {
        StopAllCoroutines();

        foreach (Transform child in _enemiesContainer)
        {
            // If its an enemy kill it
            if (TagUtils.CompareTag(child.gameObject, TagName.Enemy))
            {
                // In this case, the damage dealer is the spawner.  kinda weird but fine
                // wont be used anyways
                child.gameObject.GetComponent<Health>().Kill(gameObject);
                continue;
            }

            // otherwise its a marker so just destroy it
            Destroy(child.gameObject);
        }
    }

    private IEnumerator PeriodicSpawnRoutine()
    {
        while (true)
        {
            Vector3 spawnPos;
            if (!GetSpawnPosition(out spawnPos))
            {
                continue;
            }

            StartCoroutine(SpawnEnemyRoutine(spawnPos));
            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    private IEnumerator SpawnEnemyRoutine(Vector3 spawnPos)
    {
        // show marker somehow
        GameObject marker = Instantiate(_spawnMarkerPrefab, spawnPos, Quaternion.identity, _enemiesContainer);
        yield return new WaitForSeconds(_spawnMarkerDuration);
        Destroy(marker);
        Instantiate(_enemyPrefab, spawnPos, Quaternion.identity, _enemiesContainer);
    }

    private bool GetSpawnPosition(out Vector3 position)
    {
        // TODO: might need a more accurate height value?
        NavMeshHit hit;
        if (NavMesh.SamplePosition((Random.insideUnitSphere * Random.Range(_spawnSphereMin, _spawnSphereMax)) + _houseTransform.position, out hit, 2, NavMesh.AllAreas))
        {
            position = hit.position;
            return true;
        }

        position = Vector3.zero;
        return false;
    }

    private void OnDrawGizmos()
    {
        if (!_drawGizmos)
        {
            return;
        }

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(new Vector3(0, 0, 0), _spawnSphereMin);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(new Vector3(0, 0, 0), _spawnSphereMax);
    }

}

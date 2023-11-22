using UnityEngine;
using UnityEngine.AI;

public class Chase : MonoBehaviour
{
    private Transform _chaseTarget;
    private NavMeshAgent _navMeshAgent;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _chaseTarget = TagUtils.FindWithTag(TagName.House).transform;
    }

    private void Update()
    {
        _navMeshAgent.SetDestination(_chaseTarget.position);
    }
}

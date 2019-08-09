using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class UnitNavigation : MonoBehaviour
{

    private NavMeshAgent navMeshAgent;
    private float unitSize;

    private bool reachedDestination = true;

    void Awake()
    {
        if (navMeshAgent == null)
        {
            gameObject.AddComponent<NavMeshAgent>();
            navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
            unitSize = GetComponent<Collider>().bounds.size.x;
        }
    }

    void Update()
    {
        //Maybe not optimal, yet. Needs more testing.
        if (!navMeshAgent.pathPending)
        {
            if (navMeshAgent.remainingDistance <= (unitSize + navMeshAgent.stoppingDistance))
            {
                if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude <= 0.6f)
                {
                    navMeshAgent.isStopped = true;
                    reachedDestination = true;
                }
            }
        }
    }

    public bool SetDestination(Vector3 target)
    {
        reachedDestination = false;
        return navMeshAgent.SetDestination(target);
    }

    public bool HasReachedDestination()
    {
        return reachedDestination;
    }

}

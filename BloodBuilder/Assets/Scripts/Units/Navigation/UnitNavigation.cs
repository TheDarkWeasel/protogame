using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class UnitNavigation : MonoBehaviour
{

    NavMeshAgent navMeshAgent;

    void Awake()
    {
        if(navMeshAgent == null)
        {
            gameObject.AddComponent<NavMeshAgent>();
            navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        }
    }

    void Update()
    {
        //Maybe not optimal, yet. Needs more testing.
        if (!navMeshAgent.pathPending)
        {
            if (navMeshAgent.remainingDistance <= (GetComponent<Collider>().bounds.size.x + navMeshAgent.stoppingDistance))
            {
                if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude <= 0.6f)
                {
                    navMeshAgent.isStopped = true;
                }
            }
        }
    }

    public bool SetDestination(Vector3 target)
    {
        return navMeshAgent.SetDestination(target);
    }
}

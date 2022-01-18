using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform[] waypoints;
    NavMeshAgent agent;
    int waypointIndex;
    [SerializeField] float distance;
    private Vector3 target;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Patrol();
        

    }

    
    void Update()
    {
        if (Vector3.Distance(transform.position, target) < distance)
        {
            IncreaseIndex();
            Patrol();
        }
    }

    void Patrol()
    {
        target = waypoints[waypointIndex].position;
        agent.SetDestination(target);
    }

    void IncreaseIndex()
    {
        waypointIndex++;
        if (waypointIndex >= waypoints.Length)
        {
            waypointIndex = 0;
        }
    }
}

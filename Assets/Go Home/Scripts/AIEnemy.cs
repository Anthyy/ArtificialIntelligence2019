using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIEnemy : MonoBehaviour
{
    public Transform target; //

    private NavMeshAgent agent; // Reference to NavMeshAgent component

    void Start()
    {
        // Getting NavMeshAgent from list of components
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        // Tell NavMeshAgent to follow target position
        agent.SetDestination(target.position);
    }
}

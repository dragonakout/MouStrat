using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolBehaviour : MonoBehaviour
{
    NavMeshAgent agent;

    [SerializeField] List<GameObject> waypointList;
    [SerializeField] int waypointIndex;
    [SerializeField] float deltaPositionWaypoint;

    private bool isPassive;

    // Start is called before the first frame update
    void Start()
    {
        deltaPositionWaypoint = 2;
        // For now, each patrol need to have at least one waypoint
        if(waypointList.Count == 0)
        {
            Destroy(gameObject);
        }
        // Patrol is passive by default
        isPassive = true;
        waypointIndex = 0;
        agent = GetComponent<NavMeshAgent>();
        StartPatrolling();
    }

    // Update is called once per frame
    void Update()
    {
        //TODO add a condition to check less often 
        if(isPassive)
        {
            SwitchDestinationPassive();
        }
        
        
    }

    // Initiate the patrol with the first waypoint
    private void StartPatrolling()
    {
        agent.destination = waypointList[waypointIndex].transform.position;
    }

    // Check if a waypoint has been reached and switch to the next one
    private void SwitchDestinationPassive()
    {
        if (Vector3.Distance(transform.position, waypointList[waypointIndex].transform.position) < deltaPositionWaypoint)
        {
            if (waypointIndex == waypointList.Count - 1)
            {
                waypointIndex = 0;
            }
            else
            {
                ++waypointIndex;
            }
            agent.destination = waypointList[waypointIndex].transform.position;
        }
    }

}

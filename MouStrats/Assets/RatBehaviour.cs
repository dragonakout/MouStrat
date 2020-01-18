using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RatBehaviour : MonoBehaviour
{
    NavMeshAgent agent;

    [SerializeField] float deltaPositionWaypoint;
    [SerializeField] float passiveSpeed;
    [SerializeField] float wanderRadius;
    [SerializeField] float viewDistance;
    [SerializeField] float fov;

    public GameObject player;
    bool isPassive;
    bool isDetecting;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        isPassive = true;
        Wander();
    }

    // Update is called once per frame
    void Update()
    {
        SearchForPlayer();
        if(isPassive && Vector3.Distance(transform.position, agent.destination) < deltaPositionWaypoint)
        {
            Wander();
        }
    }

    //se promene de facon aleatoire
    public void Wander()
    {
        Vector3 wanderPoint = RandomWanderPoint();
        if (Vector3.Distance(transform.position, wanderPoint) < 2f)
        {
            wanderPoint = RandomWanderPoint();
        }
        else
        {
            agent.SetDestination(wanderPoint);
        }
    }

    //set la destination aleatoire du wander
    public Vector3 RandomWanderPoint()
    {
        Vector3 randomPoint = (Random.insideUnitSphere * wanderRadius) + transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomPoint, out navHit, wanderRadius, -1);
        return new Vector3(navHit.position.x, transform.position.y, navHit.position.z);
    }

    public void SearchForPlayer()
    {
        if (Vector3.Angle(Vector3.forward, transform.InverseTransformPoint(player.transform.position)) < fov / 2)
        {
            Debug.Log(4);
            if (Vector3.Distance(player.transform.position, transform.position) < viewDistance)
            {
                Debug.Log(3);
                RaycastHit hit;
                if (!Physics.Linecast(transform.position, player.transform.position, out hit, 8))
                {
                    Debug.Log(1);
                    AggressiveMode();
                }
                else
                {
                    isDetecting = false;
                }
            }
            else
            {
                isDetecting = false;
            }
        }
        else
        {
            isDetecting = false;
        }
    }
    void AggressiveMode()
    {
        isPassive = false;
        agent.destination = player.transform.position;

       /*if (!isDetecting)
        {
            loseTimer += Time.deltaTime;
            if (loseTimer >= loseThreshold)
            {
                isPassive = true;
                agent.speed = passiveSpeed;
                loseTimer = 0;
                agent.destination = waypointList[waypointIndex].transform.position;
            }
        }*/
    }
}

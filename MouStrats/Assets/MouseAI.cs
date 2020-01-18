using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MouseAI : MonoBehaviour
{
    public  NavMeshAgent agent;

    public MouseManager mouseManager;

    Vector3 offSetPosition;

    void Start()
    {
        offSetPosition = -transform.forward * 3;
    }


    
    void Update()
    {

        agent.SetDestination(mouseManager.currentMouse.transform.position + offSetPosition);
        if (Vector3.Distance(gameObject.transform.position, mouseManager.currentMouse.transform.position) < 4f)
        {
            agent.SetDestination(transform.position);
        }
        else
        {
            agent.SetDestination(mouseManager.currentMouse.transform.position + offSetPosition);
        }

        if (mouseManager.currentMouse.isRunning)
        {
            agent.speed = 15;
        }
        else
        {
            agent.speed = 7;
        }
    }

}

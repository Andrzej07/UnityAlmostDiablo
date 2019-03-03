using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveOrder : IOrder
{
    NavMeshAgent agent;
    Vector3 destination;
    bool destinationReached = false;

    public MoveOrder(NavMeshAgent agent, Vector3 destination)
    {
        this.agent = agent;
        this.destination = destination;
    }

    public bool Finished
    {
        get
        {
            return destinationReached;
        }
    }

    public bool Interruptible
    {
        get
        {
            return true;
        }
    }
    public void Perform()
    {
        if ((agent.destination == destination && agent.remainingDistance <= agent.stoppingDistance) || destinationReached)
        {
            Debug.Log("Destination reached");
            agent.isStopped = true;
            destinationReached = true;
        }
        else
        {
            agent.isStopped = false;
            agent.destination = destination;
            destination = agent.destination;
            agent.stoppingDistance = 0;
        }
    }

}

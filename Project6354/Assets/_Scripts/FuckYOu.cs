using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FuckYOu : MonoBehaviour
{
    void Start()
    {
        GameObject defendPoint; 
	    NavMeshAgent agent;
        defendPoint = GameObject.FindWithTag("Defend Point");
		agent = GetComponent<NavMeshAgent>();
		agent.SetDestination(defendPoint.transform.position);
    }
}

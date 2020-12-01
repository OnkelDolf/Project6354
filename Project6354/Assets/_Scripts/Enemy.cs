using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
	public int damage = 10;
	public float attackRange = 10f;

	private float t = 0;

	public GameObject defendPoint; 
	private GameObject gameMaster; // TODO(Dolf): Implement level and wave scaling.
	
	private NavMeshAgent agent;

	/////////////////////////////////LEGACY//////////////////////////////////
	private float refreshTimeDestination = 0.5f; // For oldEnemyManager();
	private float refreshTimeDistanceToDefendPoint = 0.4f; // For oldEnemyManager();
	private float timeDestination = 0f; // For oldEnemyManager();
	private float timeDistanceToDefendPoint = 0f; // For oldEnemyManager();
	
	private bool dead = false; // For oldEnemyManager();
	/////////////////////////////////////////////////////////////////////////

    // Start is called before the first frame update
    void Start()
    {
        defendPoint = GameObject.FindWithTag("Defend Point");
		agent = GetComponent<NavMeshAgent>();
		agent.SetDestination(defendPoint.transform.position);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		if(defendPoint !=  null)
		{
			enemyManager();
		}
    }

	private void enemyManager()
	{
		t += Time.deltaTime;
		if(t >= 1)
		{
			if(Vector3.Distance(transform.position, defendPoint.transform.position) <= attackRange)
			{
				if(defendPoint.GetComponent<HealthBuilding>().health <= damage)
				{
					dead = true;
					GameObject.FindWithTag("Game Master").GetComponent<GameMaster>().loose();
					Debug.Log(gameObject.name + " attacked defendPoint and reduced it's hp to less than or equal to 0");
					Destroy(gameObject);
				}
				else
				{
					defendPoint.GetComponent<HealthBuilding>().health -= damage;
					Debug.Log(gameObject.name + " attacked defendPoint and reduced it's hp to " + defendPoint.GetComponent<HealthBuilding>().health);
					dead = true;
					Destroy(gameObject);
				}
			}
		}
	}

	private void AttackDefendPoint()
	{
		if(defendPoint.GetComponent<HealthBuilding>().health <= damage)
		{
			dead = true;
			GameObject.FindWithTag("Game Master").GetComponent<GameMaster>().loose();
			Debug.Log(gameObject.name + " attacked defendPoint and reduced it's hp to less than or equal to 0");
			Destroy(gameObject);
		}
		else
		{
			defendPoint.GetComponent<HealthBuilding>().health -= damage;
			Debug.Log(gameObject.name + " attacked defendPoint and reduced it's hp to " + defendPoint.GetComponent<HealthBuilding>().health);
			dead = true;
			Destroy(gameObject);
		}
	}






















	/////////////////////////////////LEGACY//////////////////////////////////
	private void oldEnemyManager()
	{
		// Potential Optimization(Dolf): Change this to just have a public method which the building scripts runs once on every enemy everytime an object is placed.
		if(timeDistanceToDefendPoint > refreshTimeDistanceToDefendPoint)
		{
			// Debug.Log("Distance from enemy to defendPoint: " + Vector3.Distance(transform.position, defendPoint.transform.position));
			// Debug.Log(1 * attackRange > Vector3.Distance(transform.position, defendPoint.transform.position));
			if(attackRange > Vector3.Distance(transform.position, defendPoint.transform.position))
			{
				// Debug.Log("Enemy is within range");
				AttackDefendPoint();
			}
		}
		if(timeDestination > refreshTimeDestination) 
		{
			if(!dead)
			{
				// Is this check necessary after adding the defendPoint != null in FixedUpdate?
				agent.SetDestination(defendPoint.transform.position);
			}
		}

		timeDistanceToDefendPoint += Time.deltaTime;
		timeDestination += Time.deltaTime;
	}
	/////////////////////////////////////////////////////////////////////////
}
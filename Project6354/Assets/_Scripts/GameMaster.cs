using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
	public bool defendPointPlaced = false;

	[SerializeField] private GameObject buildParent;
	[SerializeField] private GameObject defendPointPrefab;
	private GameObject defendPointUI;
	private GameObject defendPoint;

	private HealthBuilding defendPointHealth;
	
	private Click cc;
	
	private Quaternion rotation;

	void Start()
	{
		cc = Camera.main.GetComponent<Click>();
		defendPointUI = GameObject.FindWithTag("Defend Point UI");
		defendPointUI.SetActive(false);
	}

    // Update is called once per frame
    void FixedUpdate()
    {
		//Potential rework(Dolf): Rework it so instead of health being checked every 16ms make it so the damage dealer checks when damage is dealt and then executes loose(); if defendPoint health is 0 or bellow. This would save on performance.
		if(defendPoint != null && defendPointHealth.health < 0)
		{
			loose();
		}
		
	}

	public void spawnDefendPoint()
	{
		if (cc.selectedObjects.Count > 0)
        {
            foreach (GameObject selected in cc.selectedObjects)
            {
                if (selected.CompareTag("Node")  && selected.GetComponent<Clickable>().built == false)
                {
                    Vector3 offset = new Vector3(0, 6.5f, 0);
					Instantiate(defendPointPrefab, offset + selected.transform.position, rotation, buildParent.transform);
					
					defendPointPlaced = true;
					defendPointUI.SetActive(false);
					defendPoint = GameObject.FindWithTag("Defend Point");
					defendPointHealth = defendPoint.GetComponent<HealthBuilding>();

					Debug.Log("'" + defendPoint.name + "'" + " Has been spawned!");

					return;
                }
                else
                {
                    Debug.Log("Invalid selected object");
                }
				
            }
        }
	}

	public void enableSpawnDefendPointUI()
	{
		defendPointUI.SetActive(true);
	}

	private void loose()
	{
		Destroy(defendPoint);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
	public bool defendPointPlaced = false;
	public bool startWave = false;
	
	[SerializeField] private int level = 1;
	[SerializeField] private int wave = 1;
	[SerializeField] private int maxWave = 5;
	[SerializeField] private int startingEnemies = 20;

	private int currentEnemies;
	private int maxLevel;
	private int aiSpawner;

	[SerializeField] private float enemySpawnPerSecond = 1f;
	[SerializeField] private float enemySpawnTimer = 0f;

	[SerializeField] private GameObject enemyPrefab;
	[SerializeField] private GameObject startWaveButton;
	[SerializeField] private GameObject buildParent;
	[SerializeField] private GameObject defendPointPrefab;
	private GameObject defendPointUI;
	private GameObject defendPoint;
	[SerializeField] private GameObject[] aiSpawnPoints;
	[SerializeField] private GameObject enemyParent;

	private HealthBuilding defendPointHealth;
	
	private Click cc;
	
	private Quaternion rotation;

	private System.Random rand;

	void Start()
	{
		cc = Camera.main.GetComponent<Click>();
		defendPointUI = GameObject.FindWithTag("Defend Point UI");
		defendPointUI.SetActive(false);
		startWaveButton.SetActive(false);
		rand = new System.Random();
	}

    // Update is called once per frame
    void FixedUpdate()
    {
		if(startWave == true)
		{
			/*
			System.Random rand = new System.Random();
			int aiSpawner = rand.Next(0, aiSpawnPoints.Length);
			*/

			// TODO(Dolf): Start doing shit.
			enemySpawnTimer += Time.deltaTime;
			if(enemySpawnTimer >= 1 / enemySpawnPerSecond && currentEnemies != 0 && defendPoint != null)
			{
				spawnEnemy(aiSpawnPoints[aiSpawner].transform.position);

				currentEnemies--;
				enemySpawnTimer = 0;
			}
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

					selected.GetComponent<Clickable>().built = true;
					
					Debug.Log("'" + defendPoint.name + "'" + " Has been spawned!");

					maxLevel = transform.childCount;

					aiSpawnPoints = new GameObject[maxLevel];

					for(int i = 0; i < maxLevel; i++)
					{
						aiSpawnPoints[i] = transform.GetChild(i).gameObject;
					}

					startWaveButton.SetActive(true);

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

	public void startCurrentWave()
	{
		startWaveButton.SetActive(false);
		currentEnemies = startingEnemies * wave * level;
		startWave = true;

		aiSpawner = rand.Next(0, aiSpawnPoints.Length);
		Debug.Log(aiSpawner);
	}

	private void endCurrentWave()
	{
		startWaveButton.SetActive(true);
		startWave = false;
	}

	private void spawnEnemy(Vector3 spawnPosition)
	{
		//Instantiate(enemyPrefab, );
		Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, enemyParent.transform);
	}
	
	public void loose()
	{
		Destroy(defendPoint);
	}
}

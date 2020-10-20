using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateLevel : MonoBehaviour
{
    [SerializeField] private InputField generateX;
    [SerializeField] private InputField generateZ;

	[SerializeField] private int maxLevels = 3;
	private int test;

    public GameObject nodePrefab; // TODO(Dolf): Make all of these prefabs private but exposed to the editor. 
    public GameObject nodeParent;
    public GameObject outlinePrefab;
	public GameObject aiSpawnPrefab;
	public GameObject aiSpawnParent;
    public GameObject background;
    private Quaternion rotation;
    
    [SerializeField] private int maxAllowedGridSize = 30;
    [SerializeField] private int minAllowedGridSize = 5;

    [SerializeField] private int outlineOffsetX = 1;
    [SerializeField] private int outlineOffsetZ = 1;

    public void generateLevel()
    {
        // Initialize and convert the text in the input field to integers.
        int _generateX = int.Parse(generateX.text);
        int _generateZ = int.Parse(generateZ.text);
        
        
        if (_generateX <= maxAllowedGridSize && _generateZ <= maxAllowedGridSize && _generateX >= minAllowedGridSize && _generateZ >= minAllowedGridSize && generateX.text != null && generateZ.text != null)
        {
			background.SetActive(false);
			aiSpawnParent.GetComponent<GameMaster>().enableSpawnDefendPointUI();

            // Generate the main platform
            for (int i = 0; i < _generateX; i++)
            {
                for (int j = 0; j < _generateZ; j++)
                {
                    Vector3 pos = new Vector3(i * 10, 0, j * 10);
                    Instantiate(nodePrefab, pos, rotation, nodeParent.transform);
                }
            }
            
            // Generate the main platforms outline
            for (int i = 0; i <= _generateX + 1; i++)
            {
                for (int j = 0; j <= _generateZ + 1; j++)
                {
                    if (i == 0 || i == _generateX + 1 || j == 0 || j == _generateZ + 1)
                    {
                        Vector3 pos = new Vector3(i * 10 - outlineOffsetX * 10, 0, j * 10 - outlineOffsetZ * 10);
                        Instantiate(outlinePrefab, pos, rotation, nodeParent.transform);
                    }
                }
            }
			
			// Generate AI spawn points
			for (int i = 0; i <= _generateX + 1; i++)
            {
                for (int j = 0; j <= _generateZ + 1; j++)
                {
					// TODO(Dolf): Make an equation for this instead of having 4 else if statements.
                    if (i == _generateX / 2 && j == 0)
                    {
						Vector3 pos = new Vector3(i * 10, 6f, j * 10);
						GameObject temp = Instantiate(aiSpawnPrefab, pos, rotation, aiSpawnParent.transform);
						temp.name = "AISpawnPoint Clone (" + i + ", " + j + ")";
                    }
					else if(i == _generateX / 2 && j == _generateZ - 1)
					{
						Vector3 pos = new Vector3(i * 10, 6f, j * 10);
						GameObject temp = Instantiate(aiSpawnPrefab, pos, rotation, aiSpawnParent.transform);
						temp.name = "AISpawnPoint Clone (" + i + ", " + j + ")";
					}
					else if(i == 0 && j == _generateZ / 2)
					{
						Vector3 pos = new Vector3(i * 10, 6f, j * 10);
						GameObject temp = Instantiate(aiSpawnPrefab, pos, rotation, aiSpawnParent.transform);
						temp.name = "AISpawnPoint Clone (" + i + ", " + j + ")";
					}
					else if(i == _generateX - 1 && j == _generateZ / 2)
					{
						Vector3 pos = new Vector3(i * 10, 6f, j * 10);
						GameObject temp = Instantiate(aiSpawnPrefab, pos, rotation, aiSpawnParent.transform);
						temp.name = "AISpawnPoint Clone (" + i + ", " + j + ")";
					}
                }
            }

            GameObject.FindWithTag("Level Generation UI").SetActive(false);
            GetComponent<CameraController>().paused = false;
        }
        else
        {
            Debug.Log("CreateLevel.cs: Size input error.");
        }
    }
}

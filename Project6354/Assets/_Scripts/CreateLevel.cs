using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateLevel : MonoBehaviour
{
    public InputField generateX;
    public InputField generateZ;

    public GameObject nodePrefab;
    public GameObject nodeParent;
    public Quaternion rotation;
    
    public int maxAllowedGridSize = 30;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            generateLevel();
        }
    }

    public void generateLevel()
    {
        if (int.Parse(generateX.text) <= maxAllowedGridSize && int.Parse(generateZ.text) <= maxAllowedGridSize)
        {
            for (int i = 0; i < int.Parse(generateX.text); i++)
            {
                for (int j = 0; j < int.Parse(generateZ.text); j++)
                {
                    Vector3 pos = new Vector3(i * 10, 0, j * 10);
                    Instantiate(nodePrefab, pos, rotation, nodeParent.transform);
                }
            }
        }
        else
        {
            Debug.Log("Gone over max grid size");
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Build : MonoBehaviour
{
    private int moneyGoBrrrrr = 1000;
    [SerializeField] private GameObject buildParent;
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject moneyCounter;
    private Quaternion rotation;
    
    private Click cc;
    
    private void Start()
    {
        cc = Camera.main.GetComponent<Click>();
        moneyCounter.GetComponent<UnityEngine.UI.Text>().text = "Money: " + moneyGoBrrrrr.ToString();
    }

    /*
    // TODO: Implement hotkeys for building shit.
    private void FixedUpdate()
    {
        throw new NotImplementedException();
    }
    */

    public void BuildWall()
    {
        if (cc.selectedObjects.Count > 0)
        {
            foreach (GameObject selected in cc.selectedObjects)
            {
                if (selected.CompareTag("Node")  && selected.GetComponent<Clickable>().built == false  && moneyGoBrrrrr >= 100)
                {
                    moneyGoBrrrrr -= 100;
                    Vector3 pos = new Vector3(0, 7.5f, 0);
                    Instantiate(wallPrefab, pos + selected.transform.position, rotation, buildParent.transform);
                }
                else
                {
                    Debug.Log("Invalid selected object");
                }
            }
        }

        moneyCounter.GetComponent<UnityEngine.UI.Text>().text = "Money: " + moneyGoBrrrrr.ToString();
    }

    public void Delete()
    {
        List<GameObject> selectedObjects = cc.selectedObjects;
        foreach (GameObject select in selectedObjects.ToList())
        {
            if (select.CompareTag("Wall")) // TODO: Add other turrets
            {
                selectedObjects.Remove(select);
                Destroy(select);
            }
        }

        cc.selectedObjects = selectedObjects;
    }
}

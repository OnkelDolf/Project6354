using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{
    private int moneyGoBrrrrr = 1000;

    private Click cc;
    
    private void Start()
    {
        cc = Camera.main.GetComponent<Click>();
    }

    public void BuildWall()
    {
        if (GetComponent<Click>().selectedObjects.Count > 0)
        {
            foreach (GameObject selected in cc.selectedObjects)
            {
                Instantiate()
            }
        }
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBuilding : MonoBehaviour
{
	public int health = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void Die()
	{
		Debug.Log(gameObject.name + " died");
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class TowerDPS : MonoBehaviour
{
    public int level = 1;
    private int damage = 10;
    
    [SerializeField] private float t = 0;
    private float fireRate = 3;
    [SerializeField] private List<GameObject> targets = new List<GameObject>();
    public bool targetsInRange = false;

    private void FixedUpdate()
    {
        if (t > fireRate && targetsInRange && targets.Count > 0)
        {
            //Scan();
			targets.RemoveAll(item => item == null);
			targets = targets.OrderBy(x => Vector2.Distance(this.transform.position,x.transform.position)).ToList();
			if(targets[0] != null)
			{
				Shoot(targets[0].transform.gameObject); // Shoots target
				t = 0;
			}
        }

        t += Time.deltaTime;
    }

    private void Shoot(GameObject target)
    {
		if(target != null)
		{
			Debug.Log("Target is not dead");
			target.GetComponent<Health>().Damage(damage * level, gameObject);
		}
		else
		{
			Debug.Log("Target is dead");
			targets.RemoveAll(item => item == null);
		}
    }

	public void noTargetCheck()
	{
		if(targets.Count == 0)
		{
			targetsInRange = false;
		}
	}

    public void removeFromList(GameObject obj)
    {
        targets.Remove(obj);
        targets = targets.OrderBy(x => Vector2.Distance(this.transform.position,x.transform.position)).ToList();
		if(targets.Count == 0)
		{
			targetsInRange = false;
		}
    }

    private void OnTriggerEnter(Collider other)
    {
		Debug.Log(other.name + " entered trigger");
        if (other.gameObject.CompareTag("Enemy"))
        {
            targets.Add(other.gameObject);
			targets.RemoveAll(item => item == null);
            targets = targets.OrderBy(x => Vector2.Distance(this.transform.position,x.transform.position)).ToList();
            Debug.Log("Object '" + other.name + "' entered trigger");
            targetsInRange = true;
        }
		else
		{
			Debug.Log(other.name);
		}
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
			targets.RemoveAll(item => item == null);
            targets.Remove(other.gameObject);
            targets = targets.OrderBy(x => Vector2.Distance(this.transform.position,x.transform.position)).ToList();
            Debug.Log("Object '" + other.name + "' left trigger");
            targetsInRange = false;
        }
    }
}

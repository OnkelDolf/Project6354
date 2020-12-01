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
    private bool targetsInRange = false;
    private void FixedUpdate()
    {
        if (t > fireRate && targetsInRange && targets.Count > 0)
        {
            Scan();
        }

        t += Time.deltaTime;
    }

    private void Scan()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, targets[0].transform.position, out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, targets[0].transform.position/* * hit.distance*/, Color.yellow);
            Debug.Log("Did Hit");

            Shoot(hit.transform.gameObject); // Shoots target
            t = 0;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
    }

    private void Shoot(GameObject target)
    {
        target.GetComponent<Health>().Damage(damage * level, gameObject);
    }

    public void removeFromList(GameObject obj)
    {
        targets.Remove(obj);
        targets = targets.OrderBy(x => Vector2.Distance(this.transform.position,x.transform.position)).ToList();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            targets.Add(other.gameObject);
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
            targets.Remove(other.gameObject);
            targets = targets.OrderBy(x => Vector2.Distance(this.transform.position,x.transform.position)).ToList();
            Debug.Log("Object '" + other.name + "' left trigger");
            targetsInRange = false;
        }
    }
}

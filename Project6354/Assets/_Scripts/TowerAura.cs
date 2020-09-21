using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerAura : MonoBehaviour
{
    public int level = 1;
    private int damage = 1;
    
    private float t = 0;
    private float fireRate = 1;
    private List<GameObject> targets = new List<GameObject>();
    private bool targetsInRange = false;
    private void FixedUpdate()
    {
        if (t > fireRate && targetsInRange && targets.Count > 0)
        {
            t = 0;
            foreach (GameObject select in targets) // TODO: Fix the error that is caused here.
            {
                select.GetComponent<Health>().Damage(damage * level, gameObject);
            }
        }
        
        t += Time.deltaTime;
    }

    public void removeFromList(GameObject obj)
    {
        targets.Remove(obj);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            targets.Add(other.gameObject);
            Debug.Log("Object '" + other.name + "' entered trigger");
            targetsInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            targets.Remove(other.gameObject);
            Debug.Log("Object '" + other.name + "' left trigger");
            targetsInRange = false;
        }
    }
}

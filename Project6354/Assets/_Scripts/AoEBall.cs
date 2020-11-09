using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AoEBall : MonoBehaviour
{
    public int level = 1;
    private int damage = 5;
    private float damageFallOff = 1.4f;
    [SerializeField]private int force = 10;

    private List<GameObject> targets = new List<GameObject>();

    private void Awake()
    {
        level = GetComponentInParent<TowerAoE>().level;
        
        //transform.LookAt(GetComponentInParent<TowerAoE>().oldTarget.transform);

        GetComponent<Rigidbody>().AddForce((GetComponentInParent<TowerAoE>().oldTarget.transform.position - transform.position) * force);
    }

    private void OnCollisionEnter(Collision other)
    {
        //throw new NotImplementedException();
        if (targets.Count > 0)
        {
            foreach (GameObject select in targets)
            {
                int distanceBetweenObject = Convert.ToInt32(Vector2.Distance(transform.position, select.transform.position));
                distanceBetweenObject = Convert.ToInt32(Math.Pow(Convert.ToDouble(distanceBetweenObject), 1.6));
                
				if(select.GetComponent<Health>().health >= (damage * level - distanceBetweenObject * Convert.ToInt32(distanceBetweenObject <= 5)))
				{
					GetComponentInParent<TowerAoE>().removeFromList(select);
				}

                select.GetComponent<Health>().Damage((damage * level - distanceBetweenObject * Convert.ToInt32(distanceBetweenObject <= 5)), gameObject);
            }
        }
        else
        {
            Debug.Log("No objects in targets[]");
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            targets.Add(other.gameObject);
            //targets = targets.OrderBy(x => Vector2.Distance(this.transform.position,x.transform.position)).ToList();
            Debug.Log("Object '" + other.name + "' entered AoE ball trigger");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            targets.Remove(other.gameObject);
            //targets = targets.OrderBy(x => Vector2.Distance(this.transform.position,x.transform.position)).ToList();
            Debug.Log("Object '" + other.name + "' left trigger");
        }
    }
}

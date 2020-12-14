using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 10;

    public void Damage(int damage, GameObject shooter)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
			
			if (CompareTag("Tower DPS"))
        	{
				GetComponent<TowerDPS>().noTargetCheck();
        	}
        	else if (CompareTag("Tower AoE"))
        	{
				GetComponent<TowerDPS>().noTargetCheck();
        	}
        	else if (CompareTag("Tower Aura"))
        	{
				GetComponent<TowerDPS>().noTargetCheck();
        	}
        }
    }

    private void Die()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        /*
        if (CompareTag("Enemy"))
        {
            // TODO: Spawn blood splatter effect.
        }
        else if (CompareTag("Wall"))
        {
            // TODO: Spawn wall break effect.
        }
        else if (CompareTag("DefendPoint"))
        {
            // TODO: End Game
        }
        */
        Debug.Log("'" + name + "'" + "Died");
        Destroy(gameObject);
    }
}

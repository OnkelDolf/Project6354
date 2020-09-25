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
            if (shooter.CompareTag("Tower DPS"))
            {
                shooter.GetComponent<TowerDPS>().removeFromList(this.gameObject);
            }
            if (shooter.CompareTag("Tower Aura"))
            {
                Debug.Log("Removed object from list");
                shooter.GetComponent<TowerAura>().removeFromList(this.gameObject);
            }
            if (shooter.CompareTag("Tower AoE"))
            {
                shooter.GetComponent<TowerAura>().removeFromList(this.gameObject);
            }
            
            Die();
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

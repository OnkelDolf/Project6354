using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawnPointManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            Debug.Log("Did Hit: " + hit.collider.gameObject.name);
			hit.collider.gameObject.GetComponent<Clickable>().built = true;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

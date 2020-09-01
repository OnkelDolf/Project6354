using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterController : MonoBehaviour
{
    private NavMeshAgent agent;

    public Camera camera;

    private float temp;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            
        }
    }

    public void goTo(RaycastHit hit)
    {
        /*
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("Raycast hit point:");
            Debug.Log(hit.point.ToString());
            agent.destination = hit.point;
        }
        */

        Debug.Log(hit.point.ToString());
        agent.destination = hit.point;
    }
}

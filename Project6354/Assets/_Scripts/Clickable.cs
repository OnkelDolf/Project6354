using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : MonoBehaviour
{
    [SerializeField]
    private Material top;
    [SerializeField]
    private Material green;

    [HideInInspector]
    public bool selected = false;

    private MeshRenderer myRend;

    // Start is called before the first frame update
    void Start()
    {
        myRend = GetComponent<MeshRenderer>();
        Camera.main.gameObject.GetComponent<Click>().selectableObjects.Add(this.gameObject);
        Clicked();
    }

    public void Clicked() 
    {
        Debug.Log("Clicked");
        if (selected == true)
        {
            myRend.material = green;
        }
        else 
        {
            myRend.material = top;
        }
    }
}

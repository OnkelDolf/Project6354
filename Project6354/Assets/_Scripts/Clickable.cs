using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : MonoBehaviour
{
    [SerializeField]
    private Material notSelected;
    [SerializeField]
    private Material selected;

    [HideInInspector]
    public bool isSelected = false;
    public bool built = false;

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
        if (isSelected == true)
        {
            var mats = myRend.materials;
            mats[0] = selected;
            myRend.materials = mats;
        }
        else 
        {
            var mats = myRend.materials;
            mats[0] = notSelected;
            myRend.materials = mats;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Click : MonoBehaviour
{
    [SerializeField]
    private LayerMask clickablesLayer;
    
    [HideInInspector]
    public List<GameObject> selectableObjects;
    public List<GameObject> selectedObjects;

    private Vector3 startPos;
    private Vector3 endPos;

    // Start is called before the first frame update
    void Awake()
    {
        selectedObjects = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) 
        {
            //startPos = new Vector3();
            startPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            RaycastHit rayHit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit, Mathf.Infinity, clickablesLayer))
            {
                Clickable clickableScript = rayHit.collider.GetComponent<Clickable>();

                if (Input.GetKey("left shift"))
                {
                    if (clickableScript.isSelected == false)
                    {
                        selectedObjects.Add(rayHit.collider.gameObject);
                        clickableScript.isSelected = true;
                        clickableScript.Clicked();
                    }
                    else
                    {
                        selectedObjects.Remove(rayHit.collider.gameObject);
                        clickableScript.isSelected = false;
                        clickableScript.Clicked();
                    }
                }
                else
                {
                    ClearSelected();

                    selectedObjects.Add(rayHit.collider.gameObject);
                    clickableScript.isSelected = true;
                    clickableScript.Clicked();
                }
            }
            else 
            {
                foreach (GameObject obj in selectedObjects)
                {
                    obj.GetComponent<Clickable>().isSelected = false;
                    obj.GetComponent<Clickable>().Clicked();
                }

                selectedObjects.Clear();
            }
        }

        if (Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject()) 
        {
            endPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            if (startPos != endPos) 
            {
                SelectObjects();
            }
        }
    }

    private void SelectObjects() 
    {
        List<GameObject> remObjects = new List<GameObject>();

        if (Input.GetKey("left shift") == false) 
        {
            ClearSelected();
        }

        Rect selectRect = new Rect(startPos.x, startPos.y, endPos.x - startPos.x, endPos.y - startPos.y);

        foreach (GameObject selectObject in selectableObjects) 
        {
            if (selectObject != null)
            {
                if (selectRect.Contains(Camera.main.WorldToViewportPoint(selectObject.transform.position), true))
                {
                    selectedObjects.Add(selectObject);
                    selectObject.GetComponent<Clickable>().isSelected = true;
                    selectObject.GetComponent<Clickable>().Clicked();
                }
            }
            else
            {
                remObjects.Add(selectObject);
            }
        }

        if (remObjects.Count > 0) 
        {
            foreach (GameObject rem in remObjects) 
            {
                selectableObjects.Remove(rem);
            }

            remObjects.Clear();
        }
    }
    private void ClearSelected() 
    {
        if (selectedObjects.Count > 0)
        {
            foreach (GameObject obj in selectedObjects)
            {
                obj.GetComponent<Clickable>().isSelected = false;
                obj.GetComponent<Clickable>().Clicked();
            }

            selectedObjects.Clear();
        }
    }
}

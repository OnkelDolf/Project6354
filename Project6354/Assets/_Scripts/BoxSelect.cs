using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSelect : MonoBehaviour
{
    [SerializeField]
    private RectTransform selectBoxImage;

    Vector3 startPos;
    Vector3 endPos;

    // Start is called before the first frame update
    void Start()
    {
        selectBoxImage.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: Box select in Click.cs needs fixing
        /*
        switch (GetComponent<CameraController>().paused)
        {
            case true:
                
                break;
            case false:
                _BoxSelect();
                break;
        }
        */
    }

    private void _BoxSelect()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            RaycastHit rayHit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit, Mathf.Infinity)) 
            {
                startPos = rayHit.point;
            }
        }

        if (Input.GetMouseButtonUp(0)) 
        {
            selectBoxImage.gameObject.SetActive(false);
        }

        if (Input.GetMouseButton(0)) 
        {
            if (!selectBoxImage.gameObject.activeInHierarchy) 
            {
                selectBoxImage.gameObject.SetActive(true);
            }

            endPos = Input.mousePosition;

            Vector3 boxStart = Camera.main.WorldToScreenPoint(startPos);
            boxStart.z = 0f;


            float sizeX = Mathf.Abs(boxStart.x - endPos.x);
            float sizeY = Mathf.Abs(boxStart.y - endPos.y);

            selectBoxImage.sizeDelta = new Vector2(sizeX, sizeY);

            Vector3 centrePos = (boxStart + endPos) / 2f;

            selectBoxImage.position = centrePos;

        }
    }
}

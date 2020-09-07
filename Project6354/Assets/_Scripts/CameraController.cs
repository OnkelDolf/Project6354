using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 100f; //Determines how fast the camera moves.
    public float scrollModifier = 5f; //Determines how big of an increase the speed of scroll should be. Scroll speed = Input.GetAxis("Mouse ScrollWheel") * scrollModifier * speed.

    public float horizontalMouseSpeed = 5f; //Assigns the horizontal sensitivity. Used to set horizontal mouse sensitivity.
    //public float verticalMouseSpeed = 2f; //Assigns the vertical sensitivity. Used to set vertical mouse sensitivity.
    private float yaw = 0f; //Assigns the yaw to 0. Used to yaw the camera.
    private float pitch = 50f; //Assigns the pitch to 50. Used to pitch the camera.

    private float cameraHeight; //Assigns the camera height. Used to store the camera height;
    [SerializeField]
    private float maxCameraHeight = 30; //Assigns the max camera height to 30. Used to make sure the camera Y position isn't to high.
    [SerializeField]
    private float defaultCameraHeight = 10; //Assigns the default cameraHeight to 10. Used when starting cameraHeight is higher than the maxCameraHeight.

    private Rigidbody rb; //Assigns the rigidbody class to rb.

    public bool paused = true;

    private GameObject buildUI;
    private bool buidUIActive;
    
    //Start is called before the first frame update
    void Start()
    {
        buildUI = GameObject.FindWithTag("Build Menu UI");
        buildUI.SetActive(false);
        
        rb = GetComponent<Rigidbody>(); //Assigns the component Rigidbody of the object to rb.
        
        if (transform.position.y <= maxCameraHeight)
        {
            cameraHeight = transform.position.y;
        }
        else
        {
            cameraHeight = defaultCameraHeight;
        }
    }

    //Update is called once per frame
    private void Update()
    {
        //TODO: Make this use switch statements instead of if.
        if (Input.GetButtonDown($"Open Buy Menu") && buidUIActive == false)
        {
            //Debug.Log("Enable Build UI");
            buildUI.SetActive(true);
            buidUIActive = true;
        }
        else if (Input.GetButtonDown($"Open Buy Menu") && buidUIActive == true)
        {
            //Debug.Log("Disable Build UI");
            buildUI.SetActive(false);
            buidUIActive = false;
        }
        
        switch (paused)
        {
            case false:
                cameraHeight += Input.GetAxis("Mouse ScrollWheel") * scrollModifier * Time.deltaTime * 120;
                transform.position = new Vector3(transform.position.x, cameraHeight, transform.position.z);
                break;
        }
    }

    //FixedUpdate is called 60 times per second
    void FixedUpdate()
    {
        switch (paused)
        {
            case false:
                CameraMovement();
                break;
        }
    }

    private void CameraMovement()
    {
        //Adds force depending on if you are pushing down the Vertical or Horizontal keys.
        rb.AddForce(transform.forward * (Input.GetAxis("Vertical") * speed));
        rb.AddForce(transform.right * (Input.GetAxis("Horizontal") * speed));

        //Turns the camera if are pushing the Turn keys. TODO: Redo this using branchless programming.
        if (Input.GetAxis($"Turn") < 0)
        {
            yaw -= horizontalMouseSpeed;
        }
        else if(Input.GetAxis($"Turn") > 0)
        {
            yaw += horizontalMouseSpeed;
        }

        transform.eulerAngles = new Vector3(pitch, yaw, transform.rotation.z);
    }
}

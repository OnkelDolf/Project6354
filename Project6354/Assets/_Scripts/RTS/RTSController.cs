using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSController : MonoBehaviour
{
    public float speed = 100f; //Determines how fast the camera moves.
    public float scrollModifier = 5f; //Determines how big of an increase the speed of scroll should be. Scroll speed = Input.GetAxis("Mouse ScrollWheel") * scrollModifier * speed.

    public float horizontalMouseSpeed = 20f; //Assigns the horizontal sensitivity. Used to set horizontal mouse sensitivity.
    //public float verticalMouseSpeed = 2f; //Assigns the vertical sensitivity. Used to set vertical mouse sensitivity.
    private float yaw = 0f; //Assigns the yaw to 0. Used to yaw the camera.
    private float pitch = 50f; //Assigns the pitch to 50. Used to pitch the camera.

    private float cameraHeight; //Assigns the camera height. Used to store the camera height;
    [SerializeField]
    private float maxCameraHeight = 30; //Assigns the max camera height to 30. Used to make sure the camera Y position isn't to high.
    [SerializeField]
    private float defaultCameraHeight = 10; //Assigns the default cameraHeight to 10. Used when starting cameraHeight is higher than the maxCameraHeight.

    private Rigidbody rb; //Assigns the rigidbody class to rb.

    // Start is called before the first frame update
    void Start()
    {
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

    // Update is called once per frame
    void Update()
    {
        CameraController();
    }

    private void CameraController()
    {
        rb.AddForce(transform.forward * Input.GetAxis("Vertical") * speed * Time.deltaTime * 120);
        rb.AddForce(transform.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime * 120);

        cameraHeight += Input.GetAxis("Mouse ScrollWheel") * scrollModifier * Time.deltaTime * 120;
        transform.position = new Vector3(transform.position.x, cameraHeight, transform.position.z);

        if (Input.GetKey(KeyCode.Q))
        {
            yaw -= horizontalMouseSpeed * Time.deltaTime * 120;
        }
        if (Input.GetKey(KeyCode.E))
        {
            yaw += horizontalMouseSpeed * Time.deltaTime * 120;
        }

        transform.eulerAngles = new Vector3(pitch, yaw, transform.rotation.z);
    }
}

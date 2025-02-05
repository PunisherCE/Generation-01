using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Maintains position offset while orbiting around target
public class OrbitCamera : MonoBehaviour
{
    [SerializeField] Transform target;

    public float rotSpeedY = 1.5f;
    public float rotSpeedX = 1.5f;

    private float maxVerticalAngle = 60f; // Maximum up and down rotation angle
    private float rotY;
    private float rotX;
    private Vector3 offset;

    // Use this for initialization
    void Start()
    {
        rotY = transform.eulerAngles.y;
        rotX = transform.eulerAngles.x;
        offset = target.position - transform.position;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Use Mouse input for both axes consistently and invert rotX
        rotY += Input.GetAxis("Mouse X") * rotSpeedY * 3;
        rotX += Input.GetAxis("Mouse Y") * rotSpeedX * 3;

        // Clamp the rotX value to prevent excessive vertical rotation
        rotX = Mathf.Clamp(rotX, -maxVerticalAngle, maxVerticalAngle);

        // Update rotation based on mouse input
        Quaternion rotation = Quaternion.Euler(-rotX, rotY, 0); // Invert rotX for reversing rotation
        Vector3 desiredPosition = target.position - (rotation * offset);

        // Handle camera collision using raycasting
        RaycastHit hit;
        if (Physics.Linecast(target.position, desiredPosition, out hit))
        {
            transform.position = hit.point; // Move the camera to the hit point to avoid passing through the object
        }
        else
        {
            transform.position = desiredPosition;
        }

        transform.LookAt(target);
    }
}

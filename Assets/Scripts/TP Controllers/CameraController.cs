using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    private float minYAngle = -30f;
    private float maxYAngle = 60f;
    private float distance = 8;
    private float sensitivity = 1000f;
    private float yaw = 0f;
    private float pitch = 0f;
    private float initialDistance;
    private Vector3 initialOffset;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        initialDistance = distance; // Store initial distance from editor
        initialOffset = transform.position - target.position; // Store initial vertical offset
    }

    void LateUpdate()
    {
        yaw += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        pitch -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, minYAngle, maxYAngle);

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 position = target.position + initialOffset.y * Vector3.up - rotation * Vector3.forward * initialDistance;

        transform.position = position;
        transform.LookAt(target);
    }
}

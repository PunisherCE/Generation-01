using UnityEngine.InputSystem;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public LayerMask collisionLayers;

    private float minYAngle = -30f;
    private float maxYAngle = 60f;
    private float distance = 8f;
    private float sensitivity = 20f;
    private float yaw = 0f;
    private float pitch = 0f;
    private Vector3 currentOffset;
    private float minDistance = 3f; // Minimum distance to maintain from the player

    void Start()
    {
        currentOffset = new Vector3(0f, 2f, -distance); // Adjusted to provide better starting height
    }

    void LateUpdate()
    {
        yaw += Mouse.current.delta.x.ReadValue() * sensitivity * Time.deltaTime;
        pitch -= Mouse.current.delta.y.ReadValue() * sensitivity * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, minYAngle, maxYAngle);

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 desiredPosition = target.position + rotation * currentOffset;

        Vector3 directionToCamera = (desiredPosition - target.position).normalized;
        float desiredDistance = currentOffset.magnitude;

        RaycastHit hit;
        if (Physics.SphereCast(target.position, 0.5f, directionToCamera, out hit, desiredDistance, collisionLayers))
        {
            float adjustedDistance = Mathf.Max(hit.distance - 0.2f, minDistance);
            transform.position = target.position + rotation * new Vector3(0f, 2f, -adjustedDistance);
        }
        else
        {
            transform.position = desiredPosition;
        }

        transform.LookAt(target);
    }
}
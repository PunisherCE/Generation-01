using UnityEngine;

public class PlayerController : MonoBehaviour
{

    float accelerate;
    float turn;
    float moveSpeed = 15f;
    float turnSpeed = 25f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        accelerate = Input.GetAxis("Vertical");
        turn = Input.GetAxis("Horizontal");

        if (accelerate != 0)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed * accelerate);
            transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * turn);
        }
    }
}

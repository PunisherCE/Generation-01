using UnityEngine;

public class PlayerController : MonoBehaviour
{

    float moveSpeed = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float accelerate = Input.GetAxis("Vertical");
        if (accelerate != 0)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed * accelerate); 
        }
    }
}

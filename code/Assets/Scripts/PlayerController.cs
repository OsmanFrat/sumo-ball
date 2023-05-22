using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private float speed = 5.0f;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");

        playerRb.AddForce(Vector3.forward * speed * forwardInput);

        float horizontalInput = Input.GetAxis("Horizontal");
        
        playerRb.AddForce(Vector3.right * horizontalInput * speed);
    }
}

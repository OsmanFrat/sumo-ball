using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private float speed = 5.0f;
    public bool hasPowerUp = false;
    private float powerUpStrength = 15.0f;
    public GameObject powerUpIndicator;
    private float rotatingPowerUp;

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

        powerUpIndicator.transform.position = transform.position  + new Vector3(0, -0.3f, 0);

        rotatingPowerUp++;
        if (rotatingPowerUp >= 2)
        {
            rotatingPowerUp = 0;
            powerUpIndicator.transform.Rotate(new Vector3(0, 3, 0));
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("PowerUp"))
        {
            hasPowerUp = true;
            powerUpIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerUpCountDownRoutine());
        }
    }

    IEnumerator PowerUpCountDownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerUp = false;
        powerUpIndicator.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
            
            Debug.Log("Collided with " + collision.gameObject.name + " with power up set to " + hasPowerUp);
            enemyRigidbody.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);
        }
        
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody playerRb;
    private float speed = 5.0f;
    public bool hasPowerUp = false;
    public bool gunPowerUp = false;
    private float powerUpStrength = 15.0f;
    public GameObject powerUpIndicator;
    public GameObject bullet;
    private float rotatingPowerUp;

    private float fireElapsedTime = 0;
    public float fireDelay = 0.3f;
    private float jumpPower = 15.0f;
    private float smashPower = 50.0f;
    private float smashRange = 12.0f;
    private bool smashEnabled;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        // Player movement
        float forwardInput = Input.GetAxis("Vertical");

        playerRb.AddForce(Vector3.forward * speed * forwardInput);

        float horizontalInput = Input.GetAxis("Horizontal");
        
        playerRb.AddForce(Vector3.right * horizontalInput * speed);

        // Power up position
        powerUpIndicator.transform.position = transform.position  + new Vector3(0, -0.3f, 0);

        // Power up rotation animation
        rotatingPowerUp++;
        if (rotatingPowerUp >= 2)
        {
            rotatingPowerUp = 0;
            powerUpIndicator.transform.Rotate(new Vector3(0, 3, 0));
        }
        
        fireElapsedTime += Time.deltaTime;
        if(Input.GetKey(KeyCode.Mouse0) && gunPowerUp == true && fireElapsedTime >= fireDelay)
        {
            fireElapsedTime = 0;
            shoot();
        }

        if(Input.GetKey(KeyCode.Space) && smashEnabled  && fireElapsedTime >= fireDelay)
        {
            fireElapsedTime = 0;
            StartCoroutine(jump());
            smash();
        }
        

    }

    IEnumerator jump()
    {
        playerRb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        yield return new WaitForSeconds(0.3f);
        playerRb.AddForce(Vector3.down * jumpPower *2, ForceMode.Impulse);

    }
    private void smash()
    {
        Enemy[] enemyList = FindObjectsOfType<Enemy>();

        foreach (Enemy enemy in enemyList)
        {

            Vector3 awayFromPlayer = enemy.gameObject.transform.position - transform.position;

            if (awayFromPlayer.magnitude <= smashRange)
            {

                float power = ( smashPower - ( (awayFromPlayer.magnitude / smashRange) * smashPower ) );
                
                Rigidbody enemyRb = enemy.gameObject.GetComponent<Rigidbody>();

                enemyRb.AddForce(awayFromPlayer.normalized * power, ForceMode.Impulse);

            }
        }
    }
    private void shoot()
    {
        //Get a list of all enemies
        Enemy[] enemyList = FindObjectsOfType<Enemy>();
        
        //Create a different bullet for each enemy
        foreach( Enemy enemy in enemyList)
        {
            Vector3 lookToEnemy = enemy.transform.position - transform.position;
            Vector3 starPos = transform.position + lookToEnemy.normalized + new Vector3(0, 1, 0);
            
            Quaternion rotate = Quaternion.LookRotation(lookToEnemy, Vector3.up) * Quaternion.Euler(90, 0, 0);

            // Create bullet and send enemy gameObject in bullet as a target
            Instantiate(bullet, starPos, rotate).GetComponent<Bullet>().target = enemy.gameObject;
        }  
    }

    // Gaining power up
    private void OnTriggerEnter(Collider other) 
    {
    
    if(other.CompareTag("PowerUp"))
        {
            hasPowerUp = true;
            powerUpIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerUpCountDownRoutine());
        }
        
    
        if(other.CompareTag("GunPowerUP"))
        {
            gunPowerUp = true;
            powerUpIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerUpCountDownRoutine());
        }
        if(other.CompareTag("SmashPowerUp"))
        {
            smashEnabled = true;
            powerUpIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerUpCountDownRoutine());
        }
    }

    // Power up countdown
    IEnumerator PowerUpCountDownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerUp = false;
        gunPowerUp = false;
        smashEnabled = false;
        powerUpIndicator.gameObject.SetActive(false);
    }

    // Power up
    private void OnCollisionEnter(Collision collision) 
    {
        if(collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
            
            Debug.Log("Collided with " + collision.gameObject.name + " with power up set to " + hasPowerUp);
            enemyRigidbody.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);
        }

    }
        
    
}


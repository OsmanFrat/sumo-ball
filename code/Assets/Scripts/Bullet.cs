using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject target;
    public float speed = 10.0f;
    public float speedRot = 10.0f;

    void Start()
    { }

    void Update()
    {

        //If the target is gone, destroy the bullet
        if (!target) { Destroy(gameObject); return; }
            
        //Turn and move the bullet toward the target
        Vector3 lookDirection = target.transform.position - transform.position;

        Quaternion rotateTarget = Quaternion.LookRotation(lookDirection, Vector3.up) * Quaternion.Euler(90.0f, 0, 0);
        Quaternion rotate = Quaternion.RotateTowards(transform.rotation, rotateTarget, speedRot);

        transform.rotation = rotate;


        transform.Translate(Vector3.up * Time.deltaTime * speed);

    }

    private void OnCollisionEnter(Collision collision)
    {
        //If a collision with an enemy, create an impulse hit on the enemy and destroy the bullet
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss"))
        {
            Destroy(gameObject);

            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;

            enemyRigidbody.AddForce(awayFromPlayer * 10.0f, ForceMode.Impulse);

        }
        //If colliding with anything other than other bullets, just destroy
        else if(!collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }
}

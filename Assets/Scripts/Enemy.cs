using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public bool multipleFirepoints = false; //If enabled, enemy will use all of its firepoints
    public bool homingBullets = false;  //The bullets enemy shoots slightly rotates towards player

    public float enemySpeed;
    public int enemyShootingInterval; //Time between bullet shots

    public Transform defaultFirepoint;
    public Transform midLeftFirepoint;
    public Transform midRightFirepoint;

    public Rigidbody rb;
    public float CurrentEnemySpeed;
    // Start is called before the first frame update
    void Start()
    {
       // rb.AddForce(new Vector3(0, 0, -enemySpeed), ForceMode.Force);
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rb.velocity.z < enemySpeed)
        {
            rb.AddForce(new Vector3(0, 0, -0.1f), ForceMode.Impulse);
        }
        else if (rb.velocity.z > enemySpeed)
        {
            rb.AddForce(new Vector3(0, 0, 0.1f), ForceMode.Impulse);
        }
    }
}

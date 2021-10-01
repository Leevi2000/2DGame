using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletHoming : EnemyBulletScript
{

    public GameObject player;
    public float turnSpeed = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
       
 
    }
  

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < 1)
        {
            CanRotate = false;
        }

        rb.velocity = transform.forward * bulletSpeed;
        if (CanRotate)
        {
            Vector3 x = player.transform.position - transform.position;
            x.Normalize();
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(x), turnSpeed * Time.deltaTime);
        }

        if (transform.position.z < -5)
        {
            Destroy(this.gameObject);
        }
 
    }
}

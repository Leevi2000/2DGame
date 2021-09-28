using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    public Rigidbody rb;
    public float bulletSpeed;
    public bool canRotate;
    public int damage;
    public bool CanRotate { get => canRotate; set => canRotate = value; }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.forward * bulletSpeed;

        if (transform.position.x > 0)
        {
            CanRotate = false;
        }

        if (transform.position.z < -10)
        {
            Destroy(this.gameObject);
        }
    }
}

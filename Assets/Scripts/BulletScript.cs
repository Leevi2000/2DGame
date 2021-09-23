using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public Rigidbody rb;
    public float bulletSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rb.transform.Rotate(0f, 90f, 90f);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.up * bulletSpeed;
    }
}

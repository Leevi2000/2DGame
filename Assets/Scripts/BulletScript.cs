using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public Rigidbody rb;
    public float bulletSpeed;
    public float turnSpeed;
    public bool canTurn;
    GameObject vihu;
    // Start is called before the first frame update
    void Start()
    {
        vihu = GameObject.FindGameObjectWithTag("Enemy");
        //rb.transform.Rotate(0f, 90f, 90f);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.forward * bulletSpeed;

        if (transform.position.z > vihu.transform.position.z)
        {
            canTurn = false;
        }

        if (canTurn)
        {
            Vector3 x = vihu.transform.position - transform.position;
            x.Normalize();
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(x), turnSpeed * Time.deltaTime);
        }

     
    }
}

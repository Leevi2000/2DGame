using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    public bool multipleFirepoints = false; //If enabled, enemy will use all of its firepoints
    public bool homingBullets = false;  //The bullets enemy shoots slightly rotates towards player

    public float enemySpeed;
    public float enemyFireRate; //Time between bullet shots
    private float nextFire = 0.5f;
    public float enemyHp;

    //--Firepoints--

    //Possible firepoints for the enemy to shoot from
    public Transform defaultFirepoint;
    public Transform midLeftFirepoint;
    public Transform midRightFirepoint;

    //Which firepoints will be used when shooting a bullet, default 1 = front firepoint
    int firePointMode = 1;

    public Rigidbody rb;

    //Bullet Prefabs 
    //Enemy Instantiates different bullets if homingBullets set to true
    public GameObject defaultBulletPrefab;
    public GameObject homingBulletPrefab;

    //
    GameObject selectedBullet;

    //To be able to make firerate
    bool allowFire = true;


    // Start is called before the first frame update
    void Start()
    {
       rb.AddForce(new Vector3(0, 0, -enemySpeed), ForceMode.Impulse);

        GetShootingType();
    }



    //Sets the right bullet and firepoints based on settings on the enemy
    void GetShootingType()
    {
        if (homingBullets)
        {
            selectedBullet = homingBulletPrefab;
        }
        else
        {
            selectedBullet = defaultBulletPrefab;
        }


        if (multipleFirepoints == true)
        {
            firePointMode = 2;
        }
    }


    void FixedUpdate()
    {
        AdjustToRightVelocity();
        if (allowFire)
        {
            StartCoroutine(Fire());
        }
        CheckHP();
    }
    void CheckHP()
    {
        if (enemyHp < 0 || enemyHp == 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       // if (other.CompareTag())
    }
    //void FireBullets()
    //{
    //    if (Time.time > nextFire)
    //    {
    //        nextFire = Time.time + enemyFireRate;
    //        Instantiate(selectedBullet, defaultFirepoint.position, defaultFirepoint.rotation);
    //    }

    //}
    IEnumerator Fire()
    {
        

        allowFire = false;
        if (firePointMode == 1)
        {
            Instantiate(selectedBullet, defaultFirepoint.position, defaultFirepoint.rotation);
        }
        else if (firePointMode == 2)
        {
            Instantiate(selectedBullet, defaultFirepoint.position, defaultFirepoint.rotation);
            Instantiate(selectedBullet, midLeftFirepoint.position, midLeftFirepoint.rotation);
            Instantiate(selectedBullet, midRightFirepoint.position, midRightFirepoint.rotation);
        }
        

        yield return new WaitForSeconds(enemyFireRate);
        allowFire = true;
    }
    void AdjustToRightVelocity()
    {
        //if (rb.velocity.z < enemySpeed)
        //{
        //    rb.AddForce(new Vector3(0, 0, -0.02f), ForceMode.Impulse);
        //}
        //else if (rb.velocity.z > enemySpeed)
        //{
        //    rb.AddForce(new Vector3(0, 0, 0.02f), ForceMode.Impulse);
        //}
        

    }



}

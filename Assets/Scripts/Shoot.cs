using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    public Transform firePoint;
    public GameObject luotiPrefab;
    public float playerFireRate;

    private bool canShoot = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
     
        if (canShoot)
            {
            StartCoroutine(Fire());
        }
            
        
    }
    IEnumerator Fire()
    {
        canShoot = false;
        Instantiate(luotiPrefab, firePoint.position, new Quaternion(firePoint.rotation.x, firePoint.rotation.y + Random.Range(-0.05f,0.05f), firePoint.rotation.z, firePoint.rotation.w));

        yield return new WaitForSeconds(playerFireRate);
        canShoot = true;
    }
    //void Fire()
    //{
    //    Instantiate(luotiPrefab, firePoint.position, firePoint.rotation);
    //}
}

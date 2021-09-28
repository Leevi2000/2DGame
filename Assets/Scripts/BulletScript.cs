using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public Rigidbody rb;
    public float bulletSpeed;
    public float turnSpeed;
    public bool canTurn;
    public int damage;

    GameObject vihu;

    GameObject selectedEnemy = new GameObject();
    GameObject closestEnemy = new GameObject();


    // Start is called before the first frame update
    void Start()
    {
        
        //rb.transform.Rotate(0f, 90f, 90f);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");

        rb.velocity = transform.forward * bulletSpeed;

            GameObject vihu = ClosestEnemy(enemyList);
       
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
        
            if (transform.position.z > 20)
        {
            Destroy(this.gameObject);
        }
    }
    GameObject ClosestEnemy(GameObject[] enemyArray)
    {
        
        bool firstEnemySet = false;
        foreach (GameObject enemy in enemyArray)
        {
            if (!firstEnemySet)
            {
                firstEnemySet = true;
                Debug.Log("EnemyWasNull");
                closestEnemy = enemy;
            }
          
            float enemyDist = CalcEnemyDist(enemy);
            float closestEnemyDistance = CalcEnemyDist(closestEnemy);
            if (enemyDist < closestEnemyDistance)
            {
                closestEnemy = enemy;
            }
            
          
          
        }
        selectedEnemy = closestEnemy;
        return selectedEnemy;
    }

    float CalcEnemyDist(GameObject obj)
    {
        float objDistance = Mathf.Sqrt((Mathf.Pow(obj.transform.position.x, 2) - Mathf.Pow(transform.position.x, 2)) + (Mathf.Pow(obj.transform.position.z, 2) - Mathf.Pow(transform.position.z, 2)));

        return objDistance;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    GameObject player;

    public Rigidbody rb;
    public float bulletSpeed;
    public float turnSpeed;
    public bool canTurn;
    public int damage;

    GameObject targetedEnemy;
    GameObject test;
    GameObject selectedEnemy;
    GameObject closestEnemy;

    bool canUpdateBulletTarget = true;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        targetedEnemy = ClosestEnemy(enemyList);

        //GameObject selectedEnemy = new GameObject();
        //GameObject closestEnemy = new GameObject();
        //rb.transform.Rotate(0f, 90f, 90f);
    }

    // Update is called once per frame
    void Update()
    {

        rb.velocity = transform.forward * bulletSpeed;
        //  GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        BulletMovement();

       
       
        //if (test.transform.position.z < vihu.transform.position.z)
        //{
        //    vihu = test;
        //}

        //if (transform.position.z > vihu.transform.position.z)
        //{
        //    canTurn = false;
        //}
        //else
        //{
        //    canTurn = true;
        //}

        //if (canTurn)
        //{
        //    Vector3 x = vihu.transform.position - transform.position;
        //    x.Normalize();
        //    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(x), turnSpeed * Time.deltaTime);
        //}

   

        //Destroy the bullet after reaching point 25 in z axle
        if (transform.position.z > 25)
        {
            Destroy(this.gameObject);
        }
    }

    GameObject ClosestEnemy(GameObject[] enemyArray)
    {

        bool firstEnemySet = false;
        foreach (GameObject enemy in enemyArray)
        {

            //If player bullet has surpassed enemy
            if (enemy.transform.position.z < transform.position.z)
            {

            }
            else
            {
                if (!firstEnemySet)
                {
                    firstEnemySet = true;

                    closestEnemy = enemy;
                }
                canTurn = true;
                float enemyDist = CalcEnemyDist(enemy);
                float closestEnemyDistance = CalcEnemyDist(closestEnemy);
                if (enemyDist < closestEnemyDistance)
                {
                    closestEnemy = enemy;
                }

            }
        }
        selectedEnemy = closestEnemy;
        return selectedEnemy;
    }

    void BulletMovement()
    {
        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestNearPlayer = ClosestEnemyNearPlayer(enemyList);
        GameObject closestNearBullet = ClosestEnemyNearBullet(enemyList);
        
        //If bullet is closer to enemy closest to player
        if(CalcEnemyDist(closestNearPlayer) < CalcEnemyDist(closestNearBullet))
        {
            //If bullet hasn't gone past enemy closest to player
            if (closestNearPlayer.transform.position.z > transform.position.z)
            {
                targetedEnemy = closestNearPlayer;
                Debug.Log("Targeted to nearest from player");
            }
            //
            else
            {
                Debug.Log("Targeted to nearest from bullet");
                targetedEnemy = closestNearBullet;
            }
            
        }
        else
        {
            Debug.Log("Targeted to nearest from bullet");
            targetedEnemy = closestNearBullet;
        }

        

        Debug.Log("Targeted enemy:" + targetedEnemy.transform.position.ToString());

        Debug.Log("Closest enemy near player: " + closestNearPlayer.transform.position.ToString());
        Debug.Log("Closest enemy near bullet: " + closestNearBullet.transform.position.ToString());


        //If bullet has gone past targeted enemy, disable bullet rotation
        if (transform.position.z > targetedEnemy.transform.position.z)
        {
            canTurn = false;
            targetedEnemy = closestNearBullet;
        }
        else //If target changes, and bullet hasn't gone past it yet, enable bullet rotation
        {
            canTurn = true;
        }

        //Rotate bullet towards targeted enemy
        if (canTurn)
        {
            Vector3 towardsTarget = targetedEnemy.transform.position - transform.position;
            towardsTarget.Normalize();
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(towardsTarget), turnSpeed * Time.deltaTime);
        }

    }
    GameObject ClosestEnemyNearPlayer(GameObject[] enemies)
    {
        // GameObject enemyToTest;
        GameObject nearestEnemy = enemies[0];
        foreach (GameObject enemyToTest in enemies)
        {
            if (CalcEnemyDist(enemyToTest, player) < CalcEnemyDist(nearestEnemy, player))
            {
                nearestEnemy = enemyToTest;
            }
        }
        return nearestEnemy; 
    }
    GameObject ClosestEnemyNearBullet(GameObject[] enemies)
    {
        GameObject nearestEnemy = enemies[0];
        foreach (GameObject enemyToTest in enemies)
        {
            if (CalcEnemyDist(enemyToTest) < CalcEnemyDist(nearestEnemy))
            {
                //If bullet hasn't gone past enemy position
                if (transform.position.z < enemyToTest.transform.position.z)
                {
                    
                    nearestEnemy = enemyToTest;
                }
                
            }
        }
        return nearestEnemy;
    }

    //float CalcEnemyDist(GameObject obj)
    //{
    //    float objDistance = Mathf.Sqrt((Mathf.Pow(transform.position.z, 2) - Mathf.Pow(obj.transform.position.z, 2)) + (Mathf.Pow(transform.position.z, 2) - Mathf.Pow(obj.transform.position.z, 2)));

    //    return objDistance;
    //}
    //float CalcEnemyDist(GameObject enemyObj, GameObject playerObj)
    //{
    //    float objDistance = Mathf.Sqrt((Mathf.Pow(playerObj.transform.position.x, 2) - Mathf.Pow(enemyObj.transform.position.x, 2)) + (Mathf.Pow(playerObj.transform.position.z, 2) - Mathf.Pow(enemyObj.transform.position.z, 2)));

    //    return objDistance;
    //}
    float CalcEnemyDist(GameObject obj)
    {
        float objDist = Mathf.Sqrt((Mathf.Pow(transform.position.x - obj.transform.position.x, 2) + Mathf.Pow(transform.position.z - obj.transform.position.z, 2)));

        return objDist;
    }
    float CalcEnemyDist(GameObject enemyObj, GameObject playerObj)
    {
        float objDist = Mathf.Sqrt((Mathf.Pow(playerObj.transform.position.x - enemyObj.transform.position.x, 2) + Mathf.Pow(playerObj.transform.position.z - enemyObj.transform.position.z, 2)));

        return objDist;
    }
}

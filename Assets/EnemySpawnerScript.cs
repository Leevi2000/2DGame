using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    //Enemies:
    public GameObject enemyLight1;
    public GameObject enemyLight2;
    public GameObject enemyLight3;
    private List<GameObject> lightEnemies = new List<GameObject>();

    public GameObject enemyMedium1;
    public GameObject enemyMedium2;
    public GameObject enemyMedium3;
    List<GameObject> mediumEnemies = new List<GameObject>();

    public GameObject enemyHeavy1;
    List<GameObject> heavyEnemies = new List<GameObject>();

    int spawnAreaWidth = 5;
    public bool randomCanSpawn = true;
    Vector3 spawnPos;
    int gameDiff;

    GameObject selectedEnemy = new GameObject();
    // Start is called before the first frame update
    void Start()
    {
     
        lightEnemies.Add(enemyLight1);
        lightEnemies.Add(enemyLight2);
        lightEnemies.Add(enemyLight3);

        mediumEnemies.Add(enemyMedium1);
        mediumEnemies.Add(enemyMedium2);
        mediumEnemies.Add(enemyMedium3);

        heavyEnemies.Add(enemyHeavy1);
    }

    // Update is called once per frame
    void Update()
    {
        spawnPos = new Vector3(Random.Range(-spawnAreaWidth, spawnAreaWidth), transform.position.y, transform.position.z);
        if (randomCanSpawn)
        {
            StartCoroutine(SpawnSingleEnemy());
        }
    }

    IEnumerator SpawnSingleEnemy()
    {
        randomCanSpawn = false;
    
        GameObject randomEnemy = selectEnemy();

        Instantiate(randomEnemy, spawnPos, transform.rotation);
        
        yield return new WaitForSeconds(Random.Range(3, 6));
       
        randomCanSpawn = true;
    }

    GameObject selectEnemy()
    {
        
        int enemylist = Random.Range(1, 3);
        int i;
        switch (enemylist)
        {
            case 1:
                i = Random.Range(0, lightEnemies.Count-1);
                selectedEnemy = lightEnemies[i];
                break;
            case 2:
                i = Random.Range(0, mediumEnemies.Count - 1);
                selectedEnemy = mediumEnemies[i];
                break;
            case 3:
                i = Random.Range(0, heavyEnemies.Count - 1);
                selectedEnemy = heavyEnemies[i];
                break;
        }
      
        return selectedEnemy;
    }
}

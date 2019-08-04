using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float SPAWN_PERIOD = 10.0f;
    public GameObject enemyPrefab;
    public PlayerController player;
    public GameObject topLeft;
    public GameObject bottomRight;
    float spawnTimer;
    int numEnemies;

    void Start()
    {
        spawnTimer = SPAWN_PERIOD;
        numEnemies = 1;
        Spawn();
        numEnemies++;
    }

    void Update()
    {
        if (spawnTimer <= 0)
        {
            for(int i = 0; i < numEnemies; i++) {
                Spawn();
            }
            numEnemies++;
            spawnTimer = SPAWN_PERIOD;
        }
        else
        {
            spawnTimer -= Time.deltaTime;
        }
    }

    void Spawn()
    {
        Vector3 topLeft = this.topLeft.transform.position;
        Vector3 bottomRight = this.bottomRight.transform.position;

        Vector3 position = topLeft + new Vector3(Random.value * (bottomRight.x - topLeft.x), Random.value * (bottomRight.y - topLeft.y));
        GameObject enemyObject = (GameObject) Instantiate(enemyPrefab);
        enemyObject.transform.position = position;
        EnemyController enemyController = enemyObject.GetComponent<EnemyController>();
        enemyController.player = player;
    }
}

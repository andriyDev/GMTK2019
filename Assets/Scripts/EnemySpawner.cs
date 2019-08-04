using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour, Togglable
{
    public bool SPAWNING = true;
    public float SPAWN_PERIOD = 10.0f;
    public int START_NUM_ENEMIES = 1;
    public bool INCREASING = true;
    public GameObject keyPrefab;
    public GameObject enemyPrefab;
    public PlayerController player;
    public GameObject topLeft;
    public GameObject bottomRight;
    public GameObject opens;
    public GameObject text;
    public bool DROP_KEYS = true;
    public float KEY_DROP_RATE = 0.2f;
    GameObject key;
    float spawnTimer;
    int numEnemies;

    void Start()
    {
        if (DROP_KEYS)
        {
            SpawnKey();
        }
        if (SPAWNING)
        {
            spawnTimer = SPAWN_PERIOD;
            numEnemies = START_NUM_ENEMIES;
            Spawn();
            if (INCREASING) {
                numEnemies++;
            }
        }
    }

    void Update()
    {
        if (SPAWNING && player != null)
        {
            if (spawnTimer <= 0)
            {
                for(int i = 0; i < numEnemies; i++) {
                    Spawn();
                }
                spawnTimer = SPAWN_PERIOD;
                if (INCREASING) {
                    numEnemies++;
                }
            }
            else
            {
                spawnTimer -= Time.deltaTime;
            }
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
        if (DROP_KEYS && Random.value < KEY_DROP_RATE)
        {
            DROP_KEYS = false;
            enemyController.key = key;
        }
    }

    void SpawnKey()
    {
        GameObject keyObject = (GameObject) Instantiate(keyPrefab);
        keyObject.SetActive(false);
        KeyController keyController = keyObject.GetComponent<KeyController>();
        Component opensController = opens.GetComponent<Door>();
        Component textController = text.GetComponent<TogglableText>();
        if (opensController != null && textController != null)
        {
            keyController.targets.Add(opensController);
            keyController.targets.Add(textController);
            keyController.targets.Add(this);
            key = keyObject;
        }
    }

    public void Toggle()
    {
        SPAWNING = !SPAWNING;
        if (SPAWNING)
        {
            spawnTimer = SPAWN_PERIOD;
        }
    }
}

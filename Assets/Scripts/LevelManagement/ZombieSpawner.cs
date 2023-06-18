using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public float spawnTimer = 8f;
    public GameObject enemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 8, spawnTimer);
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.isGameOver)
        {
            gameObject.SetActive(false);
        }
    }

    void SpawnEnemy()
    {
        Vector3 pos = transform.position;
        Vector3 spawnLocation = new Vector3(pos.x + Random.Range(-3f, 3f), pos.y, pos.z + Random.Range(-3f, 3f));
        GameObject spawnedEnemy = Instantiate(enemyPrefab, spawnLocation, transform.rotation)
            as GameObject;

        spawnedEnemy.transform.parent = gameObject.transform;
    }
}

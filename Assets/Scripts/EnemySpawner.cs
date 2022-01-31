using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public float intervalSpawn = 5f;
    float timeSpawn;
    float randomSpawn;

    bool isPresent;
    GameObject goEnemy;

    // Start is called before the first frame update
    void Start()
    {
        isPresent = false;
        timeSpawn = 0f;
        randomSpawn = Random.Range(0.5f, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPresent)
        {
            int index = Random.Range(0, enemyPrefabs.Length);
            goEnemy = Instantiate(enemyPrefabs[index], transform.position, Quaternion.Euler(Vector2.zero));
            isPresent = true;
        }

        if (goEnemy == null)
        {
            timeSpawn += Time.deltaTime;

            if (timeSpawn > intervalSpawn * randomSpawn)
            {
                isPresent = false;
                timeSpawn = 0f;
                randomSpawn = Random.Range(0.5f, 1.5f);
            } 
        }
    }
}

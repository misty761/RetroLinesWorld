using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject enemy;
    public float intervalSpawn = 5f;

    float timeSpawn;
    float randomSpawn;
    PlayerControl player;

    // Start is called before the first frame update
    void Start()
    {
        timeSpawn = 0f;
        randomSpawn = Random.Range(0.5f, 1.5f);
        player = FindObjectOfType<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.state != GameManager.State.Playing) return;

        timeSpawn += Time.deltaTime;

        float distanceX = player.transform.position.x - transform.position.x;
        float spawnX = 1.6f;

        if (distanceX > -spawnX && distanceX < spawnX)
        {
            if (timeSpawn > intervalSpawn * randomSpawn)
            {
                Vector2 pos = transform.position;
                float offsetX = 0.3f;
                if (distanceX < 0)
                {
                    pos = new Vector2(transform.position.x - offsetX, transform.position.y);
                }
                else
                {
                    pos = new Vector2(transform.position.x + offsetX, transform.position.y);
                }
                Instantiate(enemy, pos, Quaternion.Euler(Vector2.zero));

                timeSpawn = 0f;
                randomSpawn = Random.Range(0.5f, 1.5f);
            }
        }
        
    }
}

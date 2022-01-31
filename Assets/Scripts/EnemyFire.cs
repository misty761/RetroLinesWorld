using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    public GameObject prefBullet;
    public GameObject firePosition;
    public float intervalFire = 5f;

    float timeFire;
    float random;
    PlayerControl player;
    Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        timeFire = 0f;
        random = Random.Range(0.5f, 1.5f);
        player = FindObjectOfType<PlayerControl>();
        enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.state != GameManager.State.Playing) return;

        // 플레이어를 바라보도록함
        float distanceX = player.transform.position.x - transform.position.x;
        if (distanceX < 0) enemy.isLeft = true;
        else enemy.isLeft = false;

        float fireX = 1.6f;
        if (distanceX < -fireX || distanceX > fireX) return;

        timeFire += Time.deltaTime;
        if (timeFire > intervalFire * random)
        {
            timeFire = 0f;
            random = Random.Range(0.5f, 1.5f);
            Instantiate(prefBullet, firePosition.transform.position, Quaternion.Euler(Vector2.zero));
        }
    }
}

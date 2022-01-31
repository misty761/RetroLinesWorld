using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlant : MonoBehaviour
{
    public GameObject plant;
    public float attackX = 0.5f;
    public float attackY = 0.1f;
    PlayerControl player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceX = player.transform.position.x - transform.position.x;
        float distanceY = player.transform.position.y - transform.position.y;
        if (distanceX > -attackX && distanceX < attackX && distanceY > -attackY && distanceY < attackY)
        {
            Instantiate(plant, transform.position, Quaternion.Euler(Vector2.zero));
            Destroy(gameObject);
        }
    }
}

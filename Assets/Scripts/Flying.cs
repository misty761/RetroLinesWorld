using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flying : MonoBehaviour
{
    public float forceFly = 150f;
    Rigidbody2D rb;
    Enemy enemy;
    PlayerControl player;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemy = GetComponent<Enemy>();
        player = FindObjectOfType<PlayerControl>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.instance.state != GameManager.State.Playing) return;

        float distanceX = player.transform.position.x - transform.position.x;
        float movingX = 3f;
        if (distanceX > -movingX && distanceX < movingX)
        {
            if (transform.position.y < enemy.originalPosition.y)
            {
                rb.AddForce(new Vector2(0, forceFly));
            }
        }      
    }
}

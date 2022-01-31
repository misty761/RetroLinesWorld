using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 1f;
    
    bool isMovingRight;
    PlayerControl player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerControl>();
        isMovingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.state != GameManager.State.Playing) return;

        float distanceX = player.transform.position.x - transform.position.x;
        float movingX = 3f;
        if (distanceX > -movingX && distanceX < movingX)
        {
            if (isMovingRight)
            {
                transform.Translate(Vector2.right * Time.deltaTime * speed);
            }
            else
            {
                transform.Translate(Vector2.left * Time.deltaTime * speed);
            }
        } 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            if (collision.contacts[0].normal.x < -0.9f || collision.contacts[0].normal.x > 0.9f)
            {
                isMovingRight = !isMovingRight;
            }
        }    
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (isMovingRight)
            {
                collision.transform.Translate(Vector2.right * Time.deltaTime * speed);
            }
            else
            {
                collision.transform.Translate(Vector2.left * Time.deltaTime * speed);
            }
        }
    }
}

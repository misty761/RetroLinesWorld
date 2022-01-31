using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 2f;
    public float power = 1f;
    
    PlayerControl player;
    bool isRight;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerControl>();
        if (player.islookingRight) isRight = true;
        else isRight = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRight)
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed);
        }
        else
        {
            transform.Translate(Vector2.left * Time.deltaTime * speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            float distanceX = player.transform.position.x - transform.position.x;
            float attackX = 1.4f;
            if (distanceX > -attackX && distanceX < attackX) enemy.TakeDamage(power);
        }
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    public GameObject[] respawnPoints;

    PlayerControl player;
    int index;

    private void Start()
    {
        player = FindObjectOfType<PlayerControl>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GameManager.instance.PlayerHit();
            Vector2 offset = player.transform.position - respawnPoints[0].transform.position;
            float sqrLen = offset.sqrMagnitude;
            index = 0;
            for (int i = 0; i < respawnPoints.Length; i++)
            {
                offset = player.transform.position - respawnPoints[i].transform.position;
                if (offset.sqrMagnitude < sqrLen)
                {
                    sqrLen = offset.sqrMagnitude;
                    index = i;
                }
            }
            player.transform.position = respawnPoints[index].transform.position;
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;
        }
    }
}

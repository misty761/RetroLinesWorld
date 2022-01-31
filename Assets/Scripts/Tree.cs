using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public GameObject platform;
    PlayerControl player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerControl>();
        platform.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float distanceX = player.transform.position.x - platform.transform.position.x;
        float distanceY = player.transform.position.y - platform.transform.position.y;
        float appearX = 0.24f;
        float appearY = 0.06f;
        if (distanceX > -appearX && distanceX < appearX)
        {
            if (distanceY > appearY)
            {
                //print(distanceY);
                platform.SetActive(true);    
            }
            else if (distanceY < -appearY)
            {
                platform.SetActive(false);
            }
        }
        else
        {
            platform.SetActive(false);
        }

        if (player.isDown) platform.SetActive(false);
    }
}

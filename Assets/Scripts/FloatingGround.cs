using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingGround : MonoBehaviour
{
    public GameObject platformPlayer;
    PlayerControl player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerControl>();
        platformPlayer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float distanceY = player.transform.position.y - platformPlayer.transform.position.y;
        float appearY = 0.06f;
        if (distanceY > appearY)
        {
            platformPlayer.SetActive(true);
        }
        else if (distanceY < -appearY)
        {
            platformPlayer.SetActive(false);
        }

        if (player.isDown) platformPlayer.SetActive(false);
    }
}

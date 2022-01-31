using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonJump : MonoBehaviour
{
    PlayerControl player;
    Rigidbody2D rbPlayer;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerControl>();
        rbPlayer = player.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        player = FindObjectOfType<PlayerControl>();
        rbPlayer = player.GetComponent<Rigidbody2D>();

        if (Input.GetKeyDown(KeyCode.S))
        {
            ButtonClick();
        }
    }

    public void ButtonClick()
    {
        if (GameManager.instance.state != GameManager.State.Playing) return;

        if (player.countJump < player.countJumpMax)
        {
            SoundManager.instance.PlaySound(SoundManager.instance.audioJump, player.transform.position, 1f);
            player.animator.SetTrigger("Jump");
            player.countJump++;
            rbPlayer.velocity = Vector2.zero;
            rbPlayer.AddForce(new Vector2(0, player.forceJump));
        }
    }
}

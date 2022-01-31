using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAttack : MonoBehaviour
{
    public GameObject prefBullet;

    PlayerControl player;
    bool isButtonDown;
    float timeFire;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerControl>();
        isButtonDown = false;
        timeFire = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timeFire += Time.deltaTime;
        player = FindObjectOfType<PlayerControl>();
        Vector2 pos = player.firePosition.transform.position;
        if (isButtonDown && timeFire > player.intervalFire)
        {
            if (GameManager.instance.state != GameManager.State.Playing) return;

            SoundManager.instance.PlaySound(SoundManager.instance.audioFire, player.transform.position, 1f);
            player.animator.SetTrigger("Fire");
            Instantiate(prefBullet, pos, Quaternion.Euler(Vector2.zero));
            timeFire = 0f;
        }

        // Å°º¸µå
        if (Input.GetKeyDown(KeyCode.A)) ButtonDown();
        else if (Input.GetKeyUp(KeyCode.A)) ButtonUp();
    }

    public void ButtonDown()
    {
        isButtonDown = true;
    }

    public void ButtonUp()
    {
        isButtonDown = false;
    }
}

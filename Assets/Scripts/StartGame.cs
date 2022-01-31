using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            ButtonClick();
        }
    }

    public void ButtonClick()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.audioClick, 1f);
        GameManager.instance.StartGame();
    }
}

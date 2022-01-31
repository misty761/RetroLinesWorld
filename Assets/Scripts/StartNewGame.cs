using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartNewGame : MonoBehaviour
{
    public GameObject canvas;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            ButtonClick();
        }
    }

    public void ButtonClick()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.audioClick, 1f);
        Destroy(canvas);
        Destroy(GameManager.instance);
        SceneManager.LoadScene("Scene1");
    }
}

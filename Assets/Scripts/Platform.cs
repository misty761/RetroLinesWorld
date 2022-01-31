using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public float positionMinX;
    public float positionMaxX;
    public float positionMinY;
    public float positionMaxY;
    //public GameObject ground;

    public float speed = 0.5f;
    bool isRight;
    bool isUp;
    bool isPlayerOn;
    Transform platform;
    PlayerControl player;

    private void Start()
    {
        platform = GetComponent<Transform>();
        player = FindObjectOfType<PlayerControl>();

        isRight = true;
        isUp = true;
        isPlayerOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.state != GameManager.State.Playing) return;

        if (positionMaxX - positionMinX != 0)
        {
            if (platform.position.x < positionMaxX && isRight)
            {
                platform.Translate(Vector3.right * Time.deltaTime * speed);

                if (isPlayerOn) player.transform.Translate(Vector3.right * Time.deltaTime * speed);
            }
            else if (platform.position.x > positionMaxX && isRight)
            {
                isRight = false;
            }
            else if (platform.position.x > positionMinX && !isRight)
            {
                platform.Translate(Vector3.left * Time.deltaTime * speed);

                if (isPlayerOn) player.transform.Translate(Vector3.left * Time.deltaTime * speed);
            }
            else if (platform.position.x < positionMinX && !isRight)
            {
                isRight = true;
            }
        }

        if (positionMaxY - positionMinY != 0)
        {
            if (platform.position.y < positionMaxY && isUp)
            {
                platform.Translate(Vector3.up * Time.deltaTime * speed);

                if (isPlayerOn) player.transform.Translate(Vector3.up * Time.deltaTime * speed);
            }
            else if (platform.position.y > positionMaxY && isUp)
            {
                isUp = false;
            }
            else if (platform.position.y > positionMinY && !isUp)
            {
                platform.Translate(Vector3.down * Time.deltaTime * speed);

                if (isPlayerOn) player.transform.Translate(Vector3.down * Time.deltaTime * speed);
            }
            else if (platform.position.y < positionMinY && !isUp)
            {
                isUp = true;
            }
        }

    }

    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && other.gameObject.transform.position.y > this.transform.position.y)
        {
            //ground.SetActive(true);
            isPlayerOn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //ground.SetActive(false);
            isPlayerOn = false;
        }  
    }
    */

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            print(collision);
            isPlayerOn = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            print(collision);
            isPlayerOn = false;
        }
    }

}

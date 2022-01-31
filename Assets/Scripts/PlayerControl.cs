using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed = 1f;
    public GameObject firePosition;
    public float intervalFire = 0.2f;
    public float delayDamage = 1f;
    public int countJumpMax = 2;
    public float forceJump = 150f;

    Joystick joystick;
    public Animator animator;
    float h;
    float v;
    public bool islookingRight;
    bool isMoving;
    public int countJump;
    public bool isDown;
    float timeDamage;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        islookingRight = true;
        isMoving = false;
        countJump = countJumpMax;
        isDown = false;
        timeDamage = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.state != GameManager.State.Playing) return;

        // joystick
        joystick = FindObjectOfType<Joystick>();
        float joystickFactor = 2f;
        h = joystick.Horizontal * joystickFactor;
        v = joystick.Vertical * joystickFactor;
        if (h > 1f) h = 1f;
        else if (h < -1f) h = -1f;
        if (v > 1f) v = 1f;
        else if (v < -1f) v = -1f;

        // player move
        float joystickMoveMin = 0.1f;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector2.left * Time.deltaTime * speed);
            islookingRight = false;
            isMoving = true;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed);
            islookingRight = true;
            isMoving = true;
        }
        else if (h < -joystickMoveMin || h > joystickMoveMin)
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed * h);
            isMoving = true;
            if (h < -joystickMoveMin)
            {
                islookingRight = false;
            }
            else
            {
                islookingRight = true;
            }
        }
        else
        {
            isMoving = false;
        }

        // 방향키 다운
        if (Input.GetKey(KeyCode.DownArrow))
        {
            isDown = true;
        }
        else if (v < -0.99f)
        {
            isDown = true;
        }
        else
        {
            isDown = false;
        }

        // 방향에 따라 스프라이트 회전
        if (islookingRight)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else
        {
            transform.localScale = new Vector2(-1, 1);
        }

        // 애니메이터
        animator.SetBool("Moving", isMoving);

        // 데미지 딜레이
        timeDamage += Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0f)
        {
            countJump = 0;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Enemy enmey = collision.gameObject.GetComponent<Enemy>();
            if (enmey.timeAppear > enmey.delayAttack)
            {
                if (timeDamage > delayDamage)
                {
                    animator.SetTrigger("Hit");
                    GameManager.instance.PlayerHit();
                    timeDamage = 0f;
                }
            }
              
        }
    }
}

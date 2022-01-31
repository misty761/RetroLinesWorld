using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject warf;
    public float health = 10f;
    public float speed = 1f;
    public float movingDistance = 1f;
    
    // ����
    public float jumpInterval = 3f;
    public float jumpForece = 100f;
    float jumpRandom;
    float jumpTime;

    float movingRandom;
    Animator animator;
    public bool isLeft;
    public Vector2 originalPosition;
    PlayerControl player;
    bool isMoving;
    int point;
    public float delayAttack = 1f;
    public float timeAppear;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isLeft = true;
        originalPosition = transform.position;
        movingRandom = Random.Range(0.5f, 1.5f);
        player = FindObjectOfType<PlayerControl>();
        isMoving = false;
        jumpRandom = Random.Range(0.5f, 1.5f);
        jumpTime = 0f;
        point = (int)health;
        speed += GameManager.instance.offsetEnemySpeed;
        timeAppear = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.state != GameManager.State.Playing) return;

        timeAppear += Time.deltaTime;

        // ���� ����
        if (health < 0f)
        {
            GameManager.instance.AddScore(point);

            if (gameObject.tag == "Boss")
            {
                Instantiate(warf, transform.position, Quaternion.Euler(Vector2.zero));
            }

            Destroy(gameObject);
        }

        // �ٶ󺸴� ���⿡ ���� ��������Ʈ ȸ��
        if (gameObject.tag == "Boss")
        {
            if (isLeft)
            {
                transform.localScale = new Vector2(3, 3);
            }
            else
            {
                transform.localScale = new Vector2(-3, 3);
            }
        }
        else
        {
            if (isLeft)
            {
                transform.localScale = new Vector2(1, 1);
            }
            else
            {
                transform.localScale = new Vector2(-1, 1);
            }
        }

        float distanceX = player.transform.position.x - transform.position.x;
        float movingX = 3f;
        if (distanceX > -movingX && distanceX < movingX)
        {
            // �̵�
            if (isLeft)
            {
                transform.Translate(Vector2.left * Time.deltaTime * speed);
            }
            else
            {
                transform.Translate(Vector2.right * Time.deltaTime * speed);
            }

            // ���� ��ȯ
            if (transform.position.x < originalPosition.x - movingDistance * movingRandom)
            {
                isLeft = false;
                movingRandom = Random.Range(0.5f, 1.5f);
            }
            else if (transform.position.x > originalPosition.x + movingDistance * movingRandom)
            {
                isLeft = true;
                movingRandom = Random.Range(0.5f, 1.5f);
            }
            isMoving = true;

            // ����
            jumpTime += Time.deltaTime;
            if (jumpTime > jumpInterval * jumpRandom)
            {
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                rb.AddForce(new Vector2(0, jumpForece * jumpRandom));   // ���� �����ϸ� ���� ����
                jumpTime = 0;
                jumpRandom = Random.Range(0.5f, 1.5f);
            }
        }
        else
        {
            isMoving = false;
        }

        // �ִϸ�����
        animator.SetBool("Moving", isMoving);     
    }

    public void TakeDamage(float damage)
    {
        animator.SetTrigger("Hit");
        float random = Random.Range(0.5f, 1.5f);
        health -= damage * random;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isLeft = !isLeft;
    }
}

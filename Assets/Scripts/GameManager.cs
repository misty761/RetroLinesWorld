using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject uiTitle;
    public GameObject uiGameOver;
    public int playerLifeMax = 3;
    public Text textPlayerLife;
    public Text textScore;
    public Text textScoreTop;
    public Text textStage;
    public Text textTime;
    public float timeMax = 120f;
    public int stageMax = 3;
    
    public int playerLife;
    int score;
    int scoreTop;
    public int stage;
    float timeRemain;
    float preAlertTime = -100f;
    public float offsetEnemySpeed;

    public enum State
    {
        Title,
        Playing,
        GameOver 
    }

    public State state;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("GameManager : 게임 오브 젝트가 이미 존재합니다.");
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        stage = 1;
        textStage.text = "Stage " + stage;
        FirstInit();
        StageInit();
    }

    void FirstInit()
    {
        state = State.Title;
        uiTitle.SetActive(true);
        uiGameOver.SetActive(false);
        playerLife = playerLifeMax;
        textPlayerLife.text = "x " + playerLife;
        score = 0;
        textScore.text = "" + score;
        scoreTop = PlayerPrefs.GetInt("ScoreTop", 0);
        textScoreTop.text = "Top : " + scoreTop;
        offsetEnemySpeed = 0f;
    }

    private void StageInit()
    {
        timeRemain = timeMax;
        textTime.text = "Time : " + timeRemain;
        textStage.text = "Stage " + stage;
    }

    // Update is called once per frame
    void Update()
    {
        if (state != State.Playing) return;

        // 남은 시간
        timeRemain -= Time.deltaTime;
        if (timeRemain > 10f)
        {
            textTime.text = "Time : " + (int)timeRemain;
        }
        else
        {
            textTime.text = "Time : " + "<color=#FF0000>" + (int)timeRemain + "</color>";
        }
        if (timeRemain < 0)
        {
            PlayerHit();
            timeRemain += 10;
        }
        // Alert
        if (timeRemain < 10f && Time.time > preAlertTime + Mathf.Lerp(0.1f, 2f, (float)timeRemain / 10f))
        {
            SoundManager.instance.PlaySound(SoundManager.instance.audioAlert, transform.position, 1f);
            preAlertTime = Time.time;
        }
    }

    public void StartGame()
    {
        state = State.Playing;
        uiTitle.SetActive(false);
        uiGameOver.SetActive(false);
        StageInit();
    }

    public void ContinueGame()
    {
        if (stage == 1)
        {
            StartNewGame startNewGame = FindObjectOfType<StartNewGame>();
            startNewGame.ButtonClick();
        }
        else
        {
            SceneManager.LoadScene("Scene" + stage);
            FirstInit();
            StageInit();
        }
    }

    public void AddScore(int number)
    {
        score += number;
        textScore.text = "" + score;
    }

    public void PlayerHit()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.audioDamaged, transform.position, 1f);
        playerLife--;
        textPlayerLife.text = "x " + playerLife;

        if (playerLife <= 0)
        {
            // Game over
            SoundManager.instance.PlaySound(SoundManager.instance.audioGameOver, transform.position, 1f);
            state = State.GameOver;
            uiTitle.SetActive(false);
            uiGameOver.SetActive(true);
            if (score > scoreTop)
            {
                SoundManager.instance.PlaySound(SoundManager.instance.audioFanfare, transform.position, 1f);
                scoreTop = score;
                PlayerPrefs.SetInt("ScoreTop", scoreTop);
                textScoreTop.text = "Top : " + scoreTop;
            }
        }
    }

    public void LoadNextScene()
    {
        if (stage < stageMax)
        {
            stage++;
        }
        else
        {
            offsetEnemySpeed += 0.1f;
        }
        SceneManager.LoadScene("Scene" + stage);
        StageInit();
    }

}

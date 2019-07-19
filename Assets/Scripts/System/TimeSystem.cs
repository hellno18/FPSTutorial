using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class TimeSystem : MonoBehaviour
{
    //TEXT 変数
    [SerializeField] private Text m_TimeTextMinutes;
    [SerializeField] private Text m_TimeTextSeconds;
    CanvasGroup pauseCanvas;
    
    //2分でタイムカウント
    [SerializeField] private float timeLimit = 120f;
    float minutes;
    float seconds;
    //isPauseブール型
    bool isPause;

    SE se;


    public enum GameStatus
    {
        Start,
        Finished,
        Pause,
        Dead
    }

    public GameStatus gamestatus;
    // Start is called before the first frame update
    void Start()
    {
        //cast to SE 
        se = GameObject.Find("SE").GetComponent<SE>();

        isPause = false;
        pauseCanvas = GameObject.Find("Pause").GetComponent<CanvasGroup>();
        pauseCanvas.alpha = 0;
        gamestatus = GameStatus.Start;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //play SE Button
            se.PlayButtonSFX();

            GameObject player = GameObject.Find("Player");
            if (!isPause)
            {
                if (PlayerPrefs.GetString("Language") == "JP")
                {
                    Text pauseText = GameObject.Find("PauseText").GetComponent<Text>();
                    pauseText.text = "PAUSE\n" + "\n" + "\n" + "ESCボタンを押すと\nゲームへ戻る";
                }
                else
                {
                    Text pauseText = GameObject.Find("PauseText").GetComponent<Text>();
                    pauseText.text = "PAUSE\n"+ "\n" + "Press" + "\n" + "ESC For Unpause";
                }

                //Freeze camera while paused
                GameObject.Find("Player").GetComponent<FirstPersonController>().m_MouseLook.XSensitivity = 0;
                GameObject.Find("Player").GetComponent<FirstPersonController>().m_MouseLook.YSensitivity = 0;

                pauseCanvas.alpha = 1;
                Time.timeScale = 0;
                gamestatus = GameStatus.Pause;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                //打ってできない
                player.GetComponentInChildren<WeaponScript>().SetCanFire(false);
                isPause = true;
            }
            else
            {
                //unFreeze camera while paused
                GameObject.Find("Player").GetComponent<FirstPersonController>().m_MouseLook.XSensitivity = 2;
                GameObject.Find("Player").GetComponent<FirstPersonController>().m_MouseLook.YSensitivity = 2;

                pauseCanvas.alpha = 0;
                Time.timeScale = 1;
                isPause = false;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                player.GetComponentInChildren<WeaponScript>().SetCanFire(true);
                gamestatus = GameStatus.Start;
            }
            
        }
        switch (gamestatus)
        {
            case GameStatus.Start:
                TimeCountDown();
                break;

            case GameStatus.Finished:
                SceneManager.LoadScene("Result");
                break;
        }
    }

    //time countdown
    void TimeCountDown()
    {
        //タイムを減らす
        timeLimit -= Time.deltaTime;
        //分
        minutes = Mathf.Floor(timeLimit / 60);
        //秒
        seconds = timeLimit % 60;

        //タイムカウントを表示する
        m_TimeTextMinutes.text = ((int)minutes).ToString();
        m_TimeTextSeconds.text = ((int)seconds).ToString();

        if (seconds>=0&&seconds < 10)
        {
            m_TimeTextSeconds.text = "0" + (int)seconds;
        }
        if (minutes>=0&&minutes < 3)
        {
            m_TimeTextMinutes.text = "0" + (int)minutes;
        }

        // タイムカウントは０になるとゲームが終わる
        if (timeLimit <= 0f)
        {
            m_TimeTextMinutes.text = "0"+ (int)minutes;
            m_TimeTextSeconds.text = "0"+ (int)seconds;
            gamestatus = GameStatus.Finished;
        }
    }
}

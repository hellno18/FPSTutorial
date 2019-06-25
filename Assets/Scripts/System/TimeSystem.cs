using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TimeSystem : MonoBehaviour
{
    //TEXT 変数
    public Text TimeTextMinutes;
    public Text TimeTextSeconds;
    CanvasGroup pauseCanvas;
    
    //2分でタイムカウント
    [SerializeField]
    float timeLimit = 120f;
    float minutes;
    float seconds;
    //isPauseブール型
    bool isPause;

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
            if (!isPause)
            {
                pauseCanvas.alpha = 1;
                Time.timeScale = 0;
                gamestatus = GameStatus.Pause;
                isPause = true;
            }
            else
            {
                pauseCanvas.alpha = 0;
                Time.timeScale = 1;
                isPause = false;
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
        TimeTextMinutes.text = ((int)minutes).ToString();
        TimeTextSeconds.text = ((int)seconds).ToString();

        // タイムカウントは０になるとゲームが終わる
        if (timeLimit <= 0f)
        {
            TimeTextMinutes.text = "0";
            TimeTextSeconds.text = "0";
            gamestatus = GameStatus.Finished;
        }
    }
}

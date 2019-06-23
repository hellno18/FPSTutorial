using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TimeSystem : MonoBehaviour
{
    public Text timeTextMinutes;
    public Text timeTextSeconds;
    CanvasGroup pauseCanvas;
    [SerializeField]
    float timeLimit = 120f;
    float minutes;
    float seconds;
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
        timeLimit -= Time.deltaTime;
        minutes = Mathf.Floor(timeLimit / 60);
        seconds = timeLimit % 60;

        timeTextMinutes.text = ((int)minutes).ToString();
        timeTextSeconds.text = ((int)seconds).ToString();

        if (timeLimit <= 0f)
        {
            timeTextMinutes.text = "0";
            timeTextSeconds.text = "0";
            gamestatus = GameStatus.Finished;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimeSystem : MonoBehaviour
{
    public Text timeTextMinutes;
    public Text timeTextSeconds;
    [SerializeField]
    float timeLimit = 120f;
    float minutes;
    float seconds;
    public enum GameStatus
    {
        Start,
        Finished,
        Dead
    }
    public GameStatus gamestatus;
    // Start is called before the first frame update
    void Start()
    {
        gamestatus = GameStatus.Start;
    }

    // Update is called once per frame
    void Update()
    {
        TimeCountDown();
    }

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

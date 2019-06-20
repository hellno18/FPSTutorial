using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerScore : MonoBehaviour {
    public int Score;
    public Text ScoreUI;
    public int health;
    public Slider healthslider;
    public float hurtscreentimer;
    public GameObject hurtscreen;
    TimeSystem timesystem;
	// Use this for initialization
	void Start () {
        timesystem = GameObject.Find("GameController").GetComponent<TimeSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        //print score on ScoreUI
        ScoreUI.text = Score.ToString();
        healthslider.value = health;
        if (hurtscreentimer > 0)
        {
            hurtscreen.SetActive(true);
        }
        else
        {
            hurtscreen.SetActive(false);
        }
        hurtscreentimer -= 1.0f * Time.deltaTime;
        if (hurtscreentimer < 0)
        {
            hurtscreentimer = 0;
        }
        if (health <= 0)
        {
           
            StartCoroutine(DelayDead());

        }
	}

    //function for add Score by point
    public void ScoreAdd(int point)
    {
        Score += point;
    }

    //function for add damage to player
    public void hurt(int damage)
    {
        health -= damage;
    }

    IEnumerator DelayDead() {
        //change status game
        timesystem.gamestatus = TimeSystem.GameStatus.Dead;
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Main");
    }
}

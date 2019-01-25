﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerScore : MonoBehaviour {
    public int Score;
    public Text ScoreUI;
    public int health;
    public Slider healthslider;
    public float hurtscreentimer;
    public GameObject hurtscreen;
	// Use this for initialization
	void Start () {
		
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
}

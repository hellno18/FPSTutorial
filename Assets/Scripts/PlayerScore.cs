using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerScore : MonoBehaviour {
    //UIスコアとHP
    public Text ScoreUI;
    public Slider HealthSlider;
    public GameObject HurtScreen;
    //HP変数
    [SerializeField] int health=100;
    float HurtScreenTimer;
    int Score;
    TimeSystem timesystem;
	// Use this for initialization
	void Start () {
        timesystem = GameObject.Find("GameController").GetComponent<TimeSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        //print score on ScoreUI
        ScoreUI.text = Score.ToString();
        HealthSlider.value = health;
        //ダメージを受けるとき、タイマーが０以上
        if (HurtScreenTimer > 0)
        {
            //HurtScreenをアクティブする
            HurtScreen.SetActive(true);
        }
        else
        {
            HurtScreenTimer = 0;
            HurtScreen.SetActive(false);
        }
        //タイマー元に戻すーー＞０になる
        HurtScreenTimer -= 1.0f * Time.deltaTime;
       
        //HPが０以下より
        if (health <= 0)
        {
            //DelayDead 関数を呼ぶ
            StartCoroutine(DelayDead());
        }
	}

    //GET　HP関数
    public int GetHealth()
    {
        return health;
    }

    //SET　HP関数
    public void SetHealth(int healthRecover)
    {
        health = healthRecover;
    }

    //function for add Score by point
    public void ScoreAdd(int point)
    {
        Score += point;
    }

    //SET SetHurtTimer関数
    public void SetHurtTimer(float timer)
    {
        HurtScreenTimer = timer;
    }

    //function for add damage to player
    public void hurt(int damage)
    {
        health -= damage;
    }

    //DelayDead関数、ディレイ
    IEnumerator DelayDead() {
        //change status game
        //死ぬ
        timesystem.gamestatus = TimeSystem.GameStatus.Dead;
        yield return new WaitForSeconds(1f);
        //次のシーン移動
        SceneManager.LoadScene("Main");
    }
}

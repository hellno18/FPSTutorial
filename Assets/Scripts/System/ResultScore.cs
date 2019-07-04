using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultScore : MonoBehaviour
{
    public Text ResultScoreText;
    public Text FadeText;
    
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetString("Language") != "JP")
        {
            //スコアを表示する
            ResultScoreText.text = "Your Result is " + PlayerPrefs.GetInt("TotalScore").ToString();
            FadeText.text = "Press Any Key to Continue!";
        }
        if(PlayerPrefs.GetString("Language") == "JP")
        {
            //スコアを表示する
            ResultScoreText.text = "あなたスコアは　" + PlayerPrefs.GetInt("TotalScore").ToString();
            FadeText.text = "押したら、次のページへ！";
        }
        
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("Title");
        }
    }
}

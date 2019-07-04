using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TranslateSystem : MonoBehaviour
{
    //TEXT UI変数
    public Text StartButton;
    public Text HowToPlay;
    public Text Exit;
    public Text BackButton;
    public Text HowtoPlayText;

    //MAINMENUをキャストする
    MainMenu system;
    
    // Start is called before the first frame update
    void Start()
    {
        system = GameObject.Find("GameManager").GetComponent<MainMenu>();

    }

    // Update is called once per frame
    void Update()
    {
        //system ブール型がTRUE
        if (system.GetBool())
        {
            //日本語を翻訳する
            StartButton.text = "スタート";
            HowToPlay.text = "遊び方";
            Exit.text = "閉じる";
            BackButton.text = "戻る";
            HowtoPlayText.text = "ASDW  -  移動\n" + "R - リロード\n" + "CTRL - しゃがむ\n" + "Shift - 走る\n" + "左クリック　-　シュート\n";

        }
        //system ブール型がFalse
        else
        {
            //英語を翻訳する
            StartButton.text = "Start";
            HowToPlay.text = "How To Play";
            Exit.text = "Exit";
            BackButton.text = "Back";
            HowtoPlayText.text = "ASDW  -  Movement\n" + "R - Reload\n" + "CTRL - Crouch\n" + "Shift - Sprint\n" + "Left - Click - Shoot\n";
        }
        
    }
}

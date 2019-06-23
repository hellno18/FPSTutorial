using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TranslateSystem : MonoBehaviour
{
    public Text startButton;
    public Text howToPlay;
    public Text exit;
    public Text backButton;
    public Text howtoPlayText;
    MainMenu system;
    
    // Start is called before the first frame update
    void Start()
    {
        system = GameObject.Find("GameManager").GetComponent<MainMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        if (system.GetBool())
        {
            startButton.text = "スタート";
            howToPlay.text = "遊び方";
            exit.text = "閉じる";
            backButton.text = "戻る";
            howtoPlayText.text = "ASDW  -  移動\n" + "R - リロード\n" + "CTRL - しゃがむ\n" + "Shift - 走る\n" + "左クリック　-　シュート\n";

        }
        else
        {
            startButton.text = "Start";
            howToPlay.text = "How To Play";
            exit.text = "Exit";
            backButton.text = "Back";
            howtoPlayText.text = "ASDW  -  Movement\n" + "R - Reload\n" + "CTRL - Crouch\n" + "Shift - Sprint\n" + "Left - Click - Shoot\n";
        }
        
    }
}

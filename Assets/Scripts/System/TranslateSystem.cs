using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TranslateSystem : MonoBehaviour
{
    public Text startButton;
    public Text howToPlay;
    public Text exit;
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
        }
        else
        {
            startButton.text = "Start";
            howToPlay.text = "How To Play";
            exit.text = "Exit";
        }
        
    }
}

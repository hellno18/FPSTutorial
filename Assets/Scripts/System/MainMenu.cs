using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //isJapaneseブール型の変数を宣言
    bool isJapanese;
    //ゲームオブジェクトの変数を宣言
    //ボタングループ
    CanvasGroup canvasGroupButton;
    //操作方法のゲームオブジェクト
    GameObject canvasGroupPanel;
    GameObject groupButton;

    //フェイドの早さ
    [SerializeField]
    private float _speed = 0.2f;

    //ENUM CanvasFader
    private enum CanvasFader
    {
        None,
        FadeIn,
        Finished,
        FadeOut
    }

    private CanvasFader fadeState;
    // Start is called before the first frame update
    void Start()
    {
        //isJapaneseがfalse
        isJapanese = false;
        fadeState = CanvasFader.None;
        canvasGroupButton = GameObject.Find("ButtonGroup").GetComponent<CanvasGroup>();
        groupButton = GameObject.Find("ButtonGroup");
        canvasGroupPanel = GameObject.Find("Panel");

        //canvasGroupPanelをアクティブしない
        canvasGroupPanel.SetActive(false);

    }

    private void Update()
    {
        switch (fadeState)
        {
            case CanvasFader.FadeIn:
                // 1は表示また０は表示しない
                //　表示のエフェクトはフェイドIN
                canvasGroupButton.alpha += _speed;
                if (canvasGroupButton.alpha > 1)
                {
                    canvasGroupButton.alpha = 1;
                    fadeState = CanvasFader.None;
                }
                canvasGroupPanel.SetActive(false);
                groupButton.SetActive(true);
                break;
            case CanvasFader.Finished:
                break;
            case CanvasFader.FadeOut:
                //　表示のエフェクトはフェイドOUT
                canvasGroupButton.alpha -= _speed;
                if (canvasGroupButton.alpha < 0)
                {
                    canvasGroupButton.alpha = 0;
                    fadeState = CanvasFader.Finished;
                }
                canvasGroupPanel.SetActive(true);
                groupButton.SetActive(false);
                break;

        }
    }

    public void StartButton()
    {
        //Main シーンに移動
        SceneManager.LoadScene("Main");
    }

    public void HowToPlay()
    {
        fadeState = CanvasFader.FadeOut;
    }

    public void Japanese()
    {
        isJapanese = true;
        PlayerPrefs.SetString("Language", "JP");
    }

    public void English()
    {
        isJapanese = false;
        PlayerPrefs.SetString("Language", "EN");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Back()
    {
        fadeState = CanvasFader.FadeIn;
    }

    //GETブール型
    public bool GetBool()
    {
        return isJapanese;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    bool isJapanese;
    CanvasGroup _canvasGroupButton;
    GameObject _canvasGroupPanel;
    [SerializeField]
    private float speed = 0.2f;

    private enum CanvasFader
    {
        None,
        FadeIn,
        Finished,
        FadeOut
    }
    private CanvasFader _fadeState;
    // Start is called before the first frame update
    void Start()
    {
        isJapanese = false;
        _fadeState = CanvasFader.None;
        _canvasGroupButton = GameObject.Find("ButtonGroup").GetComponent<CanvasGroup>();
        _canvasGroupPanel = GameObject.Find("Panel");
        _canvasGroupPanel.SetActive(false);

    }

    private void Update()
    {
        switch (_fadeState)
        {
            case CanvasFader.FadeIn:
                _canvasGroupButton.alpha -= speed;
                if (_canvasGroupButton.alpha < 0)
                {
                    _canvasGroupButton.alpha = 0;
                    _fadeState= CanvasFader.Finished;
                }
                _canvasGroupPanel.SetActive(true);
                break;
            case CanvasFader.Finished:
                break;
            case CanvasFader.FadeOut:
                _canvasGroupButton.alpha += speed;
                if(_canvasGroupButton.alpha > 1)
                {
                    _canvasGroupButton.alpha = 1;
                    _fadeState = CanvasFader.None;
                }
                _canvasGroupPanel.SetActive(false);
                break;

        }
    }

    public void StartButton()
    {
        SceneManager.LoadScene("Main");
    }

    public void HowToPlay()
    {
        _fadeState = CanvasFader.FadeIn;
    }

    public void Japanese()
    {
        isJapanese = true;
    }

    public void English()
    {
        isJapanese = false;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Back()
    {
        _fadeState = CanvasFader.FadeOut;
    }

    public bool GetBool()
    {
        return isJapanese;
    }
}

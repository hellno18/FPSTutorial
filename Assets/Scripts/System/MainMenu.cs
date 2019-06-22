using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    bool isJapanese;
    // Start is called before the first frame update
    void Start()
    {
        isJapanese = false;
    }

    public void StartButton()
    {
        SceneManager.LoadScene("Main");
    }

    public void HowToPlay()
    {

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

    public bool GetBool()
    {
        return isJapanese;
    }
}

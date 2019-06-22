using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CheckEnemy : MonoBehaviour
{
    public Text remainingEnemyText;
    int enemyRemaining = 26;
    // Start is called before the first frame update
    void Start()
    {
        remainingEnemyText.text = enemyRemaining.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //update to display
        remainingEnemyText.text = enemyRemaining.ToString();
        if (enemyRemaining < 0)
        {
            StartCoroutine(Delay());
        }
    }

    public void DecreaseEnemy(int x)
    {
        enemyRemaining -= x;
    }

    //give delay 1 second
    IEnumerator Delay()
    {
        enemyRemaining = 0;
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Result");
    }
}

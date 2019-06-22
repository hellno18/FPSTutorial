using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    }

    public void DecreaseEnemy(int x)
    {
        enemyRemaining -= x;
    }
}

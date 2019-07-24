using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CheckEnemy : MonoBehaviour
{
    //TEXT　UI変数
    [SerializeField] private Text RemainingEnemyText;
    // 残り敵のローカル変数
    int enemyRemaining = 26;
    // Start is called before the first frame update
    void Start()
    {
        //enemyRemainingINT型からString型に変数するとremainingEnemyTextで表示する
        RemainingEnemyText.text = enemyRemaining.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //表示を更新（time.deltaTime）
        //update to display
        RemainingEnemyText.text = enemyRemaining.ToString();
        //enemyRemainingは０以下
        if (enemyRemaining < 0)
        {
            //Delay関数を呼び
            StartCoroutine(Delay());
        }
    }

    //残り敵を減らす
    public void DecreaseEnemy(int enemy)
    {
        enemyRemaining -= enemy;
    }

    //ディレイ１秒、次のシーン
    //give delay 1 second
    IEnumerator Delay()
    {
        enemyRemaining = 0;
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Result");
    }
}

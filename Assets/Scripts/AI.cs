using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour {
    //敵の変数（HPや叩くスピードやアイテムなど）
    public ParticleSystem BloodSpread;
    [SerializeField] int health=100;
    [SerializeField] Transform enemy;
    [SerializeField] Transform target;
    [SerializeField] int hitDamage = 25;
    [SerializeField] int attackSpeed=1;
    [SerializeField] float seeSight=5f;
    [SerializeField] float distance=4f;
    [SerializeField] float far = 1.4f;
    [SerializeField] GameObject[] Bonus;
    //アイテム
    int randomItem;

    private bool _isWaiting;
    Animator anim;
    NavMeshAgent nav;

    enum EnemyState
    {
        idle,
        walk,
        attack
    }
    EnemyState enemyState;

	// Use this for initialization
	void Start () {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponentInChildren<Animator>();
        nav = GetComponent<NavMeshAgent>();
        _isWaiting = false;
        //SET RandomItemが0
        PlayerPrefs.SetInt("RandomItem", 0);
    }
	
	// Update is called once per frame
	void Update () {
        //プレイヤーと敵の環境
        distance = Vector3.Distance(target.position, enemy.position);
        //もし敵はプレイヤーを見える
        if (distance < seeSight )
        {
            enemyState = EnemyState.walk;
            var targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);

            // Smoothly rotate towards the target point.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1 * Time.deltaTime);
            
            //walk animation
            anim.SetBool("walk", true);
            //follow player
            //nav.Resume();
            nav.isStopped = false;
            nav.SetDestination(target.position);
            
            //プレイヤーと敵近く場合は
            if (distance< far)
            {
                //stop nav agent
                //nav.Stop();
                nav.isStopped=true;
                //walk animation stopped
                anim.SetBool("walk", false);
                
                //Attack state
                enemyState = EnemyState.attack;
                if(!_isWaiting) Strike(); // Strike関数を呼ぶ
            }
        
        }
        //見えないとき
        else
        {
            enemyState=EnemyState.idle;
            //walk animation stopped
            anim.SetBool("walk", false);
        }

       //HPは０以下
        if (health <= 0)
        {
            //call die function when health is below than 0. 
            //Die関数を呼び
            Die();
            //HPが０になる
            health = 0;
        }
	}

   //HPはダメージを与える
    public void Damage(int damage)
    {
        health -= damage;
        StartCoroutine(EffectDelay());
    }

    //DIE関数
    void Die()
    {
        anim.SetBool("die", true);
        //diedelay関数を呼び、ディレイを上げる.
        StartCoroutine(DieDelay());
    }

    void Strike()
    {
        anim.SetBool("attack", true);
        Debug.Log("Player Attack");
        if (anim.GetBool("attack"))
        {
            //ダメージ
            target.GetComponent<PlayerScore>().hurt(hitDamage);
            //赤いシーン出る
            target.GetComponent<PlayerScore>().SetHurtTimer(2);
            _isWaiting = true;
            //call strikedelay to delay by AttactSpeed
            StartCoroutine(StrikeDelay());
        }
       
    }

    //乱数関数アイテム
    void RandomItem()
    {
        randomItem = Random.Range(0, 51);

    }

    //GET関数HP
    public int GetHealth()
    {
        return health;
    }


    // Effect Delay function
    IEnumerator EffectDelay()
    {
        Quaternion q = Quaternion.Euler(transform.position.x, transform.position.y, 90);
        //GET location
        Vector3 location = target.GetComponentInChildren<WeaponScript>().GetPosition();
        //Spawn object on location
        GameObject obj = Instantiate(BloodSpread, location, q).gameObject;
        yield return new WaitForSeconds(0.5f);
        //破壊する
        Destroy(obj);
        
    }

    //StrikeDelay関数
    IEnumerator StrikeDelay()
    {
        yield return new WaitForSeconds(attackSpeed);
        //Enemy Attack  false
        anim.SetBool("attack", false);
        yield return new WaitForSeconds(attackSpeed);
        _isWaiting = false;
    }

    //DieDelay関数
    IEnumerator DieDelay()
    {
        yield return new WaitForSeconds(0.5f);
        //hide enemy
        anim.SetBool("die", false);
        gameObject.SetActive(false);
        //randomitem関数を呼び、乱数をあげる
        RandomItem();
        //ｙ方向ヴェクターは１です。
        Vector3 pos = transform.position;
        pos.y -= 1;
        transform.position = pos;

        /* 1-10 HP　　　回復アイテム
           11-40 Ammo 　弾薬アイテム
           41-50 Nothing　ない
         */
        if (randomItem <= 10)
        {
            //print("health");
            PlayerPrefs.SetInt("RandomItem", randomItem);
            Instantiate(Bonus[0], pos, Quaternion.identity);
        }
        if (randomItem > 10 && randomItem <= 40)
        {
            //print("ammo");
            PlayerPrefs.SetInt("RandomItem", randomItem);
            Instantiate(Bonus[1], pos, Quaternion.identity);
        }
        if (randomItem > 40)
        {
            //print("nothing");
            PlayerPrefs.SetInt("RandomItem", randomItem);
            
        }

        //cast to check enemy
        CheckEnemy checkenemy= GameObject.Find("GameController").GetComponent<CheckEnemy>();
        //敵を減らす
        checkenemy.DecreaseEnemy(1); 
        //破壊する
        Destroy(gameObject);
    }
}

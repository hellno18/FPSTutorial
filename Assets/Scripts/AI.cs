using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour {
    //敵の変数（HPや叩くスピードやアイテムなど）
    [SerializeField] int health=100;
    [SerializeField] Transform enemy;
    [SerializeField] Transform target;
    [SerializeField] int hitDamage = 25;
    [SerializeField] int attackSpeed=1;
    [SerializeField] float seeSight=5f;
    [SerializeField] float distance=4f;
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
        PlayerPrefs.SetInt("RandomItem", 0);
    }
	
	// Update is called once per frame
	void Update () {
        distance = Vector3.Distance(target.position, enemy.position);
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
            if (distance<1.4f)
            {
                //stop nav agent
                //nav.Stop();
                nav.isStopped=true;
                //walk animation
                anim.SetBool("walk", false);
                             
                enemyState = EnemyState.attack;
                if(!_isWaiting) Strike();
            }
        
        }
        else
        {
            enemyState=EnemyState.idle;
            //walk animation
            anim.SetBool("walk", false);
        }

       
        if (health <= 0)
        {
            //call die function when health is below than 0
            Die();
            health = 0;
        }
	}

   
    public void Damage(int damage)
    {
        health -= damage;
    }

    void Die()
    {
        anim.SetBool("die", true);
        StartCoroutine(diedelay());
    }

    void Strike()
    {
        anim.SetBool("attack", true);
        Debug.Log("Player Attack");
        if (anim.GetBool("attack"))
        {
            target.GetComponent<PlayerScore>().hurt(hitDamage);
            target.GetComponent<PlayerScore>().hurtscreentimer = 2;
            _isWaiting = true;
            //call strikedelay to delay by AttactSpeed
            StartCoroutine(strikedelay());
        }
       
    }

    void RandomItem()
    {
        randomItem = Random.Range(0, 51);

    }

    //GET関数HP
    public int GetHealth()
    {
        return health;
    }

    IEnumerator strikedelay()
    {
        yield return new WaitForSeconds(attackSpeed);
        //Enemy Attack  false
        anim.SetBool("attack", false);
        yield return new WaitForSeconds(attackSpeed);
        _isWaiting = false;
    }
    IEnumerator diedelay()
    {
        yield return new WaitForSeconds(0.5f);
        //hide enemy
        anim.SetBool("die", false);
        gameObject.SetActive(false);
        RandomItem();
        Vector3 pos = transform.position;
        pos.y -= 1;
        transform.position = pos;
        if (randomItem < 10)
        {
            print("health");
            PlayerPrefs.SetInt("RandomItem", randomItem);
            Instantiate(Bonus[0], pos, Quaternion.identity);
        }
        if (randomItem > 10 && randomItem < 40)
        {
            print("ammo");
            PlayerPrefs.SetInt("RandomItem", randomItem);
            Instantiate(Bonus[1], pos, Quaternion.identity);
        }
        if (randomItem > 40)
        {
            PlayerPrefs.SetInt("RandomItem", randomItem);
            print("nothing");
        }

        //cast to check enemy
        CheckEnemy checkenemy= GameObject.Find("GameController").GetComponent<CheckEnemy>();
        checkenemy.DecreaseEnemy(1); 
        Destroy(gameObject);
    }
}

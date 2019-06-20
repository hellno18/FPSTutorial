using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour {
    public int health;
    public Transform Enemy;
    public Transform Target;
    public int damage;
    public int AttactSpeed;
    public float seeSight;
    public float distance;
   
    private bool isWaiting;
    private Animator anim;
    private NavMeshAgent nav;

    enum EnemyState
    {
        idle,
        walk,
        attack
    }
    EnemyState enemyState;

	// Use this for initialization
	void Start () {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponentInChildren<Animator>();
        nav = GetComponent<NavMeshAgent>();
        isWaiting = false;
    }
	
	// Update is called once per frame
	void Update () {
        distance = Vector3.Distance(Target.position, Enemy.position);
        if (distance < seeSight )
        {
            enemyState = EnemyState.walk;
            var targetRotation = Quaternion.LookRotation(Target.transform.position - transform.position);

            // Smoothly rotate towards the target point.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1 * Time.deltaTime);
            
            //walk animation
            anim.SetBool("walk", true);
            //follow player
            nav.Resume();
            nav.SetDestination(Target.position);
            if (distance<1.5f)
            {
                //stop nav agent
                nav.Stop();
                //walk animation
                anim.SetBool("walk", false);
                             
                enemyState = EnemyState.attack;
                if(!isWaiting) Strike();
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
            Target.GetComponent<PlayerScore>().hurt(damage);
            Target.GetComponent<PlayerScore>().hurtscreentimer = 2;
            isWaiting = true;
            //call strikedelay to delay by AttactSpeed
            StartCoroutine(strikedelay());
        }
       
    }

    IEnumerator strikedelay()
    {
        yield return new WaitForSeconds(AttactSpeed);
        //Enemy Attack  false
        anim.SetBool("attack", false);
        yield return new WaitForSeconds(AttactSpeed);
        isWaiting = false;
    }
    IEnumerator diedelay()
    {
        yield return new WaitForSeconds(0.5f);
        //hide enemy
        anim.SetBool("die", false);
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}

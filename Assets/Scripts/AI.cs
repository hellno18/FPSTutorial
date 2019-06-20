using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour {
    public int health;
    public Transform Enemy;
    public Transform Target;
    public Camera maincamera;
    public int damage;
    public int AttactSpeed;
    public int speed;
    public int AttackRadiusl;
    public float distance;
    public bool attacking;
    public bool waiting;
    public bool inView;

    private Animator anim;
	// Use this for initialization
	void Start () {
        anim = GameObject.Find("Zombie").GetComponent<Animator>();
        inView = false;
        waiting = false;
        attacking = false;
	}
	
	// Update is called once per frame
	void Update () {
        //if enemy not find player
        if (Target == null)
        {
            Target = GameObject.FindWithTag("Player").transform;
        }
        //if enemy find player
        if (Target != null)
        {
            Enemy.LookAt(Target);
        }
        //distance from enemy to player position
        distance = Vector3.Distance(Target.position, Enemy.position);
        
        // enemy can't attack if distance far from player
        if(distance>AttackRadiusl && inView)
        {
            attacking = false;
            waiting = false;
        }

        if(attacking && !waiting && health > 0)
        {
            //call strike function to attack player
            Strike();
        }

        Vector3 rayOrigin = maincamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit hit;

        if(Physics.Raycast(rayOrigin,maincamera.transform.forward,out hit))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                inView = true;
            }
            else
            {
                inView=false;
            }
        }

        // enemy can attack if distance near from player
        if (distance <= AttackRadiusl)
        {
            attacking = true;
            //Enemy Attack true
            anim.SetBool("attack", true);
        }
        else
        {
            attacking = false;
            //Enemy Attack false
            anim.SetBool("attack", false);
        }

        //enemy will jump into the player
        if(Target!=null && inView && distance>=AttackRadiusl&&distance<=20)
        {
            Enemy.Translate(Vector3.forward * speed * Time.deltaTime);
            //Enemy Walk animation
            anim.SetBool("walk", true);
           
        }

        else
        {
            //Enemy Walk animation
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
        waiting = true;
        Debug.Log("Player Attack");
        Target.GetComponent<PlayerScore>().hurt(damage);
        Target.GetComponent<PlayerScore>().hurtscreentimer=2;

        //call strikedelay to delay by AttactSpeed
        StartCoroutine(strikedelay());
    }

    IEnumerator strikedelay()
    {
        yield return new WaitForSeconds(AttactSpeed);
        //Enemy Attack true
        anim.SetBool("attack", false);
        waiting = false;
    }
    IEnumerator diedelay()
    {
       
        yield return new WaitForSeconds(1f);
        //hide enemy
        anim.SetBool("die", false);
        Destroy(gameObject);
    }
}

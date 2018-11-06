using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	// Use this for initialization
	void Start () {
        inView = false;
        waiting = false;
        attacking = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Target == null)
        {
            Target = GameObject.FindWithTag("Player").transform;
           
        }
        if(Target != null)
        {
            Enemy.LookAt(Target);
        }
       distance = Vector3.Distance(Target.position, Enemy.position);
        if(distance>AttackRadiusl && inView)
        {
            attacking = false;
            waiting = false;
        }
        if(attacking && !waiting && health > 0)
        {
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

        if (distance <= AttackRadiusl)
        {
            attacking = true;
        }
        else
        {
            attacking = false;
        }

        if(Target!=null && inView && distance>=AttackRadiusl&&distance<=20)
        {
            Enemy.Translate(Vector3.forward * speed * Time.deltaTime);
            
        }

        if (health <= 0)
        {
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
        gameObject.SetActive(false);
    }
    void Strike()
    {
        waiting = true;
        Debug.Log("Player Attack");
        Target.GetComponent<PlayerScore>().hurt(damage);
        Target.GetComponent<PlayerScore>().hurtscreentimer=2;
        StartCoroutine(strikedelay());
    }
    IEnumerator strikedelay()
    {
        yield return new WaitForSeconds(AttactSpeed);
        waiting= false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    int healthRecover=30;
    [SerializeField]
    int ammoRefill=20;
    int temp;
    WeaponScript weapon;
    PlayerScore player;
    // Start is called before the first frame update
    void Start()
    {
        temp = PlayerPrefs.GetInt("RandomItem");
        weapon = GameObject.Find("Player").GetComponentInChildren<WeaponScript>();
        player = GameObject.Find("Player").GetComponentInChildren<PlayerScore>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (temp < 10)
            {
                //heal player
                if (player.health > 100) ;
                else
                {
                    player.health += healthRecover;
                    if (player.health > 100)
                    {
                        player.health = 100;
                    }
                    Destroy(gameObject);
                }
            }

            if (temp > 10 && temp < 40)
            {
                //get AmmoClip and Refill
                weapon.AmmoLeft += ammoRefill;
                Destroy(gameObject);
            }
        }
    }
}

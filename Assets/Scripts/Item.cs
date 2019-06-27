using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    //HP回復アイテムや弾薬アイテム
    [SerializeField] int healthRecover=30;
    [SerializeField] int ammoRefill=20;
    //乱数アイテム
    int randomItem;
    WeaponScript weapon;
    PlayerScore player;
    // Start is called before the first frame update
    void Start()
    {
        randomItem = PlayerPrefs.GetInt("RandomItem");
        weapon = GameObject.Find("Player").GetComponentInChildren<WeaponScript>();
        player = GameObject.Find("Player").GetComponentInChildren<PlayerScore>();
    }

    private void OnTriggerEnter(Collider other)
    {
        /* 1-10 HP　　　回復アイテム
          11-40 Ammo 　弾薬アイテム
          41-50 Nothing　ない
        */
        if (other.gameObject.tag == "Player")
        {
            if (randomItem <= 10)
            {
               
                //HPが１００以下
                if (player.GetHealth() < 100)
                {
                    int health = player.GetHealth();
                    //回復する
                    health += healthRecover;
                    //HPが１００以上
                    if (player.GetHealth() > 100)
                    {
                        // HPが１００
                        health = 100;
                        player.SetHealth(health);
                    }
                    else
                    {
                        player.SetHealth(health);
                    }

                    //オブジェクト破壊する
                    Destroy(gameObject);
                }
            }

            if (randomItem > 10 && randomItem <= 40)
            {
                //GET 弾薬アイテム
                int ammo = weapon.GetAmmoLeft();
                if (ammo < weapon.GetAmmoMax())
                {
                    // Refill 
                    ammo += ammoRefill;
                    if (ammo >= 180)
                    {
                        //MAX Ammo 180
                        ammo = 180;
                    }
                    //SET
                    weapon.SetAmmoLeft(ammo);
                    //オブジェクト破壊する
                    Destroy(gameObject);
                }
                
            }
        }
    }

}

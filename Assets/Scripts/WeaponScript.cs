using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WeaponScript : MonoBehaviour
{
    //変数（Ammoやダメージやリロードタイムなど）
    public string FireAnim;
    public string ReloadAnim;
    public string DrawAnim;
    public string IdleAnim;
    public AudioClip Firesound;
    public AudioClip FireReloadSound;
    public Text AmmoUI;
    public Text AmmoLeftUI;
    public GameObject MuzzleFlashObject;
    public ParticleSystem MuzzleFlash;
    public GameObject Effect;
    public Camera MainCamera;
    [SerializeField] int ammo=30;
    [SerializeField] int ammoLeft=150;        //left ammo
    [SerializeField] int ammoClip=30;        // ammo clip
    [SerializeField] int ammoMax=180;         //max of ammo
    [SerializeField] float fireTime=0.25f;
    [SerializeField] float reloadTime = 3;
    [SerializeField] int damage=30;
    bool canFire;
    int chanceHeadShot;
   
   
    public PlayerScore player;
    // Use this for initialization
    void Start()
    {
        GetComponent<Animation>().Blend(DrawAnim);
        MuzzleFlash =GetComponentInChildren<ParticleSystem>();
        canFire = false;
        StartCoroutine(DrawDelay());
    }

    //シュートできる関数
    IEnumerator DrawDelay()
    {
        yield return new WaitForSeconds(0.25f);
        canFire = true;
    }

    // Update is called once per frame
    void Update()
    {
        //ammo  
        AmmoUI.text = ammo.ToString() + "/";
        //ammo left
        AmmoLeftUI.text = ammoLeft.ToString();
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began && canFire && ammo > 0)
            {
                // while pressed touch or right click, call fire function
                Fire();
            }
        }
        //クリックしたあと、シュートする
        if (Input.GetMouseButton(0) && canFire && ammo > 0)
        {
            // while pressed touch or right click, call fire function
            Fire();
        }

        //リロードする
        if (Input.GetKey(KeyCode.R) && canFire && ammo >= 0 &&ammo<30)
        {
            //While pressed R, call reload function
            Reload();
        }
    }

    void RandomChance()
    {
        //0-99
        chanceHeadShot = Random.Range(0, 100);
    }

    //リロード関数
    void Reload()
    {
        canFire = false;
        GetComponent<Animation>().Blend(ReloadAnim);
        GetComponent<AudioSource>().PlayOneShot(FireReloadSound);
        ammoLeft += ammo - 30;

        //reload delay by Reloadtime
        StartCoroutine(ReloadDelay());
        
    }

    //シュート関数
    void Fire()
    {
        ammo -= 1;
        GetComponent<Animation>().Stop(FireAnim);
        GetComponent<Animation>().Blend(FireAnim);
        GetComponent<AudioSource>().PlayOneShot(Firesound);
        canFire = false;

        //fire delay by Firetime
        StartCoroutine(FireDelay());

        //flash effect while firing by 0.15f
        StartCoroutine(FlashDelay());

        //エフェクトをアクティブする
        MuzzleFlashObject.SetActive(true);
        Effect.SetActive(true);

        //muzzleFlash play particle effect
        MuzzleFlash.Play();
        Vector3 rayOrigin = MainCamera.ViewportToWorldPoint(new Vector3(0.5f,0.5f,0.0f));
        RaycastHit hit;

        if(Physics.Raycast(rayOrigin,MainCamera.transform.forward,out hit))
        {
            //when hit tag like "enemy" 
            if (hit.collider.tag == "Enemy")
            {
                if (hit.collider.GetComponent<AI>().GetHealth() <= damage)
                {
                    player.ScoreAdd(50);
                }
                //give damage to enemy　ダメージ与える
                hit.collider.GetComponent<AI>().Damage(damage);
                //add score スコア
                player.ScoreAdd(damage);
                //Debug.Log("HIT!!!");
            }
            else if(hit.collider.tag=="Hidrant"){
                Hidrant hidrant = hit.collider.GetComponent<Hidrant>();
                //エフェクト表示する
                hidrant.setHidrant(true);
                //Debug.Log("EFECT!!");
            }
            else if (hit.collider.tag == "Explosion")
            {
                Explosion explosion = hit.collider.GetComponent<Explosion>();
                //エフェクト表示する
                explosion.setExplosion(true);
            }
           else if (hit.collider.tag == "Head")
            {
                print("HEAD");
                //乱数チャンスHEADSHOT
                RandomChance();
                if (chanceHeadShot > 0 && chanceHeadShot <= 50)
                {
                    //give damage to Head (HeadShot System)
                    hit.collider.GetComponentInParent<AI>().Damage(damage/3*10);
                    //add score　スコア
                    player.ScoreAdd(damage / 3 * 10);
                }
                else
                {
                    //give damage to Head (HeadShot System)
                    hit.collider.GetComponentInParent<AI>().Damage(damage/3);
                    //add score　スコア
                    player.ScoreAdd(damage/3);
                }


               
            }
        }
    }

    //Get AmmoLeft関数
    public int GetAmmoLeft()
    {
        return ammoLeft;
    }
    //SET AmmoLeft関数
    public void SetAmmoLeft(int ammoleft)
    {
        ammoLeft = ammoleft;
    }

    //Get AmmoMax関数
    public int GetAmmoMax()
    {
        return ammoMax;
    }
    //SET CanFire 関数
    public void SetCanFire(bool canfire)
    {
        canFire = canfire;
    }

    //シュートディレイ関数
    IEnumerator FireDelay()
    {
        yield return new WaitForSeconds(fireTime);
        canFire = true;
    }

    //リロードディレイ関数
    IEnumerator ReloadDelay()
    {
        yield return new WaitForSeconds(reloadTime);
        canFire = true;
        ammo = ammoClip;
    }

    //エフェクトのシュートディレイ関数
    IEnumerator FlashDelay()
    {
        yield return new WaitForSeconds(0.15f);
        MuzzleFlashObject.SetActive(false);
        Effect.SetActive(false);
        MuzzleFlash.Stop();
    }
}

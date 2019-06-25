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
    [SerializeField] int Damage=30;
    bool canFire;
   
   
    public PlayerScore player;
    // Use this for initialization
    void Start()
    {
        GetComponent<Animation>().Blend(DrawAnim);
        MuzzleFlash =GetComponentInChildren<ParticleSystem>();
        canFire = false;
        StartCoroutine(drawdelay());
    }
    IEnumerator drawdelay()
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
        if (Input.GetMouseButton(0) && canFire && ammo > 0)
        {
            // while pressed touch or right click, call fire function
            Fire();
        }

        if (Input.GetKey(KeyCode.R) && canFire && ammo >= 0 &&ammo<30)
        {
            //While pressed R, call reload function
            Reload();
        }
    }

    void Reload()
    {
        canFire = false;
        GetComponent<Animation>().Blend(ReloadAnim);
        GetComponent<AudioSource>().PlayOneShot(FireReloadSound);
        ammoLeft += ammo - 30;

        //reload delay by Reloadtime
        StartCoroutine(reloaddelay());
        
    }

    void Fire()
    {
        ammo -= 1;
        GetComponent<Animation>().Stop(FireAnim);
        GetComponent<Animation>().Blend(FireAnim);
        GetComponent<AudioSource>().PlayOneShot(Firesound);
        canFire = false;

        //fire delay by Firetime
        StartCoroutine(firedelay());

        //flash effect while firing by 0.15f
        StartCoroutine(flashdelay());

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
                if (hit.collider.GetComponent<AI>().GetHealth() <= Damage)
                {
                    player.ScoreAdd(50);
                }
                //give damage to enemy
                hit.collider.GetComponent<AI>().Damage(Damage);
                //add score
                player.ScoreAdd(Damage);
                //Debug.Log("HIT!!!");
            }
            else if(hit.collider.tag=="Hidrant"){
                Hidrant hidrant = hit.collider.GetComponent<Hidrant>();
                hidrant.setHidrant(true);
                Debug.Log("EFECT!!");
            }
            else if (hit.collider.tag == "Explosion")
            {
                Explosion explosion = hit.collider.GetComponent<Explosion>();
                explosion.setExplosion(true);
            }
           else if (hit.collider.tag == "Head")
            {
                print("HEAD");
                //give damage to Head (HeadShot System)
                hit.collider.GetComponentInParent<AI>().Damage(100);
                //add score
                player.ScoreAdd(100);
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

    IEnumerator firedelay()
    {
        yield return new WaitForSeconds(fireTime);
        canFire = true;
    }
    IEnumerator reloaddelay()
    {
        yield return new WaitForSeconds(reloadTime);
        canFire = true;
        ammo = ammoClip;
    }
    

    IEnumerator flashdelay()
    {
        yield return new WaitForSeconds(0.15f);
        MuzzleFlashObject.SetActive(false);
        Effect.SetActive(false);
        MuzzleFlash.Stop();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WeaponScript : MonoBehaviour
{
    public int Ammo;
    public int AmmoLeft;        //left ammo
    public int AmmoClip;        // ammo clip
    public int AmmoMax;         //max of ammo
    public float Firetime;
    public float Reloadtime;
    public string FireAnim;
    public string ReloadAnim;
    public string DrawAnim;
    public string IdleAnim;
    public int Damage;
    public bool canFire;
    public AudioClip Firesound;
    public AudioClip FireReloadSound;
    public Text AmmoUI;
    public Text AmmoLeftUI;
    public GameObject muzzleFlashObject;
    public ParticleSystem muzzleFlash;
    public GameObject effect;
    public Camera maincam;
    public PlayerScore player;
    // Use this for initialization
    void Start()
    {
        GetComponent<Animation>().Blend(DrawAnim);
        muzzleFlash =GetComponentInChildren<ParticleSystem>();
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
        AmmoUI.text = Ammo.ToString() + "/";
        //ammo left
        AmmoLeftUI.text = AmmoLeft.ToString();
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began && canFire && Ammo > 0)
            {
                // while pressed touch or right click, call fire function
                Fire();
            }
        }
        if (Input.GetMouseButton(0) && canFire && Ammo > 0)
        {
            // while pressed touch or right click, call fire function
            Fire();
        }

        if (Input.GetKey(KeyCode.R) && canFire && Ammo >= 0 &&Ammo<30)
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
        AmmoLeft += Ammo - 30;

        //reload delay by Reloadtime
        StartCoroutine(reloaddelay());
        
    }

    void Fire()
    {
        Ammo -= 1;
        GetComponent<Animation>().Stop(FireAnim);
        GetComponent<Animation>().Blend(FireAnim);
        GetComponent<AudioSource>().PlayOneShot(Firesound);
        canFire = false;

        //fire delay by Firetime
        StartCoroutine(firedelay());

        //flash effect while firing by 0.15f
        StartCoroutine(flashdelay());

        muzzleFlashObject.SetActive(true);
        effect.SetActive(true);

        //muzzleFlash play particle effect
        muzzleFlash.Play();
        Vector3 rayOrigin = maincam.ViewportToWorldPoint(new Vector3(0.5f,0.5f,0.0f));
        RaycastHit hit;

        if(Physics.Raycast(rayOrigin,maincam.transform.forward,out hit))
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

    IEnumerator firedelay()
    {
        yield return new WaitForSeconds(Firetime);
        canFire = true;
    }
    IEnumerator reloaddelay()
    {
        yield return new WaitForSeconds(Reloadtime);
        canFire = true;
        Ammo = AmmoClip;
    }
    

    IEnumerator flashdelay()
    {
        yield return new WaitForSeconds(0.15f);
        muzzleFlashObject.SetActive(false);
        effect.SetActive(false);
        muzzleFlash.Stop();
    }
}

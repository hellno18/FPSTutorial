using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    //ゲームオブジェクト（アクター、ブール型変数など)
    public GameObject Particle;
    
    //boolean型の変数を宣言
    bool effect;
    bool isCreate;

    // Start is called before the first frame update
    void Awake()
    {
        //'EffectとisCreateがFalse
        effect = false;
        isCreate = false;

        //GETキャスト ParticleSystem
        Particle.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        //effectがTrue AND isCreateがFalse
        if (effect && !isCreate)
        {
            //particleを表示される
            Instantiate(Particle, this.transform.position, Quaternion.identity);
            //isCreateがtrue
            isCreate = true;
            //ゲームオブジェクトを破壊する
            Destroy(this.gameObject);
        }


    }

    //SET爆発の関数
    public bool setExplosion(bool explosion)
    {
        //エフェクトは爆発です.
        effect = explosion;
        return explosion;
    }
}

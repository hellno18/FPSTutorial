using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hidrant : MonoBehaviour
{
    //ゲームオブジェクト（アクター、ブール型変数など)
    [SerializeField] private GameObject Particle;

    //boolean型の変数を宣言
    bool effect;
    bool isCreate;

    // Start is called before the first frame update
    void Awake()
    {
        //EffectとisCreateがFalse
        effect = false;
        isCreate = false;

        //GETキャスト ParticleSystem
        Particle.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        // X方向にー９０゜回転にする
        Quaternion q = Quaternion.Euler(-90f, 0f, 0f); //rotate

        //effectがTrue AND isCreateがFalse
        if (effect&&!isCreate)
        {
            //particleを表示される
            Instantiate(Particle, this.transform.position, q);
            //isCreateがTrue
            isCreate = true;
            
        }
           

    }
    
    //SET水道栓の関数
    public bool setHidrant(bool hydrant)
    {
        //エフェクトは水道栓です.
        effect = hydrant;
        return hydrant;
    }
}

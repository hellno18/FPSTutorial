using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject particle;
    bool effect;
    bool isCreate;
    // Start is called before the first frame update
    void Awake()
    {
        effect = false;
        isCreate = false;
        particle.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (effect && !isCreate)
        {
            Instantiate(particle, this.transform.position, Quaternion.identity);
            isCreate = true;
            Destroy(this.gameObject);
            
        }


    }

    public bool setExplosion(bool x)
    {
        effect = x;
        return x;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hidrant : MonoBehaviour
{
    public GameObject particle;
    bool effect;
    bool isCreate;
    // Start is called before the first frame update
    void Awake()
    {
        effect = false;
        isCreate = false;
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion q = Quaternion.Euler(-90f, 0f, 0f); //rotate
        
        if (effect&&!isCreate)
        {
            Instantiate(particle, this.transform.position, q);
            isCreate = true;
        }
           

    }
    
    public bool setHidrant(bool x)
    {
        effect=x;
        return x;
    }
}

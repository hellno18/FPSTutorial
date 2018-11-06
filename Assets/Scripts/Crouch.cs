using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Crouch : MonoBehaviour {
    public Transform t_mesh;                    // Player Transform
    public CharacterController ccr_controller;  // Get the character controller
    public bool IsCrouch = false;               // 
    public float LocalScaleY;                   // Y scale of "t_mesh"
    public float ControllerHeight;              // Y scale of the character controller
    public WeaponScript player;
    // PRIVATE VARS //

    void Start()
    {
        //nothing here yet
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            IsCrouch = !IsCrouch;
            CrouchFunction();
        }
    }

    void CrouchFunction()
    {
        if (IsCrouch == true)
        {
            t_mesh.localScale = new Vector3(1, LocalScaleY, 1);
            ccr_controller.height = ControllerHeight;
            Debug.Log("c_func 1");
            player.GetComponent<WeaponScript>().canFire = false;
        }
        else
        {
            t_mesh.localScale = new Vector3(1, 1, 1);
            ccr_controller.height = 1.8f;
            player.GetComponent<WeaponScript>().canFire = true;
            Debug.Log("c_func 0");
        }
    }
    
}

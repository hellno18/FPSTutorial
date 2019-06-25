using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Crouch : MonoBehaviour {
    [SerializeField] Transform t_mesh;                    // Player Transform
    [SerializeField] CharacterController ccr_controller;  // Get the character controller
    [SerializeField] float localScaleY=0f;                   // Y scale of "t_mesh"
    [SerializeField] float controllerHeight=0f;              // Y scale of the character controller
    //しゃがむモード変数
    bool isCrouch = false;
    WeaponScript player;
    GameObject crossFire;

    private void Awake()
    {
        crossFire = GameObject.Find("CrossFire");
        player = this.gameObject.GetComponentInChildren<WeaponScript>();
    }

    void Update()
    {
        //while ctrl button pressed, player will crouch 
        //CTRLボタン押したら、プレイヤーはしゃがむモード
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            //isCrouchがfalse
            isCrouch = !isCrouch;
            //call crouchfunction to crouch 
            //CrouchFunction関数を呼び
            CrouchFunction();
        }
    }

    //crouchfunction to crouch
    void CrouchFunction()
    {
        //isCrouchがtrue
        if (isCrouch)
        {
            GameObject crossFire = GameObject.Find("CrossFire");
            //武器UIをアクティブしない
            crossFire.SetActive(false);
            t_mesh.localScale = new Vector3(1, localScaleY, 1);
            ccr_controller.height = controllerHeight;
            //Debug.Log("c_func 1");
            //打ってできない
            player.GetComponent<WeaponScript>().SetCanFire(false);
        }
        else
        {
            //武器UIをアクティブしない
            crossFire.SetActive(true);
            t_mesh.localScale = new Vector3(1, 1, 1);
            ccr_controller.height = 1.8f;
            //打ってできる
            player.GetComponent<WeaponScript>().SetCanFire(true);
            //Debug.Log("c_func 0");
        }
    }
    
}

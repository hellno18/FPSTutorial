using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprint : MonoBehaviour {
    [SerializeField] private string SprintAnim = "Sprint";
    [SerializeField] private string ReturnAnim = "EndSprint";
  	
	// Update is called once per frame
	void Update () {
        //while pressed down left shift, player will start sprint 
        //押したら、走る
        if (Input.GetKeyDown(KeyCode.LeftShift)){
            //Sprint関数を呼ぶ
            SprintMethod();
        }

        //押した後、走る終わる
        //while pressed up left shift, player will stop sprint
        if (Input.GetKeyUp(KeyCode.LeftShift)){
            //Sprint終わる関数を呼ぶ
            EndSprint();
        }
    }
    //SprintMethod関数
    void SprintMethod()
    {
        GetComponent<Animation>().Blend(SprintAnim);
    }

    //EndSprint関数
    void EndSprint()
    {
        GetComponent<Animation>().Blend(ReturnAnim);
    }
}

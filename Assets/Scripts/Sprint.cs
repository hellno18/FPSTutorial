using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprint : MonoBehaviour {
    public string SprintAnim;
    public string ReturnAnim;
  	
	// Update is called once per frame
	void Update () {
        //while pressed down left shift, player will start sprint 
        if (Input.GetKeyDown(KeyCode.LeftShift)){
            sprintMethod();
        }
        //while pressed up left shift, player will stop sprint
        if (Input.GetKeyUp(KeyCode.LeftShift)){
            endSprint();
        }
    }

    void sprintMethod()
    {
        GetComponent<Animation>().Blend(SprintAnim);
    }
    void endSprint()
    {
        GetComponent<Animation>().Blend(ReturnAnim);
    }
}

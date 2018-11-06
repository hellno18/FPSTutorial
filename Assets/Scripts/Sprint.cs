using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprint : MonoBehaviour {
    public string SprintAnim;
    public string ReturnAnim;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.LeftShift)){
            sprintMethod();
        }
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

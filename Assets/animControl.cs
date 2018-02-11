using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animControl : MonoBehaviour {
    //Somewhat empty class to manage the animation states of paddle Links
    public Animator anim;
	void Start () {
        anim = GetComponent<Animator>();
	}

    public void PlayAnim()
    {
        anim.Play("LinkAnimationsSwing");
    }
}

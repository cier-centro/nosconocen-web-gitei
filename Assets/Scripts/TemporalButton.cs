using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TemporalButton : MonoBehaviour {

	public Animator anim1;
	public Animator anim2;
	public Button but;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void clickTemporal(){
		anim1.SetBool ("goOut", true);
		anim2.SetBool ("goOut", true);

	//	but.GetComponent<loadButton> ().isAviable = true;
	}

}

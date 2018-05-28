using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selfRotate : MonoBehaviour {

	public float speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		rotate ();
	}

	private void rotate(){
		Vector3 vec = new Vector3 (transform.localEulerAngles.x,transform.localEulerAngles.y+speed*Time.deltaTime,transform.localEulerAngles.z);
		transform.localEulerAngles = vec;
	}
}

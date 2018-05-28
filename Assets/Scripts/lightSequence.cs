using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightSequence : MonoBehaviour {

	public float startDelay=1;
	private bool start=false;
	private float time=0;
	private bool turnedOn=false;
	void Start () {
		Invoke ("startSequence", startDelay);
	}

	public void startSequence(){
		start = true;
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (start) {
			time += Time.deltaTime;
			if (time > 0.3f) {
				turnOnOff ();
				time = 0;

			}
		}
	}

	private void turnOnOff(){
		if (turnedOn) {
			gameObject.GetComponent<Renderer> ().material.color = Color.gray;
			turnedOn = !turnedOn;
		} else {
			gameObject.GetComponent<Renderer> ().material.color = new Color (227, 222, 192);
			turnedOn = !turnedOn;
		}

	}
}

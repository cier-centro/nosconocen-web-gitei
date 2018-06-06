using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextResize : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Text txt = GetComponent<Text> ();
		txt.fontSize = (int)((txt.fontSize*1f) * (Screen.width/1320f));
	}
}

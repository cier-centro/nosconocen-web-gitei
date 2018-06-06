using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendMessageScript : MonoBehaviour {

	public GameObject whoRecive;
	public GameObject prefab;
	public int tipe;

	public void send(GameObject selectedObject){
		switch(tipe){
		case 0:
			if (whoRecive.GetComponent<CardScript> () != null) {
				whoRecive.GetComponent<CardScript> ().enabled = true;
				whoRecive.GetComponent<CardScript> ().doSomething ();
			}
			break;
		case 1:
			if (whoRecive.GetComponent<SelectedBuild> () != null) {
				whoRecive.GetComponent<SelectedBuild> ().switchModel(prefab);
			}
			break;
		}
	}
}

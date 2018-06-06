using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadButton : MonoBehaviour {

	public bool isAviable=false;
	public GameObject canv;

	public void clickDetect(){
		if (isAviable) {
			Destroy(canv);
		}
	}
}

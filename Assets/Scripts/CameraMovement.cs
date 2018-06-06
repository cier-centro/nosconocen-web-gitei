using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	public Camera cam;
	//Camera Settings
	private List<float[]> cS=new List<float[]>();
	//Possition Count
	private int pC=0;
	private bool movementFinished = false;

	// Use this for initialization
	void Start () {
		//cam = gameObject.GetComponent<Camera> ();
		cS.Add(new float[]{ -10f,60f,0.2f,90f,90f,0f,72f});
		cS.Add(new float[]{-8.3f,2.06f,0.3f,0f,84f,0f,79f});
		cS.Add(new float[]{-7.68f,1.03f,-5.23f,0f,164.67f,0f,79f});
		cS.Add(new float[]{-22.6f,4.0f,-3.4f,3.13f,166f,0f,79f});
		cS.Add(new float[]{-22.34f,1.19f,-5.27f,0f,280.6f,0f,79f});
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float[] temp = {
			(float) cam.transform.position.x,
			(float) cam.transform.position.y,
			(float) cam.transform.position.z,
			(float) cam.transform.eulerAngles.x,
			(float) cam.transform.eulerAngles.y,
			(float) cam.transform.eulerAngles.z,
			(float) cam.fieldOfView
		};
		if (!isEquals(temp, cS[pC])&&!movementFinished ) {
			if(isClose(temp, cS[pC])){
				cam.transform.position = new Vector3 (cS [pC] [0], cS [pC] [1], cS [pC] [2]);
				cam.transform.eulerAngles = new Vector3 (cS [pC] [3], cS [pC] [4], cS [pC] [5]);
				cam.fieldOfView = cS [pC] [6];
				movementFinished = true;
			}else {
				cam.transform.position= Vector3.Lerp(cam.transform.position,new Vector3 (cS [pC] [0], cS [pC] [1], cS [pC] [2]),Time.deltaTime);
				cam.transform.eulerAngles= Vector3.Lerp(cam.transform.eulerAngles,new Vector3(cS [pC] [3], cS [pC] [4], cS [pC] [5]),Time.deltaTime);
				cam.fieldOfView = cam.fieldOfView+((int)cS [pC] [6]-cam.fieldOfView)*Time.deltaTime;
				//cam.fieldOfView = (int)cS [pC] [6];
			}
		}
    }

	public bool isClose(float[] arrayA, float[] arrayB){
		for (int i = 0; i < arrayA.Length; i++) {
			if (Mathf.Abs(arrayA [i] - arrayB [i])>0.02f) {
				return false;
			}
		}
		movementFinished = true;
		GetComponent<CameraMovScript> ().active(arrayB[4],arrayB[3]);
		return true;
	}

	public bool isEquals(float[] arrayA, float[] arrayB){
		for (int i = 0; i < arrayA.Length; i++) {
			if (arrayA[i] != arrayB[i]) {
					return false;
			}
		}

		GetComponent<CameraMovScript> ().active(arrayB[4],arrayB[3]);
		movementFinished = true;
		return true;
			
	}



	public void principalTouch(){
		pC++;
		GetComponent<CameraMovScript> ().activate = false;
		movementFinished = false;
		if (pC >= 5) {
			pC = 1;
		}
        GetComponent<ShowInfoProject>().loadName(pC);
    }

	public void secondaryTouch(){
		pC--;
		GetComponent<CameraMovScript> ().activate = false;
		movementFinished = false;
		if (pC >= 0) {
			pC = 4;
		}
		GetComponent<ShowInfoProject>().loadName(pC);
	}



}

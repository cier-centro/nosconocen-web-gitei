using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTouch : MonoBehaviour {

	public Camera cam;
	// Use this for initialization
	void Start () {
		//cam = GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () {
		foreach (Touch t in Input.touches) {
			//if ((Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began)) {
				//print ("plop");
				//Ray raycast = cam.ScreenPointToRay (Input.GetTouch (0).position);
				Ray raycast = cam.ScreenPointToRay (t.position);
				RaycastHit raycastHit;
				if (Physics.Raycast (raycast, out raycastHit)) {
					if (raycastHit.collider.gameObject.GetComponent<SendMessageScript> () != null) {
						raycastHit.collider.gameObject.GetComponent<SendMessageScript> ().send (raycastHit.collider.gameObject);
					}
				}
			//}
		}
	}
}

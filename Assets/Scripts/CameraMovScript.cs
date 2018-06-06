using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovScript : MonoBehaviour {

	private Touch initTouch = new Touch ();
	float swiftSpeed = 1f;
	//public Camera cam;
	Vector3 initialRotationOnWindEditor;
	private Vector3 initialRotation;
	public float dragSpeed = 2;
	public Text text;
	private Vector3 dragOrigin;
	private float yCenter=0;
	private float xCenter=0;
	public bool activate=false;

	public void active(float Yrot, float Xrot){
		yCenter = Yrot;
		xCenter = Xrot;
		activate = true;
	}
	void Start(){
		initialRotation = transform.eulerAngles;
	}



	void FixedUpdate(){
		if (activate) {
			#if UNITY_ANDROID
			foreach (Touch touch in Input.touches) { 
				if (touch.phase == TouchPhase.Began) {
					initTouch = touch;
				} else if (touch.phase == TouchPhase.Moved) {
				
					Vector2 temp = initTouch.position - touch.position;
					initialRotation.x -= temp.y * swiftSpeed * Time.deltaTime;
					initialRotation.y += temp.x * swiftSpeed * Time.deltaTime;
					if(initialRotation.x<xCenter-30f)
						initialRotation.x=xCenter-29f;
					
					if(initialRotation.x<xCenter+30f)
						initialRotation.x=xCenter+29f;

					if(initialRotation.y<yCenter-30f)
						initialRotation.y=yCenter-29f;
					
					if(initialRotation.y>yCenter+30f)
						initialRotation.y=yCenter+29f;

					transform.eulerAngles = new Vector3(initialRotation.x, initialRotation.y, 0f);

					text.text=transform.eulerAngles.x +" "+transform.eulerAngles.y +" "+transform.eulerAngles.z;
				} else if (touch.phase == TouchPhase.Ended) {
					initTouch = new Touch ();
				}

			}
			#elif UNITY_WEBGL
			#elif UNITY_EDITOR_WIN
		if (Input.GetMouseButtonDown(0))
		{
			dragOrigin = Input.mousePosition;
			return;
		}

		if (!Input.GetMouseButton(0)) return;

		Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
		Vector3 move = new Vector3(pos.y * dragSpeed, pos.x * dragSpeed, 0 );
		transform.Rotate(move);
		transform.eulerAngles=new Vector3(transform.eulerAngles.x,transform.eulerAngles.y,0);
			#endif
		}
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CubeManMove : MonoBehaviour {

	public int type = 0;
	public float speed = 0.5f;
	public Vector3 moveDirection=new Vector3(0f,0f,0f);
	// Use this for initialization
	void Start () {
		changeDirection ();
		if (type == 1) {
			GetComponent<Renderer> ().material.color = new Color (Random.value, Random.value, Random.value);
		}
	}
	
	// Update is called once per frame
	void Update () {
		switch (type) {
		default:
			
		case 0:
			transform.position = new Vector3 (transform.position.x + moveDirection.x * (speed * Time.deltaTime), transform.position.y, transform.position.z + moveDirection.z * (speed * Time.deltaTime));
			break;
		case 1:
			transform.position = new Vector3 (transform.position.x + moveDirection.x * (speed * Time.deltaTime), transform.position.y + moveDirection.y * (speed * Time.deltaTime), transform.position.z + moveDirection.z * (speed * Time.deltaTime));
			break;

		}

		Vector3 vec= Vector3.RotateTowards (transform.forward, moveDirection, 0.3f, 0.2f);
		gameObject.transform.rotation = Quaternion.LookRotation (vec);


		//gameObject.transform.LookAt (transform.position.x+moveDirection.x,0f,transform.position.z+moveDirection.z);

		//if(){RaycastHit}

	}

	private void comeBack(){
		Vector3 vec = transform.position - new Vector3 (0, 0, 0);
	}

	void OnTriggerEnter(Collider c){
		float rand = Random.value;
		if (rand < 0.45f) {
			if (moveDirection.x < 0) {
				changeDirection (0, 10f, 0f, 0f, -10f, 10f);
			} else {
				changeDirection (-10f, 0f, 0f, 0f, -10f, 10f);
			}
		} else if (rand > 0.45f && rand < 0.95f) {
			if (moveDirection.z < 0) {
				changeDirection (-10f, 10f, 0f, 0f, 0f, 10f);
			} else {
				changeDirection (-10f, 10f, 0f, 0f, -10f, 0f);
			}
		} else {
			changeDirection (-10f, 10f, -2f, 2f, 0f, 10f);
		}
				
	}

	public void changeDirection(){
		moveDirection.x=Random.Range (-10, 10f);
		moveDirection.z=Random.Range (-10, 10f);
		moveDirection.Normalize ();
	}
	public void changeDirection(float minValueX, float maxValueX, float minValueY, float maxValueY,float minValueZ, float maxValueZ){
		moveDirection.x=Random.Range (minValueX,maxValueX);
		moveDirection.y=Random.Range (minValueY,maxValueY);
		moveDirection.z=Random.Range (minValueZ,maxValueZ);
		moveDirection.Normalize ();
	}
}

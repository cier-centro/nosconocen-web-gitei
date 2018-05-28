using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour {

	// Use this for initialization
	public float movementSpeed=1;
	private List<Vector3> finalPoss=new List<Vector3>();
	public bool expand=true;
	private bool move=true;

	void Start(){
		setPositions ();
		print ("jueputa vida ");
	}

	void Update(){
		bool finish = true;
		for (int i = 0; i < finalPoss.Count; i++) {
			print (i+" aqui "+finalPoss [i]);
		}
		if (move) {
			if (expand) {
				//print ("plop ");
				int i = 0;
				foreach (Transform child in transform){
					//print (child.transform.localPosition);
					child.transform.localPosition= ((finalPoss [i] - child.transform.localPosition) * Time.deltaTime);
					//print (child.transform.position.x + " " + child.transform.position.y + " " + child.transform.position.z);
					i++;
				}
			} else {
				print ("no se que hago aqui");
				/*
				for (int i = 0; i < cards.Length; i++) {
					cards [i].transform.Translate ((new Vector3 (0f, 0f, 0f) - cards [i].transform.position) * Time.deltaTime);
					if ((finalPoss [i].x - cards [i].transform.position.x) == 0 && (finalPoss [i].z - cards [i].transform.position.z) == 0) {
						finish = false;
					}
				}*/
			}
		}
	}

	public void setPositions(){
		for (int i = 0; i < transform.childCount; i++) {
			finalPoss.Add(determinePossitions (i));
		}
	}

	private Vector3 determinePossitions(int cardNumb){
		int layer = (int) Mathf.Floor (cardNumb / 8)+1;
		//this array makes reference to X and Z axis, because the Y axis is not used in this case
		Vector3 possition;
		switch (cardNumb%8){
		case 0: possition= new Vector3(1.2f*layer,0,0);
			break;

		case 1: possition= new Vector3(-1.2f*layer,0,0);
			break;

		case 2: possition=new Vector3(0,0,1.2f*layer);
			break;

		case 3: possition=new Vector3(0,0,-1.2f*layer);
			break;

		case 4: possition=new Vector3(1.2f*layer,0,1.2f*layer);
			break;

		case 5: possition=new Vector3(1.2f*layer,0,-1.2f*layer);
			break;

		case 6: possition=new Vector3(-1.2f*layer,0,1.2f*layer);
			break;

		case 7: possition=new Vector3(-1.2f*layer,0,-1.2f*layer);
			break;
		default: possition=new Vector3(-1.2f*layer,100,-1.2f*layer);
			break;
		}
		return possition;
	}

	public void doSomething(){
		expand = !expand;
	}
}

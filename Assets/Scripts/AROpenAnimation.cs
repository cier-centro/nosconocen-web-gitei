using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AROpenAnimation : MonoBehaviour {

	public Vector3 finalRotation=new Vector3(0,0,0);
	public List<string> projects = new List<string> ();
	public float distance=3;
	public Vector3 contractedCardsSize = new Vector3 (0.0006f,0.0001f,0.015f);
	public GameObject cardBase;
	private bool goToTheMiddle=false;
	private bool rotate=false;
	private bool expand=false;
	private bool toTheScreen= false;

	private List<GameObject> cards=new List<GameObject>();


	// Use this for initialization
	void Start () {
		for (int i = 0; i < projects.Count; i++) {
			cards.Add (Instantiate (cardBase) as GameObject);
			//cards [i].transform.eulerAngles = cards [i].transform.eulerAngles + new Vector3 (0, 20, 0);
			cards [i].transform.SetParent (transform);
			cards [i].transform.localScale = contractedCardsSize;
			cards [i].transform.localPosition = new Vector3 (0f, 0f, 0f);
		}
		print (projects.Count);
		print (cards.Count);
	}
	
	// Update is called once per frame
	void Update () {

	}


	void FixedUpdate(){
		if (goToTheMiddle) {
			toMiddle ();		
		}
		if(rotate){
			makeRotation ();
		}
		if(expand){
			expandCards ();
		}
		if (toTheScreen) {
			cardsToTheScreen ();
		}
	}

	public void setAppear(){
		goToTheMiddle = true;
	}

	private void toMiddle(){
		if (isCloser(transform.localPosition.y, 0f)) {
			//transform.localPosition = new Vector3 (transform.localPosition.x, 0f, transform.localPosition.x);
			goToTheMiddle = false;
			rotate = true;
		} else {
			transform.localPosition = Vector3.Lerp (transform.localPosition, new Vector3 (transform.localPosition.x, 0f, transform.localPosition.z), Time.deltaTime * 3f);
		}
	}

	private void makeRotation(){
		if (isCloser (transform.localEulerAngles.x, finalRotation.x)) {
			transform.localEulerAngles = finalRotation;
			rotate = false;
			expand = true;
		} else {
			transform.localEulerAngles = Vector3.Lerp (transform.localEulerAngles, finalRotation, Time.deltaTime * 3f);
		}
	}

	private void expandCards(){
		for (int i = 0; i < cards.Count; i++) {
			cards [i].transform.localPosition = Vector3.Lerp (cards [i].transform.localPosition, new Vector3 ((-distance*Mathf.Sin((20*(i)*Mathf.PI)/180)),0,(-distance*Mathf.Cos((20*(i)*Mathf.PI)/180))), Time.deltaTime * 4);
			cards [i].transform.localScale = Vector3.Lerp (cards [i].transform.localScale, Vector3.Scale(contractedCardsSize,new Vector3 (1500f,50f,1500f)), Time.deltaTime * 4);
		}

		if (isCloser(-distance,cards[0].transform.localPosition.z)) {
			expand = false;
			toTheScreen = true;
			return;
		}
	}

	private void cardsToTheScreen(){
		transform.localPosition = Vector3.Lerp (transform.localPosition, Vector3.zero,Time.deltaTime);
		transform.localEulerAngles = Vector3.Lerp (transform.localEulerAngles, new Vector3 (0f, 179.8f, 0f), Time.deltaTime);
	}


	private bool isCloser(float valueA, float valueB){
		return (Mathf.Abs (valueA - valueB) < 0.05);
	}

	private bool isCloser(Vector3 valueA, Vector3 valueB){
		return((Mathf.Abs (valueA.x - valueB.x) < 0.05) && (Mathf.Abs (valueA.y - valueB.y) < 0.05) && (Mathf.Abs (valueA.z - valueB.z) < 0.05));
	}

}

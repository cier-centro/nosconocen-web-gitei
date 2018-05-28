using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedBuild : MonoBehaviour {

	private GameObject build;
	public GameObject parent;
	private GameObject storedBuild;
	public Animator anim;
	private bool instantiated;


	void Start(){
		instantiated = false;
		Animator anim = GetComponentInParent<Animator> ();
	}
	// Update is called once per frame
	void Update () {
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("giteiCardsCompresSelected") && anim.IsInTransition (0)) {
			build = Instantiate (storedBuild);
			build.transform.SetParent (parent.transform);
		}
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("giteiCardsContractBack") && anim.IsInTransition (0)) {
			foreach (Transform child in parent.transform) {
				GameObject.Destroy (child.gameObject);
			}
		}
	}


	public void switchModel(GameObject SelectedBuild){
		if (!instantiated) {
			instantiated = true;
			anim.SetBool ("contract", true);
			storedBuild = SelectedBuild;
			build = Instantiate (storedBuild) as GameObject;
			build.transform.SetParent (parent.transform);
			build.transform.localScale = build.transform.localScale * 0.01f;
			build.transform.localPosition = new Vector3 (0f, 0f, 0f);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonsScript : MonoBehaviour {

	public GameObject tutorial;
	public GameObject selectionView;

	void Update()
	{
		if (Input.GetKey(KeyCode.Escape))
		{
			SceneManager.LoadScene(0);
		}
	}

	public void closeTutorial(){
		tutorial.SetActive (false);
	}

	public void openTutorial(){
		tutorial.SetActive (true);
		tutorial.GetComponent<ScrollSnapRect>().init();
	}

	public void openSelection(){
		selectionView.SetActive(true);

	}

	public void loadAR(){
		SceneManager.LoadScene (3);
	}

	public void loadCity(){
		SceneManager.LoadScene (1);
	}

	public void backButton(){
		SceneManager.LoadScene (0);
	}

	public void projectList(){
		SceneManager.LoadScene (2);
	}
}

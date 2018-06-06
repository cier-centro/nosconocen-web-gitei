using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShowInfoProject : MonoBehaviour {


    public Text textTitle;
    public Text textDescription;
    private List<string[]> projects = new List<string[]>();
	private bool writeDescription = false;
    private int descriptionTextPosition = 0;
    private string[] textToWrite= new string[2];
    private float time;
    private bool hideTit = true;
    private bool hideDesc = true;
    private float positionTextY;

    // Use this for initialization
    void Start () {
        cargarArchivo();
        positionTextY = textTitle.transform.parent.transform.position.y;
	}

	// Update is called once per frame

	void FixedUpdate () {


        if (hideTit)
        {
            float temp = Mathf.Lerp(textTitle.transform.parent.transform.position.y,positionTextY, Time.deltaTime*3*speed(Screen.height));
            textTitle.transform.parent.transform.position = new Vector3( textTitle.transform.parent.transform.position.x,temp, 0);
        }
        else {
            float temp = Mathf.Lerp(textTitle.transform.parent.transform.position.y, positionTextY-Screen.height/2, Time.deltaTime*speed(Screen.height));
            textTitle.transform.parent.transform.position = new Vector3( textTitle.transform.parent.transform.position.x, temp, 0);
        }
		if (writeDescription){
			time = Time.deltaTime;
			if (time > 0.001f){
				textDescription.text = textDescription.text + textToWrite[1].ToCharArray()[descriptionTextPosition];
				descriptionTextPosition++;
				if (descriptionTextPosition >= textToWrite [1].ToCharArray ().Length) {
					descriptionTextPosition = 0;
					writeDescription = false;
				}
			}

		}
		descriptionPosition();
	}

    private void descriptionPosition() {
		//RectTransform rectT = textDescription.transform.parent.gameObject.GetComponent<RectTransform> ();
		//float distance=Vector2.Distance (new Vector2 (rectT.anchorMin.y*Screen.width, 0), new Vector2 (rectT.anchorMax.y*Screen.width, 0));
		Vector2 actual =textDescription.transform.parent.transform.parent.gameObject.GetComponent<RectTransform> ().pivot;
        if (hideDesc)
        {
			if (hideTit) {
				textDescription.transform.parent.transform.parent.gameObject.GetComponent<RectTransform> ().pivot = Vector2.Lerp(actual, new Vector2 (0,1), Time.deltaTime*500000);
				float temp = Mathf.Lerp (textDescription.transform.parent.transform.parent.transform.position.x, Screen.width*1.3f, Time.deltaTime*speed(Screen.width*900));
				textDescription.transform.parent.transform.parent.transform.position = new Vector3 (temp, textDescription.transform.parent.transform.parent.transform.position.y, 0);
			} else {
				//rectT.pivot = new Vector2 (0, 0);
				textDescription.transform.parent.transform.parent.gameObject.GetComponent<RectTransform> ().pivot = Vector2.Lerp(actual, new Vector2 (0,1), Time.deltaTime*5000);
				float temp = Mathf.Lerp (textDescription.transform.parent.transform.parent.transform.position.x, Screen.width, Time.deltaTime*speed(Screen.width));
				textDescription.transform.parent.transform.parent.transform.position = new Vector3 (temp, textDescription.transform.parent.transform.parent.transform.position.y, 0);
			}
        }
        else
        {
			//float distance=Vector2.Distance (rectT.offsetMin*Screen.width,rectT.offsetMax*Screen.width);
			//rectT.pivot = new Vector2 (1, 0);
			//print (distance);
			textDescription.transform.parent.transform.parent.gameObject.GetComponent<RectTransform> ().pivot = Vector2.Lerp(actual, new Vector2 (1, 1), Time.deltaTime*9000);
			float temp = Mathf.Lerp(textDescription.transform.parent.transform.parent.transform.position.x, Screen.width, Time.deltaTime*speed(Screen.width));
			textDescription.transform.parent.transform.parent.transform.position = new Vector3(temp, textDescription.transform.parent.transform.parent.transform.position.y, 0);
        }
    }

    private bool cargarArchivo(){
        try
        {
            TextAsset textAsset = (TextAsset)Resources.Load("ProjectList") as TextAsset;
            string[] lines = textAsset.text.Split("\n"[0]);
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i] != null)
                {
                    projects.Add(lines[i].Split('|'));
                }
            }
            /*StreamReader archivo = new StreamReader(Application.dataPath+"/Resources/ListaDocs.txt");
			while((linea=archivo.ReadLine())!=null){
				documentos.Add(linea.Split('|'));
			}
			archivo.Close();*/
            return true;
        }
        catch (System.Exception e)
        {
            print("Error al cargar Lista de documentos: \n" + e.StackTrace);
            return false;
        }
    }

    public void loadName(int code) {
		hideTit = true;
		//textDescription.transform.parent.transform.parent.gameObject.GetComponent<RectTransform>().pivot=new Vector2 (0f,1f);
		hideDesc = true;
		textDescription.text = ".";
        textToWrite[0] = projects[code][0];
		textToWrite[1] = projects[code][1];
        time = 0;
		Invoke ("showTitle", 1.5f);
    }

	private float speed(float destinx){
        float auxiliar = textDescription.transform.parent.transform.parent.transform.position.y + destinx;
		auxiliar = Mathf.Abs (auxiliar);
        if (auxiliar<Screen.height*(3/4)) {
			return 4;
		}
        if (auxiliar<Screen.height*(2/3)) {
			return 2;
		}
		return 1;
	}

	private void showTitle(){
		textTitle.text = textToWrite[0];
		hideTit = false;
	}

    public void loadDescription(){
		//textDescription.transform.parent.transform.parent.gameObject.GetComponent<RectTransform>().pivot=new Vector2 (1f,1f);
		if (hideDesc) {
			hideDesc = false;
			textDescription.text = "";
			Invoke ("startWriting", 0.5f);
		} else {
			hideDesc = true;
			textDescription.text = ".";
			textDescription.text = "";
		}
    }

	private void startWriting(){
		descriptionTextPosition = 0;
		time = 0.001f;
		writeDescription = true;
	}

}

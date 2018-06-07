using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;


public class ShowInfoProject : MonoBehaviour {


    public Text textTitle;
    public Text textTitle2;
    public Text textDescription;
    private List<string[]> projects = new List<string[]>();
	private bool writeDescription = false;
    private int descriptionTextPosition = 0;
    private string[] textToWrite= new string[2];
    private float time;
    private bool hideTit = true;
    private bool hideDesc = true;
    private float positionTextY;
    private float positionTextY2;
    private float positionTextY3;

    // Use this for initialization
    void Start () {
        cargarArchivo();
        positionTextY = textTitle.transform.parent.transform.position.y;
        positionTextY2 = textTitle2.transform.parent.transform.position.y;
        positionTextY3 = textDescription.transform.parent.transform.position.y;
	}

	// Update is called once per frame

	void FixedUpdate () {


        if (hideTit)
        {
            float temp = Mathf.Lerp(textTitle.transform.parent.transform.position.y,positionTextY, Time.deltaTime*3*speed(Screen.height,1));
            textTitle.transform.parent.transform.position = new Vector3( textTitle.transform.parent.transform.position.x,temp, 0);

            float temp2 = Mathf.Lerp(textTitle2.transform.parent.transform.position.y, positionTextY2, Time.deltaTime * 3 * speed(Screen.height,1));
            textTitle2.transform.parent.transform.position = new Vector3(textTitle2.transform.parent.transform.position.x, temp2, 0);

            float temp3 = Mathf.Lerp(textDescription.transform.parent.transform.position.y, positionTextY3, Time.deltaTime * 3 * speed(Screen.height*2,4));
            textDescription.transform.parent.transform.position = new Vector3(textDescription.transform.parent.transform.position.x, temp3, 0);

        }
        else {
            float temp = Mathf.Lerp(textTitle.transform.parent.transform.position.y, positionTextY-Screen.height*0.6f, Time.deltaTime*speed(Screen.height,1));
            textTitle.transform.parent.transform.position = new Vector3( textTitle.transform.parent.transform.position.x, temp, 0);

            float temp2 = Mathf.Lerp(textTitle2.transform.parent.transform.position.y, positionTextY2 - Screen.height*0.6f, Time.deltaTime * speed(Screen.height,1));
            textTitle2.transform.parent.transform.position = new Vector3(textTitle2.transform.parent.transform.position.x, temp2, 0);

            float temp3 = Mathf.Lerp(textDescription.transform.parent.transform.position.y, positionTextY3 - Screen.height*0.7f, Time.deltaTime * speed(Screen.height*2,4));
            textDescription.transform.parent.transform.position = new Vector3(textDescription.transform.parent.transform.position.x, temp3, 0);

        }
		/*if (writeDescription){
			time = Time.deltaTime;
			if (time > 0.001f){
				textDescription.text = textDescription.text + textToWrite[1].ToCharArray()[descriptionTextPosition];
				descriptionTextPosition++;
				if (descriptionTextPosition >= textToWrite [1].ToCharArray ().Length) {
					descriptionTextPosition = 0;
					writeDescription = false;
				}
			}

		}*/

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
        Debug.Log(textToWrite[1]);
        time = 0;
		Invoke ("showTitle", 1.5f);
        Invoke("loadDescription", 2.0f);
    }

	private float speed(float destinx,int v){
        float auxiliar = textTitle.transform.parent.position.y;

		auxiliar = Mathf.Abs (auxiliar);
        if (auxiliar<destinx*(3/4)) {
			return 4*v;
		}
        if (auxiliar<destinx*(2/3)) {
			return 1*v;
		}
		return 0.5f*v;
	}

	private void showTitle(){
        string title=textToWrite[0].Split(' ')[0];
        textTitle.text = title.ToUpper();
        textTitle2.text = Regex.Replace(textToWrite[0],title+" ","").ToUpper() ;
		hideTit = false;
	}

    public void loadDescription(){
        //textDescription.transform.parent.transform.parent.gameObject.GetComponent<RectTransform>().pivot=new Vector2 (1f,1f);
        textDescription.text = textToWrite[1];
        hideTit = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;  
using UnityEngine.UI;

public class ListaDocumentos : MonoBehaviour {
	public GameObject prefab;
	List<string[]> documentos=new List<string[]>();
	private bool first=false;
	double factorHeightInv = Screen.height / 1280f;
	//double factorHeight = 1280f / Screen.height;

	private bool cargarArchivo(){
		try{
			TextAsset textAsset= (TextAsset)Resources.Load("ListaDocs") as TextAsset;
			string[] lineas=textAsset.text.Split("\n"[0]);
			for(int i=0; i<lineas.Length;i++){
				if(lineas[i]!=null){
					documentos.Add(lineas[i].Split('|'));
				}
			}
			/*StreamReader archivo = new StreamReader(Application.dataPath+"/Resources/ListaDocs.txt");
			while((linea=archivo.ReadLine())!=null){
				documentos.Add(linea.Split('|'));
			}
			archivo.Close();*/
			return true;
		}
		catch (System.Exception e)	{
			print("Error al cargar Lista de documentos: \n"+e.StackTrace);
			return false;
		}
	}

	public void botones(){
		RectTransform padre= gameObject.GetComponent<RectTransform>();
		RectTransform rectPrefab = prefab.GetComponent<RectTransform> ();
		float width = padre.rect.width;
		//float height=Screen.height/8;

		/*
		//Tamaño del panel que contiene los botones
		float scrollHeight = (float)(height *(documentos.Count)*(factorHeight-0.3))+(correccion()*documentos.Count);
		float minY = padre.offsetMax.y - scrollHeight;
		padre.offsetMin = new Vector2 (padre.offsetMin.x, minY);
		float yAnterior=0-(float)((height/2)*factorHeight);
		*/

		for (int i = 0; i <documentos.Count; i++) {
			
			//se instancia el boton y se coloca un padre
			GameObject boton = Instantiate (prefab) as GameObject;
			boton.transform.SetParent (gameObject.transform);
			boton.name=""+i;

			//agregar evento al click
			if (documentos [i] [0]=="Tipo1") {
				boton.GetComponent<Button> ().onClick.AddListener (() => Application.OpenURL (documentos [int.Parse (boton.name)] [2]));
			}
			if (documentos [i] [0] == "Tipo2") {
				//Abre pdf local en el navegador
				//boton.GetComponent<Button> ().onClick.AddListener (() => {pdfLocal(documentos [int.Parse (boton.name)] [2]);});
				//boton.GetComponent<Button> ().onClick.AddListener (() => {pdfLocal("ManualParaElRectorDiaE");});
				boton.GetComponent<Button> ().onClick.AddListener (() => {StartCoroutine( pdfLocal(documentos [int.Parse (boton.name)] [2]));});
				//boton.GetComponent<Button> ().onClick.AddListener (()=>Application.OpenURL(Application.streamingAssetsPath + "/ManualParaElRectorDiaE.pdf"));
			}

			//crear y ubicar boton
			RectTransform botonRectTransform = boton.GetComponent<RectTransform>();
			//boton.GetComponent<LayoutElement> ().preferredHeight = Screen.width*0.25f;


			//botonRectTransform.offsetMin=new Vector2(x,y);
			//y = y +(height);
			//x = padre.rect.width/4;
			//x= x+(Screen.width);
			//botonRectTransform.offsetMax=new Vector2(x,y);

			/*
			 * este codigo se usa para colocar el boton en Y cuando este no se ajusta automaticamente
			 * 
			 * 
			//y = (float)(0-((height/2)*factorHeight)-((height+correc) * i*factorHeight));
			botonRectTransform.anchoredPosition = new Vector2 (0,yAnterior);
			yAnterior=yAnterior-(float)(height*factorHeight)-correc;
			*/




			//modificaciones de texto
			Text[] texs = boton.GetComponentsInChildren<Text> ();
			RectTransform tex1RectTrans = texs [0].GetComponent<RectTransform> ();
			texs [0].text=documentos [i] [1];
			texs [0].fontSize = (int)(texs [0].fontSize*factorHeightInv);
			RectTransform tex2RectTrans = texs [1].GetComponent<RectTransform> ();

			if (documentos [i].Length>3) {
				texs [1].text = documentos [i] [3];
				if (Screen.height >= 1280 && Screen.width >= 720) {
					texs[1].GetComponent<RectTransform>().anchorMax=new Vector2(1f,0.7f);
				}
				if (Screen.height < 1024 && Screen.width < 600) {
					texs[1].GetComponent<RectTransform>().anchorMax=new Vector2(1f,0.5f);
				}
			} else {
				texs [0].GetComponent<RectTransform> ().anchorMin=new Vector2(texs [0].GetComponent<RectTransform> ().anchorMin.x,0.5f);
			}
			texs [1].fontSize = (int)(texs [1].fontSize*factorHeightInv);
			tex1RectTrans.anchoredPosition = new Vector2 (0f, -botonRectTransform.sizeDelta.y / 6);
			float xText =(float)(botonRectTransform.rect.width*1.7);
			tex1RectTrans.sizeDelta = new Vector2 (xText, tex1RectTrans.sizeDelta.y);
			tex2RectTrans.anchoredPosition = new Vector2 (0f, -botonRectTransform.sizeDelta.y / 6);
			xText = (float)(botonRectTransform.rect.width*1.7);
			tex2RectTrans.sizeDelta = new Vector2 (xText, tex2RectTrans.sizeDelta.y);
		}

	}

	public IEnumerator pdfLocal(string file){


		//string filename = file.Substring (0, file.Length - 1);
		string filename=file;
		string path =Application.streamingAssetsPath + "/"+filename+".pdf";
		string savePath = Application.persistentDataPath;
		
		WWW www = new WWW(path);
		yield return www;
		string error = www.error;
		byte[] bytes = www.bytes;
		string result = "Done, bytes downloaded, File size : "+bytes.Length;
		try{
			File.WriteAllBytes(savePath+"/"+filename+".pdf", bytes);
			print(savePath+"/"+filename+".pdf");
			Application.OpenURL(savePath+"/"+filename+".pdf");
			//error = "No Errors so far";
			print("hasta aqui bien");
		}catch(System.Exception ex){
			error = ex.Message;
		}
		//Application.OpenURL(savePath+"/"+filename+".pdf");

		/*
		TextAsset pdfTemp = Resources.Load (filename, typeof(TextAsset)) as TextAsset;
		System.IO.File.WriteAllBytes(Application.persistentDataPath + "/" + filename + ".pdf", pdfTemp.bytes);
		Application.OpenURL(Application.persistentDataPath + "/" + filename+".pdf");


		string path = System.IO.Path.Combine(Application.persistentDataPath, filename + ".pdf");

		TextAsset pdfTemp = Resources.Load(filename, typeof(TextAsset)) as TextAsset;

		File.WriteAllBytes(path, pdfTemp.bytes);

		Application.OpenURL(path);*/
	}

	public float correccion(){
		
		float correc=0f;
		if ((float)(Screen.height)>1280f){
			correc= (float)(3 * (factorHeightInv+3.7));
		}
		return correc;
	}

	void Start(){
		cargarArchivo();
		//se obtienen las medidas de los elementos del padre
		botones();
	}
}

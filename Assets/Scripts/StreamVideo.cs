﻿using System.Collections; using System.Collections.Generic; using UnityEngine; using UnityEngine.UI; using UnityEngine.Video;  public class StreamVideo : MonoBehaviour {      public RawImage rawImage;     public VideoPlayer videoPlayer;     public AudioSource audioSource;     public Canvas canvas;     public Text textVideo;     private bool hideVid = true;     public float positionVideoY;    // Use this for initialization  void Start () {         //StartCoroutine(PlayVideo());         positionVideoY = videoPlayer.transform.parent.transform.position.y;         Debug.Log(positionVideoY);    }        IEnumerator PlayVideo(){          videoPlayer.url = "http://aprende.colombiaaprende.edu.co/sites/default/files/naspublic/diae2018/videos/Bienvenida%20Dia%20E%202018.mp4";          videoPlayer.Prepare();         WaitForSeconds waitForSeconds = new WaitForSeconds(1);         while(!videoPlayer.isPrepared){             yield return waitForSeconds;             break;         }          rawImage.texture = videoPlayer.texture;         videoPlayer.Play();         audioSource.Play();         canvas.transform.localPosition = new Vector3(0, 0, 0);     }      void Update()     {                   /*if (hideVid)         {             float temp = Mathf.Lerp(videoPlayer.transform.parent.transform.position.y, positionVideoY, Time.deltaTime * 3 * speed(Screen.height));             videoPlayer.transform.parent.transform.position = new Vector3(videoPlayer.transform.parent.transform.position.x, temp, 0);         }         else         {             float temp = Mathf.Lerp(videoPlayer.transform.parent.transform.position.y, positionVideoY - Screen.height / 2, Time.deltaTime * speed(Screen.height));             videoPlayer.transform.parent.transform.position = new Vector3(videoPlayer.transform.parent.transform.position.x, temp, 0);         }*/      }      private float speed(float destinx)     {         float auxiliar = videoPlayer.transform.parent.transform.parent.transform.position.y + destinx;         auxiliar = Mathf.Abs(auxiliar);         if (auxiliar < Screen.height * (3 / 4))         {             return 4;         }         if (auxiliar < Screen.height * (2 / 3))         {             return 2;         }         return 1;     }      public void playVideo(){                  Debug.Log("cambio pantalla");         StartCoroutine(PlayVideo());          }      public void stopVideo(){         videoPlayer.Stop();         canvas.transform.localPosition = new Vector3(0, positionVideoY+1000, 0);         textVideo.text = videoPlayer.tag;     }  } 
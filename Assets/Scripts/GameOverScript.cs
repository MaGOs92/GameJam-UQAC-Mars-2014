using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour {
	
	public Color couleurEntrer = Color.red;
	public Color couleurSortie = Color.white;
	public string selection = "";
    //public AudioClip komtuveu;
	
	public void Start(){
		if (guiText.text == "Score"){
			guiText.text = "Score : " + GameStateManager.Instance.getScore();
		}
        //AudioSource.PlayClipAtPoint(komtuveu, transform.position);
	}
	
	public void OnMouseUp() { 
		if (selection == "RageQuit" ){
			Application.Quit();
		}
	}
	
	public void OnMouseEnter() {
		if (guiText.text == "RageQuit"){
			guiText.material.color = couleurEntrer;
			selection = guiText.text;
		}
	}
	
	public void OnMouseExit() {
		if (guiText.text == "RageQuit"){
			guiText.material.color = couleurSortie;
			selection = "";
		}
	}
}

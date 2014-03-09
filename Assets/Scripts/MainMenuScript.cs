using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour {

	public Color couleurEntrer = Color.red;
	public Color couleurSortie = Color.white;
	public string selection = "";



	public void OnMouseUp() { 
		if (selection == "Jouer" ) 
			GameStateManager.Instance.GameState = GameStateManager.GameStates.JeuDeShoot;
		else if (selection == "Quitter" ) {
            Application.Quit();
			//GameStateManager.Instance.GameState = GameStateManager.GameStates.Quitter;
		}
	}

	public void OnMouseEnter() {
		guiText.material.color = couleurEntrer;
		selection = guiText.text;
	}
	
	public void OnMouseExit() {
		guiText.material.color = couleurSortie;
		selection = "";
	}
}

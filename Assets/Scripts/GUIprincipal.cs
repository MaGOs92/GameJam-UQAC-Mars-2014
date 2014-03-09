using UnityEngine;
using System.Collections;

public class GUIprincipal : MonoBehaviour {

	public Font font;

	public Texture2D txPapier;
	public Texture2D txPatate;
	public Texture2D txOeuf;
	public Texture2D txPetard;

	private Transform a_playerPrefab;

	void OnGUI(){

		a_playerPrefab = GameObject.FindGameObjectWithTag("Player").transform;

		int resPatate = GameStateManager.Instance.getQuantite (Loot_Enum.Pattate);
		int resOeuf = GameStateManager.Instance.getQuantite (Loot_Enum.Oeuf_Pourri);
		int resPetard = GameStateManager.Instance.getQuantite (Loot_Enum.Petard);


		GUIStyle myStyle = new GUIStyle ();
		myStyle.font = font;
		myStyle.normal.textColor = Color.white;
		myStyle.fontSize = 20;
		
		GUI.Label(new Rect(Screen.width-550,40,90,40)," NIVEAU " + GameStateManager.Instance.ZoneDeJeu, myStyle );
		GUI.Label(new Rect(Screen.width-350,40,90,40)," SCORE " + GameStateManager.Instance.getScore(), myStyle );
		GUI.Label(new Rect(Screen.width-150,40,90,40)," VIE " + GameStateManager.Instance.getVie(), myStyle );

		if (a_playerPrefab.GetComponent<ControlPlayer>().getSelectedArme() == ArmeEnum.LanceBoulette) GUI.color = Color.white;
		else GUI.color = Color.grey;
		GUI.Box (new Rect (20, 15, 70, 75), "");
		GUI.Label (new Rect (30, 15, 55, 55), txPapier);
		GUI.Label (new Rect (38, 65, 50, 20), " infini ");

		if(GameStateManager.Instance.isPossessed(ArmeEnum.LancePatate)){
			if (a_playerPrefab.GetComponent<ControlPlayer>().getSelectedArme() == ArmeEnum.LancePatate) GUI.color = Color.white;
			else GUI.color = Color.grey;
			GUI.Box(new Rect(100,15,70,75), "");
			GUI.Label (new Rect (110, 15, 55, 55), txPatate);
			GUI.Label (new Rect (120, 65, 50, 20), " x " + resPatate);
		}

		if(GameStateManager.Instance.isPossessed(ArmeEnum.LanceOeuf)){
			if (a_playerPrefab.GetComponent<ControlPlayer>().getSelectedArme() == ArmeEnum.LanceOeuf) GUI.color = Color.white;
			else GUI.color = Color.grey;
			GUI.Box(new Rect(180,15,70,75), "");
			GUI.Label (new Rect (190, 15, 55, 55), txOeuf);
			GUI.Label (new Rect (200, 65, 50, 20),  " x " + resOeuf);
		}

		if(GameStateManager.Instance.isPossessed(ArmeEnum.LancePetard)){
			if (a_playerPrefab.GetComponent<ControlPlayer>().getSelectedArme() == ArmeEnum.LancePetard) GUI.color = Color.white;
			else GUI.color = Color.grey;
			GUI.Box(new Rect(260,15,70,75), "");
			GUI.Label (new Rect (270, 15, 55, 55), txPetard);
			GUI.Label (new Rect (280, 65, 50, 20), " x " + resPetard);
		}

	}

}

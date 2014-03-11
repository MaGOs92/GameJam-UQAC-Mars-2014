using UnityEngine;
using System.Collections;

//gerer l GUI du craft (très salement)
public class ScriptGui : MonoBehaviour 
{

	public Texture2D txScotch;
	public Texture2D txClous; 
	public Texture2D txPlanche;
	public Texture2D txPlaqMetal; 
	public Texture2D txCarton; 
	public Texture2D txTube;
	public Texture2D txUstensile;
	public Texture2D txGaz;
	public Texture2D txElastique;
	public Texture2D txPatate;
	public Texture2D txOeuf; 
	public Texture2D txPetard;
	
	public Texture2D txGunPatate;
	public Texture2D txGunPetard;
	public Texture2D txGunOeuf; 
	public Texture2D txArmCarton;
	public Texture2D txArmBois;
	public Texture2D txArmMetal;
	public Texture2D txRepar;



	public AudioClip sonBuy;

	void OnGUI () {

		Rect fc = new Rect((Screen.width/2)-390,(Screen.height/2)-290,780,580);
		Rect rc = new Rect(fc.xMin+130, fc.yMin+30,200, 150);

		int resScotch = GameStateManager.Instance.getQuantite(Loot_Enum.Scotch);
		int resClous = GameStateManager.Instance.getQuantite(Loot_Enum.Clous);
		int resBois = GameStateManager.Instance.getQuantite(Loot_Enum.Planche_de_bois);
		int resMetal = GameStateManager.Instance.getQuantite(Loot_Enum.Plaque_de_metal);
		int resCarton = GameStateManager.Instance.getQuantite(Loot_Enum.Carton);
		int resTube = GameStateManager.Instance.getQuantite(Loot_Enum.Tube_plastique);
		int resUstensile = GameStateManager.Instance.getQuantite(Loot_Enum.Ustensile);
		int resGaz = GameStateManager.Instance.getQuantite(Loot_Enum.Bonbonne_de_gaz);
		int resElastisque = GameStateManager.Instance.getQuantite(Loot_Enum.Elastique);
		int resPatate = GameStateManager.Instance.getQuantite(Loot_Enum.Pattate);
		int resOeuf = GameStateManager.Instance.getQuantite(Loot_Enum.Oeuf_Pourri);
		int resPetard = GameStateManager.Instance.getQuantite(Loot_Enum.Petard);
		//Debug.Log("du coup on a " + GameStateManager.Instance.getQuantite(Loot_Enum.Petard) + " dans cette scène");



		//box principale
		GUI.Box(fc, "");

		//sous box
		GUI.Box(new Rect(fc.xMin+10,  fc.yMin+10, 100, 428), "RESSOURCES");
		GUI.Box(new Rect(fc.xMin+10,  fc.yMin+440, 100, 130), "");
		GUI.Box(new Rect(fc.xMin+120, fc.yMin+10, 650, 180), "ARMES");
		GUI.Box(new Rect(fc.xMin+120, fc.yMin+200,650, 180), "ARMURES");
		GUI.Box(new Rect(fc.xMin+120, fc.yMin+390,230, 180), "REPARATION");
		GUI.Box(new Rect(fc.xMin+360, fc.yMin+390,320, 180), "LEGENDE");

		//ressources
		GUI.Label(new Rect(fc.xMin+20, fc.yMin+30,100, 50), new GUIContent(" x " + resScotch, txScotch));
		GUI.Label(new Rect(fc.xMin+20, fc.yMin+75,100, 50), new GUIContent(" x " + resClous, txClous));
		GUI.Label(new Rect(fc.xMin+20, fc.yMin+120,100, 50), new GUIContent(" x " + resBois, txPlanche));
		GUI.Label(new Rect(fc.xMin+20, fc.yMin+165,100, 50), new GUIContent(" x " + resMetal, txPlaqMetal));
		GUI.Label(new Rect(fc.xMin+20, fc.yMin+210,100, 50), new GUIContent(" x " + resCarton, txCarton));
		GUI.Label(new Rect(fc.xMin+20, fc.yMin+255,100, 50), new GUIContent(" x " + resTube, txTube));
		GUI.Label(new Rect(fc.xMin+20, fc.yMin+300,100, 50), new GUIContent(" x " + resUstensile, txUstensile));
		GUI.Label(new Rect(fc.xMin+20, fc.yMin+345,100, 50), new GUIContent(" x " + resGaz, txGaz));
		GUI.Label(new Rect(fc.xMin+20, fc.yMin+390,100, 50), new GUIContent(" x " + resElastisque, txElastique));
		GUI.Label(new Rect(fc.xMin+20, fc.yMin+435,100, 50), new GUIContent(" x " + resPatate, txPatate));
		GUI.Label(new Rect(fc.xMin+20, fc.yMin+480,100, 50), new GUIContent(" x " + resOeuf, txOeuf));
		GUI.Label(new Rect(fc.xMin+20, fc.yMin+525,100, 50), new GUIContent(" x " + resPetard, txPetard));

		//GUI Gun à patate
		GUI.Box(rc, "");
		GUI.Label(new Rect(rc.xMin+60, rc.yMin+8,80, 100),"Gun à patate");
		GUI.Label(new Rect(rc.xMin+13, rc.yMin+30,85, 60),txGunPatate);
		GUI.Label(new Rect(rc.xMin+110, rc.yMin+45,100, 40),new GUIContent(" x 10",txTube));
		GUI.Label(new Rect(rc.xMin+110, rc.yMin+85,100, 40),new GUIContent(" x 02",txGaz));
		if (GameStateManager.Instance.isPossessed(ArmeEnum.LancePatate)) GUI.color = Color.green;
		else if(resTube>=10 && resGaz>=2) GUI.color = Color.yellow;
		else GUI.color = Color.red;
		if (GUI.Button (new Rect (rc.xMin + 10, rc.yMin + 100, 80, 35), "BUY")  && GUI.color == Color.yellow) {
			GameStateManager.Instance.setPossessed(ArmeEnum.LancePatate);
			GameStateManager.Instance.soustractionQuantite(Loot_Enum.Tube_plastique, 10);
			GameStateManager.Instance.soustractionQuantite(Loot_Enum.Bonbonne_de_gaz, 2);
			AudioSource.PlayClipAtPoint(sonBuy, transform.position);
		}
		GUI.color = Color.white;

		//GUI Slingshot à oeuf pourri
		GUI.Box(new Rect(rc.xMin+210, rc.yMin,200, 150), "");
		GUI.Label(new Rect(rc.xMin+260, rc.yMin+8,150, 100),"Slingshot à oeuf");
		GUI.Label(new Rect(rc.xMin+223, rc.yMin+30,85, 60), txGunOeuf);
		GUI.Label(new Rect(rc.xMin+320, rc.yMin+30,100, 40),new GUIContent(" x 08",txPlanche));
		GUI.Label(new Rect(rc.xMin+320, rc.yMin+70,100, 40),new GUIContent(" x 10",txElastique));
		GUI.Label(new Rect(rc.xMin+320, rc.yMin+110,100, 40),new GUIContent(" x 10",txScotch));
		if (GameStateManager.Instance.isPossessed(ArmeEnum.LanceOeuf ))GUI.color = Color.green;
		else if(resBois>=8 && resElastisque>=10 && resScotch>=10) GUI.color = Color.yellow;
		else GUI.color = Color.red;
		if (GUI.Button (new Rect (rc.xMin + 220, rc.yMin + 100, 80, 35), "BUY") && GUI.color == Color.yellow) {
			GameStateManager.Instance.setPossessed(ArmeEnum.LanceOeuf);
			GameStateManager.Instance.soustractionQuantite(Loot_Enum.Planche_de_bois, 8);
			GameStateManager.Instance.soustractionQuantite(Loot_Enum.Elastique, 10);
			GameStateManager.Instance.soustractionQuantite(Loot_Enum.Scotch, 10);
			AudioSource.PlayClipAtPoint(sonBuy, transform.position);
		}
		GUI.color = Color.white;

		//GUI catapulte à pétrard
		GUI.Box(new Rect(rc.xMin+420, rc.yMin,200, 150), "");
		GUI.Label(new Rect(rc.xMin+450, rc.yMin+8,150, 100),"Catapulte à pétard");
		GUI.Label(new Rect(rc.xMin+433, rc.yMin+30,85, 60), txGunPetard);
		GUI.Label(new Rect(rc.xMin+530, rc.yMin+30,100, 40),new GUIContent(" x 15",txUstensile));
		GUI.Label(new Rect(rc.xMin+530, rc.yMin+70,100, 40),new GUIContent(" x 10",txElastique));
		GUI.Label(new Rect(rc.xMin+530, rc.yMin+110,100, 40),new GUIContent(" x 10",txPlaqMetal));
		if (GameStateManager.Instance.isPossessed(ArmeEnum.LancePetard )) GUI.color = Color.green;
		else if(resUstensile>=15 && resElastisque>=10 && resMetal>=10) GUI.color = Color.yellow;
		else GUI.color = Color.red;
		if (GUI.Button (new Rect (rc.xMin + 430, rc.yMin + 100, 80, 35), "BUY") && GUI.color == Color.yellow) {
			GameStateManager.Instance.setPossessed(ArmeEnum.LancePetard);
			GameStateManager.Instance.soustractionQuantite(Loot_Enum.Ustensile, 15);
			GameStateManager.Instance.soustractionQuantite(Loot_Enum.Elastique, 10);
			GameStateManager.Instance.soustractionQuantite(Loot_Enum.Plaque_de_metal, 10);
			AudioSource.PlayClipAtPoint(sonBuy, transform.position);
		}
		GUI.color = Color.white;

		//GUI armure en carton
		GUI.Box(new Rect(rc.xMin, rc.yMin+190,200, 150), "");
		GUI.Label(new Rect(rc.xMin+50, rc.yMin+198,150, 100),"armure en carton");
		GUI.Label(new Rect(rc.xMin+13, rc.yMin+220,85, 60), txArmCarton);
		GUI.Label(new Rect(rc.xMin+110, rc.yMin+230,100, 40),new GUIContent(" x 15",txCarton));
		GUI.Label(new Rect(rc.xMin+110, rc.yMin+275,100, 40),new GUIContent(" x 15",txScotch));
		if (GameStateManager.Instance.isPossessed(ArmureEnum.ArmureCarton )) GUI.color = Color.green;
		else if(resCarton>=15 && resScotch>=15) GUI.color = Color.yellow;
		else GUI.color = Color.red;
		if (GUI.Button (new Rect (rc.xMin + 10, rc.yMin + 290, 80, 35), "BUY") && GUI.color == Color.yellow) {
			GameStateManager.Instance.setPossessed(ArmureEnum.ArmureCarton);
			GameStateManager.Instance.soustractionQuantite(Loot_Enum.Carton, 15);
			GameStateManager.Instance.soustractionQuantite(Loot_Enum.Scotch, 15);
            GameStateManager.Instance.setVie(GameStateManager.Instance.getVie() + 100);
			AudioSource.PlayClipAtPoint(sonBuy, transform.position);
		}
		GUI.color = Color.white;

		//GUI armure de bois
		GUI.Box(new Rect(rc.xMin+210, rc.yMin+190,200, 150), "");
		GUI.Label(new Rect(rc.xMin+270, rc.yMin+198,150, 100),"armure de bois");
		GUI.Label(new Rect(rc.xMin+223, rc.yMin+220,85, 60), txArmBois);
		GUI.Label(new Rect(rc.xMin+320, rc.yMin+220,100, 40),new GUIContent(" x 08",txClous));
		GUI.Label(new Rect(rc.xMin+320, rc.yMin+260,100, 40),new GUIContent(" x 20",txScotch));
		GUI.Label(new Rect(rc.xMin+320, rc.yMin+300,100, 40),new GUIContent(" x 15",txPlanche));
		if (GameStateManager.Instance.isPossessed(ArmureEnum.ArmureBois )) GUI.color = Color.green;
		else if(resClous>=8 && resScotch>=20 && resBois>=15) GUI.color = Color.yellow;
		else GUI.color = Color.red;
		if (GUI.Button (new Rect (rc.xMin + 220, rc.yMin + 290, 80, 35), "BUY") && GUI.color == Color.yellow) {
			GameStateManager.Instance.setPossessed(ArmureEnum.ArmureBois);
			GameStateManager.Instance.soustractionQuantite(Loot_Enum.Clous, 8);
			GameStateManager.Instance.soustractionQuantite(Loot_Enum.Scotch, 20);
            GameStateManager.Instance.soustractionQuantite(Loot_Enum.Planche_de_bois, 15);
            GameStateManager.Instance.setVie(GameStateManager.Instance.getVie() + 200);
			AudioSource.PlayClipAtPoint(sonBuy, transform.position);
		}
		GUI.color = Color.white;

		//GUI armure en metal
		GUI.Box(new Rect(rc.xMin+420, rc.yMin+190,200, 150), "");
		GUI.Label(new Rect(rc.xMin+470, rc.yMin+198,150, 100),"armure en metal");
		GUI.Label(new Rect(rc.xMin+433, rc.yMin+220,85, 60), txArmMetal);
		GUI.Label(new Rect(rc.xMin+530, rc.yMin+220,100, 40),new GUIContent(" x 10",txPlaqMetal));
		GUI.Label(new Rect(rc.xMin+530, rc.yMin+260,100, 40),new GUIContent(" x 30",txScotch));
		GUI.Label(new Rect(rc.xMin+530, rc.yMin+300,100, 40),new GUIContent(" x 08",txUstensile));
		if (GameStateManager.Instance.isPossessed(ArmureEnum.ArmureMetal )) GUI.color = Color.green;
		else if(resMetal>=10 && resScotch>=30 && resUstensile>=8) GUI.color = Color.yellow;
		else GUI.color = Color.red;
		if (GUI.Button (new Rect (rc.xMin + 430, rc.yMin + 290, 80, 35), "BUY") && GUI.color == Color.yellow) {
			GameStateManager.Instance.setPossessed(ArmureEnum.ArmureMetal);
			GameStateManager.Instance.soustractionQuantite(Loot_Enum.Plaque_de_metal, 10);
			GameStateManager.Instance.soustractionQuantite(Loot_Enum.Scotch, 30);
            GameStateManager.Instance.soustractionQuantite(Loot_Enum.Ustensile, 8);
            GameStateManager.Instance.setVie(GameStateManager.Instance.getVie() + 400);
			AudioSource.PlayClipAtPoint(sonBuy, transform.position);
		}
		GUI.color = Color.white;

		//GUI reparation
		GUI.Box(new Rect(rc.xMin, rc.yMin+380,200, 150), "");
		GUI.Label(new Rect(rc.xMin+50, rc.yMin+385,150, 100),"possédé : " + GameStateManager.Instance.getNombreSoin());
		GUI.Label(new Rect(rc.xMin+13, rc.yMin+410,85, 60), txRepar);
		GUI.Label(new Rect(rc.xMin+110, rc.yMin+410,100, 40),new GUIContent(" x 02",txPlaqMetal));
		GUI.Label(new Rect(rc.xMin+110, rc.yMin+450,100, 40),new GUIContent(" x 01",txGaz));
		GUI.Label(new Rect(rc.xMin+110, rc.yMin+490,100, 40),new GUIContent(" x 03",txClous));
		if(resMetal>=2 && resGaz>=1 && resClous>=3) GUI.color = Color.yellow;
		else GUI.color = Color.red;
		if(GUI.Button(new Rect(rc.xMin+10, rc.yMin+480,80, 35), "BUY") && GUI.color == Color.yellow) {
			GameStateManager.Instance.AddSoin();
			GameStateManager.Instance.soustractionQuantite(Loot_Enum.Plaque_de_metal, 2);
			GameStateManager.Instance.soustractionQuantite(Loot_Enum.Bonbonne_de_gaz, 1);
            GameStateManager.Instance.soustractionQuantite(Loot_Enum.Clous, 3);
            GameStateManager.Instance.setVie(GameStateManager.Instance.getVie() + 10);
			AudioSource.PlayClipAtPoint(sonBuy, transform.position);
  		}
		GUI.color = Color.white;


		//legende
		GUI.color = Color.green;
		GUI.Button (new Rect (fc.xMin + 375, fc.yMin + 430, 80, 35), "BUY");
		GUI.color = Color.yellow;
		GUI.Button (new Rect (fc.xMin + 375, fc.yMin + 475, 80, 35), "BUY");
		GUI.color = Color.red;
		GUI.Button (new Rect (fc.xMin + 375, fc.yMin + 520, 80, 35), "BUY");
		GUI.color = Color.white;
		GUI.Label (new Rect (fc.xMin + 470, fc.yMin + 435, 300, 50), "DEJA ACHETE");
		GUI.Label (new Rect (fc.xMin + 470, fc.yMin + 480, 300, 50), "ACHETABLE");
		GUI.Label (new Rect (fc.xMin + 470, fc.yMin + 525, 300, 50), "PAS ASSEZ DE RESSOURCES");


		//continuer
		GUI.color = Color.green;
		if (GUI.Button (new Rect (fc.xMin + 690, fc.yMin + 390, 80, 180), "FINISH")) {
			//passage a la scene suivante
            GameStateManager.Instance.GameState = GameStateManager.GameStates.JeuDeShoot;
		}
		GUI.color = Color.white;

	}


}
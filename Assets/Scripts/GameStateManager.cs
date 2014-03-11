using UnityEngine;
//using UnityEditor;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Ce manager s'occupe du changement de scenes. Il permet en outre de mettre le jeu en pause
/// Mais aussi de le terminer (GameEnd). Il contient les éléments à garer au fur et à mesure
/// des changements de scènes.
/// </summary>
public class GameStateManager : MonoBehaviour
{
	#region Variables
	
	/// <summary>
	/// Instance de nous-meme, nous sommes donc indépendant de tout objet tier.
	/// </summary>
	private static GameStateManager instance;

    //private static AudioClip audio;
	
	public enum GameStates
	{
		LancementJeu,
		JeuDeShoot,
		Pause,
		MenuDeCraft,
		Quitter,
		Dead
	}
	
	/// <summary>
	/// Etat du jeu.
	/// </summary>
	private GameStates a_gameState;
	private GameStates a_ancienGameState;
	private int a_zoneDeJeu; //zone de jeu de la phase jeuDeShoot

	// Etat du joueur

	private int a_score;
	private int a_vie;
    //possession de ressources du joueur
    public static Dictionary<Loot_Enum, int> playerLoots;
	public static Dictionary<Loot_Enum, Texture2D> lootTx;
	public static Dictionary<ArmeEnum, Texture2D> ArmeTx;
	public static Dictionary<ArmeEnum, bool> isPossessedArme;
	public static Dictionary<ArmureEnum, Texture2D> ArmureTx;
	public static Dictionary<ArmureEnum, bool> isPossessedArmure;
	public static Dictionary<TypeMunition, Texture2D> MunitionTx;

    private int a_nombreSoin;

    //L'ensemble des images des ressources
    public Texture2D txGunPatate; public Texture2D txGunPetard; public Texture2D txGunOeuf;
    public Texture2D txArmCarton; public Texture2D txArmBois; public Texture2D txArmMetal;
    public Texture2D txSoin;
    public Texture2D txGunBase; public Texture2D txArmBase;
    public Texture2D txDuckTape; public Texture2D txClous; public Texture2D txPlanche;
    public Texture2D txPlaqMetal; public Texture2D txCarton; public Texture2D txTube;
    public Texture2D txUstensile; public Texture2D txGaz; public Texture2D txElastique;
    public Texture2D txBoulette; public Texture2D txPatate2; public Texture2D txOeuf2;
    public Texture2D txPetard2;

    public Texture2D txPatate; public Texture2D txOeuf; public Texture2D txPetard;	


	#endregion

	void Awake()
	{
        //audio = new AudioClip
		//Permet de garder toutes les variables entre les scènes
		DontDestroyOnLoad(this);

        a_score = 0;
        a_nombreSoin = 0;
        a_vie = 100;
		a_gameState = GameStates.LancementJeu;
		a_zoneDeJeu = 0;
        a_ancienGameState = GameStates.LancementJeu;

        MunitionTx = new Dictionary<TypeMunition, Texture2D>();
        MunitionTx.Add(TypeMunition.BoulettePapier, txBoulette);
        MunitionTx.Add(TypeMunition.Patate, txPatate2);
        MunitionTx.Add(TypeMunition.Oeuf, txOeuf2);
        MunitionTx.Add(TypeMunition.Petard, txPetard2);

        ArmeTx = new Dictionary<ArmeEnum, Texture2D>();
        ArmeTx.Add(ArmeEnum.LanceBoulette, txGunBase);
        ArmeTx.Add(ArmeEnum.LancePatate, txGunPatate);
        ArmeTx.Add(ArmeEnum.LanceOeuf, txGunOeuf);
        ArmeTx.Add(ArmeEnum.LancePetard, txGunPetard);

        ArmureTx = new Dictionary<ArmureEnum, Texture2D>();
        ArmureTx.Add(ArmureEnum.Aucune, txArmBase);
        ArmureTx.Add(ArmureEnum.ArmureCarton, txArmCarton);
        ArmureTx.Add(ArmureEnum.ArmureBois, txArmBois);
        ArmureTx.Add(ArmureEnum.ArmureMetal, txArmMetal);

        isPossessedArme = new Dictionary<ArmeEnum, bool>();
        isPossessedArme.Add(ArmeEnum.LanceBoulette, true);
        isPossessedArme.Add(ArmeEnum.LancePatate, false);
        isPossessedArme.Add(ArmeEnum.LanceOeuf, false);
        isPossessedArme.Add(ArmeEnum.LancePetard, false);


        isPossessedArmure = new Dictionary<ArmureEnum, bool>();
        isPossessedArmure.Add(ArmureEnum.Aucune, true);
        isPossessedArmure.Add(ArmureEnum.ArmureCarton, false);
        isPossessedArmure.Add(ArmureEnum.ArmureBois, false);
        isPossessedArmure.Add(ArmureEnum.ArmureMetal, false);


        playerLoots = new Dictionary<Loot_Enum, int>();
        playerLoots.Add(Loot_Enum.Bonbonne_de_gaz, 20);
        playerLoots.Add(Loot_Enum.Carton, 20);
        playerLoots.Add(Loot_Enum.Clous, 20);
        playerLoots.Add(Loot_Enum.Elastique, 20);
        playerLoots.Add(Loot_Enum.Pattate, 20);
        playerLoots.Add(Loot_Enum.Petard, 20);
        playerLoots.Add(Loot_Enum.Plaque_de_metal, 20);
        playerLoots.Add(Loot_Enum.Planche_de_bois, 20);
        playerLoots.Add(Loot_Enum.Oeuf_Pourri, 20);
        playerLoots.Add(Loot_Enum.Tube_plastique, 20);
        playerLoots.Add(Loot_Enum.Scotch, 20);
        playerLoots.Add(Loot_Enum.Ustensile, 20);

        lootTx = new Dictionary<Loot_Enum, Texture2D>();
        lootTx.Add(Loot_Enum.Bonbonne_de_gaz, txGaz);
        lootTx.Add(Loot_Enum.Carton, txCarton);
        lootTx.Add(Loot_Enum.Clous, txClous);
        lootTx.Add(Loot_Enum.Elastique, txElastique);
        lootTx.Add(Loot_Enum.Pattate, txPatate);
        lootTx.Add(Loot_Enum.Petard, txPetard);
        lootTx.Add(Loot_Enum.Plaque_de_metal, txPlaqMetal);
        lootTx.Add(Loot_Enum.Planche_de_bois, txPlanche);
        lootTx.Add(Loot_Enum.Oeuf_Pourri, txOeuf);
        lootTx.Add(Loot_Enum.Tube_plastique, txTube);
        lootTx.Add(Loot_Enum.Scotch, txDuckTape);
        lootTx.Add(Loot_Enum.Ustensile, txUstensile);
	}
	
	public void GestionPause()
	{
        
		if(a_gameState != GameStates.Pause)
		{
			a_ancienGameState = a_gameState;
			a_gameState = GameStates.Pause;
		}else if(a_gameState == GameStates.Pause)
		{
			GameStates temp = a_gameState;
			a_gameState = a_ancienGameState;
			a_ancienGameState = temp;
		}
	}

	
	#region Accesseurs
	
	/// <summary>
	/// Accesseur permettant la création d'une instance de nous-meme.
	/// Si nous n'existons pas puis la renvoie.
	/// </summary>
	/// <value>L'instance indépendante du GameStatesManager.</value>
	public static GameStateManager Instance
	{
		get
		{
			if(instance == null)
				instance = new GameObject("GameStatesManager").AddComponent<GameStateManager>();
			return instance;
		}
	}




	/// <summary>
	/// Renvoie l'état du jeu actuel.
	/// </summary>
	/// <returns>L'état du jeu.</returns>
	public GameStates GameState
	{
		get
		{
			return this.a_gameState;
		}
		set 
		{ 
			a_ancienGameState = a_gameState;
			a_gameState = value; 
			if(a_gameState == GameStates.JeuDeShoot)
			{
				a_zoneDeJeu++;
				//Debug.Log (a_zoneDeJeu);
				Application.LoadLevel("ScenePrincipale");
			}else if(a_gameState == GameStates.MenuDeCraft)
			{
				Application.LoadLevel("SceneCrafting");
			}
			else if (a_gameState == GameStates.Dead){
				Application.LoadLevel("GameOverScene");
			}

		}
	}

	/// <summary>
	/// Permet de savoir si on a gagner ou perdu le dernier niveau.
	/// </summary>
	/// <value><c>true</c> si oui <c>false</c> sinon.</value>
	public int ZoneDeJeu
	{
		get
		{
			return this.a_zoneDeJeu;
		}
		set 
		{ 
			a_zoneDeJeu = value; 
		}
	}

	

	public int getScore()
	{
		return a_score;
	}

	public void setScore(int a)
	{
		a_score = a;
	}

	public void setVie(int i)
	{
		a_vie =i;
	}

	public int getVie()
	{
		return a_vie;
	}



    public void setPossessed(ArmureEnum a)
    {
        isPossessedArmure[a] = true;
    }

    public void setPossessed(ArmeEnum a)
    {
        isPossessedArme[a] = true;
    }

    public bool isPossessed(ArmureEnum type)
    {
        return isPossessedArmure[type];
    }

    public bool isPossessed(ArmeEnum type)
    {
        return isPossessedArme[type];
    }

    public void AddQuantite(Loot_Enum type, int quantite)
    {
        playerLoots[type] += quantite;
		/*Debug.Log ("ajout de " + quantite +  " de " + type);
		Debug.Log ("quantité un fois ajouté " + getQuantite(type));
		Debug.Log ("quantité de patate " + getQuantite(Loot_Enum.Pattate));*/
    }

    public int getQuantite(Loot_Enum type)
    {
        return playerLoots[type];
    }

    public int getNombreSoin()
    {
        return a_nombreSoin;
    }

    public void UtiliserSoin()
    {
        a_nombreSoin--;
    }

    public void AddSoin()
    {
        a_nombreSoin++;
    }

    public bool soustractionQuantite(Loot_Enum type, int aEnlever)
    {
        if (playerLoots[type] - aEnlever < 0)
        {
            return false;
        }
        else
        {

            playerLoots[type] -= aEnlever;
            return true;
        }
    }

    public Texture2D getTexture2D(Loot_Enum l)
    {
        return lootTx[l];
    }

    public Texture2D getTexture2D(ArmeEnum a)
    {
        return ArmeTx[a];
    }


    


    public Texture2D getTexture2DSoin()
    {
        return txSoin;
    }

    public Texture2D getTexture2D(ArmureEnum a)
    {
        return ArmureTx[a];
    }

    public Texture2D getTexture2D(TypeMunition a)
    {
        return MunitionTx[a];
    }

	#endregion
}

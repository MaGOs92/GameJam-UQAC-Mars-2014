using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreeationdeLoot : MonoBehaviour 
{
	public Transform perso;
	private Transform plop;

	public Transform a_prefabScotch;
	public Transform a_prefabClous; 
	public Transform a_prefabPlanche;
	public Transform a_prefabPlaqMetal; 
	public Transform a_prefabCarton; 
	public Transform a_prefabTube;
	public Transform a_prefabUstensile;
	public Transform a_prefabGaz;
	public Transform a_prefabElastique;
	public Transform a_prefabPatate;
	public Transform a_prefabOeuf; 
	public Transform a_prefabPetard;


	//Permettra d'assurer le meme genre d'accés que le GameStateManager
	private static CreeationdeLoot instanceLoot = null;

	// Use this for initialization
	void Start ()
    {
        instanceLoot = this;
        //Debug.Log("start");
		//Permet de garder toutes les variables entre les scènes
        //DontDestroyOnLoad(this);
        //Debug.Log(loots[Loot_Enum.Scotch]);

		Vector3 v = new Vector3(0,-4,0);
		plop = (Transform)Instantiate(perso, v, Quaternion.identity);

		Vector3 v2 = new Vector3(0,3,0);
		//Transform trans = (Transform)Instantiate(a_prefabScotch, v2, Quaternion.identity);
		//trans.GetComponent<Loot>().setQuantite(5);
		//trans.GetComponent<Loot>().setTypeLoot(Loot_Enum.Scotch);
		
	}




	public static CreeationdeLoot Instance
	{
		get
		{
			if(instanceLoot == null)
				instanceLoot = new GameObject("CreeationDeLoot").AddComponent<CreeationdeLoot>();
			return instanceLoot;
		}
	}

	// Update is called once per frame
	void Update () 
	{

        //Debug.Log(loots[Loot_Enum.Scotch]);
		if(GameStateManager.Instance.GameState != GameStateManager.GameStates.JeuDeShoot)
			return;


	}


	public void CreerLoot(Vector3 pos, int quantite)
    {
        //Debug.Log(loots[Loot_Enum.Scotch]);
        if (Random.value > 0.8f)
        {
            float r = Random.value;
            //Cas du Scotch
            if (r < 0.08)
            {
                Transform trans = (Transform)Instantiate(a_prefabScotch, pos, Quaternion.identity);

                trans.GetComponent<Loot>().setQuantite(quantite);
                trans.GetComponent<Loot>().setTypeLoot(Loot_Enum.Scotch);
                //Cas du Clous
            }
            else if (r >= 0.08 && r < 0.16)
            {
                Transform trans = (Transform)Instantiate(a_prefabClous, pos, Quaternion.identity);
                trans.GetComponent<Loot>().setQuantite(quantite);
                trans.GetComponent<Loot>().setTypeLoot(Loot_Enum.Clous);
                //Cas de la Planche de bois
            }
            else if (r >= 0.16 && r < 0.24)
            {
                Transform trans = (Transform)Instantiate(a_prefabPlanche, pos, Quaternion.identity);
                trans.GetComponent<Loot>().setQuantite(quantite);
                trans.GetComponent<Loot>().setTypeLoot(Loot_Enum.Planche_de_bois);
                //Cas du bout de carton
            }
            else if (r >= 0.24 && r < 0.32)
            {
                Transform trans = (Transform)Instantiate(a_prefabCarton, pos, Quaternion.identity);
                trans.GetComponent<Loot>().setQuantite(quantite);
                trans.GetComponent<Loot>().setTypeLoot(Loot_Enum.Carton);
                //Cas de la Plaque de Métal
            }
            else if (r >= 0.32 && r < 0.40)
            {
                Transform trans = (Transform)Instantiate(a_prefabPlaqMetal, pos, Quaternion.identity);
                trans.GetComponent<Loot>().setQuantite(quantite);
                trans.GetComponent<Loot>().setTypeLoot(Loot_Enum.Plaque_de_metal);
                //Cas du tube pvc
            }
            else if (r >= 0.40 && r < 0.48)
            {
                Transform trans = (Transform)Instantiate(a_prefabTube, pos, Quaternion.identity);
                trans.GetComponent<Loot>().setQuantite(quantite);
                trans.GetComponent<Loot>().setTypeLoot(Loot_Enum.Tube_plastique);
                //Cas des ustensiles
            }
            else if (r >= 0.48 && r < 0.56)
            {
                Transform trans = (Transform)Instantiate(a_prefabUstensile, pos, Quaternion.identity);
                trans.GetComponent<Loot>().setQuantite(quantite);
                trans.GetComponent<Loot>().setTypeLoot(Loot_Enum.Ustensile);
                //Cas de la bonbonne de gaz
            }
            else if (r >= 0.56 && r < 0.64)
            {
                Transform trans = (Transform)Instantiate(a_prefabGaz, pos, Quaternion.identity);
                trans.GetComponent<Loot>().setQuantite(quantite);
                trans.GetComponent<Loot>().setTypeLoot(Loot_Enum.Bonbonne_de_gaz);
                //Cas de l'elastique
            }
            else if (r >= 0.64 && r < 0.72)
            {
                Transform trans = (Transform)Instantiate(a_prefabElastique, pos, Quaternion.identity);
                trans.GetComponent<Loot>().setQuantite(quantite);
                trans.GetComponent<Loot>().setTypeLoot(Loot_Enum.Elastique);
                //Cas de la patate
            }
            else if (r >= 0.72 && r < 0.80)
            {
                Transform trans = (Transform)Instantiate(a_prefabPatate, pos, Quaternion.identity);
                trans.GetComponent<Loot>().setQuantite(quantite * 10);
                trans.GetComponent<Loot>().setTypeLoot(Loot_Enum.Pattate);
                //Cas de l'oeuf
            }
            else if (r >= 0.80 && r < 0.88)
            {
                Transform trans = (Transform)Instantiate(a_prefabOeuf, pos, Quaternion.identity);
                trans.GetComponent<Loot>().setQuantite(quantite * 5);
                trans.GetComponent<Loot>().setTypeLoot(Loot_Enum.Oeuf_Pourri);
                //Cas du pétard
            }
            else if (r >= 0.88 && r < 0.96)
            {
                Transform trans = (Transform)Instantiate(a_prefabPetard, pos, Quaternion.identity);
                trans.GetComponent<Loot>().setQuantite(quantite);
                trans.GetComponent<Loot>().setTypeLoot(Loot_Enum.Petard);
            }
        }




	}

}

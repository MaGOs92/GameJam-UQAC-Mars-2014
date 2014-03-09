using UnityEngine;
using System.Collections;

public class Loot : MonoBehaviour 
{
	//attributs
	private Loot_Enum a_typeLoot;
	private int a_quantite;
	private Vector3 a_positionCible;


	private Transform a_playerPrefab;
	public float a_vitesseAugmente;

	// Use this for initialization
	void Start () 
	{
		a_vitesseAugmente =0;
		//a_typeLoot = Loot_Enum.Scotch;
		//a_quantite = 1;
		a_positionCible = Vector3.zero;
		a_playerPrefab = GameObject.FindGameObjectWithTag("Player").transform;
		a_positionCible = a_playerPrefab.transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(GameStateManager.Instance.GameState != GameStateManager.GameStates.JeuDeShoot)
			return;

		a_vitesseAugmente += 1.7f * Time.deltaTime;
		a_positionCible = a_playerPrefab.transform.position;
		if(a_positionCible==Vector3.zero)return;
		float m_Distance = Vector3.Distance( a_positionCible, this.transform.position);
		if ( m_Distance > 1 ) 
		{ 
			Vector3 delta = a_positionCible - this.transform.position;
			delta.Normalize();
			
			float speed = a_vitesseAugmente * Time.deltaTime;
			this.transform.position = this.transform.position + (delta * speed);
		}
		else
		{
			try
			{
				GameStateManager.Instance.AddQuantite(a_typeLoot,a_quantite);
				Destroy(gameObject);
			}catch(System.Exception e)
			{
				Debug.Log(e.Message);
			}
		}
	}

	//Gette/Setter
	public void setTypeLoot(Loot_Enum l)
	{
		a_typeLoot = l;
	}

	public void setQuantite(int q)
	{
		a_quantite = q;
	}

}

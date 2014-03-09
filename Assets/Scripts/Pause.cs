using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pause : MonoBehaviour 
{
	float time;

	// Use this for initialization
	void Start () 
	{
		time = Time.time;
		//Debug.Log(GameStateManager.Instance.GameState);
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Input.GetKe
		if (Time.time-time > 7.0f)
		{
			GameStateManager.Instance.GameState = GameStateManager.GameStates.MenuDeCraft;
		}
		if (Input.GetKeyDown("p"))
		{
			
			GameStateManager.Instance.GestionPause();
		}
	}


}

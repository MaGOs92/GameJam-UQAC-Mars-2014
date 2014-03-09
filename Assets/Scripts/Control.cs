using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Control : MonoBehaviour {

    public Transform ennemy;

    public static Transform player;

    public static List<Transform> Loots;
    public static Transform loot1;

    public bool playing;
    public float spawnRate;
    public float lastSpawn;

	// Use this for initialization
	void Start () {
        playing = false;
        spawnRate = 1.2f;
        lastSpawn = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(GameStateManager.Instance.GameState != GameStateManager.GameStates.JeuDeShoot)
			return;

        var dt = Time.deltaTime;
        if (playing)
        {
            lastSpawn += dt;
            if (lastSpawn >= spawnRate)
            {
                lastSpawn = 0;
                var a = (Transform)Instantiate(ennemy);
            }
        }

        
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum CreepType{Slow,ZigZag,Cross,Looper};

public class EnnemyManager : MonoBehaviour 
{

	public static int killed;

	public Transform creepSlow;
	public Transform creepZigZag;
	public Transform creepCross;
	public Transform creepLooper;

    public Dictionary<CreepType,Transform> creeps;

    public int hasToSpawn;
    public int spawned;
    public float spawnRate;
    public float lastSpawn;

    public float rafaleChance;
    public int rafMax;
    public int rafMin;
    public float rafaleSpawnRate;
    public float rafaleLastSpawn;
    public int numRaf;
    public List<int> rafalesCreeps;
    public List<CreepType> rafalesTypes;
    public List<float> rafalesPoints;

	//condition de réussite
	private bool isFini;

	// Use this for initialization
	void Start () 
	{
		killed =0;
        var level = GameStateManager.Instance.ZoneDeJeu;
        //level = 200;
		isFini = false;
        hasToSpawn = (int)(30 * Mathf.Pow(1.03f, level));
        spawned = 0;
        spawnRate = 1.5f * (Mathf.Pow(0.99f,level)+0.6f);
        lastSpawn = 0;

        numRaf = 0;

        rafMax = (int)(10 * Mathf.Pow(1.03f, level));
        rafMin = (int)(5 * Mathf.Pow(1.03f, level));

        rafaleChance = 5.0f + 20 * (1 - Mathf.Exp(-level/50));

        rafaleSpawnRate = 0.57f * (Mathf.Pow(0.99f, level) + 0.6f);
        rafaleLastSpawn = 0;
        rafalesPoints = new List<float>();
        rafalesTypes = new List<CreepType>();
        rafalesCreeps = new List<int>();

        creeps = new Dictionary<CreepType, Transform>();
        creeps.Add(CreepType.ZigZag, creepZigZag);
        creeps.Add(CreepType.Cross, creepCross);
        creeps.Add(CreepType.Looper, creepLooper);
        creeps.Add(CreepType.Slow, creepSlow);

	}
	
	// Update is called once per frame
	void Update () 
	{
		if(GameStateManager.Instance.GameState != GameStateManager.GameStates.JeuDeShoot)
			return;
        var dt = Time.deltaTime;
		if(isFini ==true)
			Debug.Log ("c'est finis");

        spawnCreeps(dt);

        manageRafales(dt);

		VerifSiFinDeZone();



	}

    private void manageRafales(float dt)
    {
        rafaleLastSpawn += dt;
        if (rafaleLastSpawn > rafaleSpawnRate)
        {
            rafaleLastSpawn = 0;
            for (int i = 0; i < rafalesCreeps.Count; i++)
            {
                if (rafalesCreeps[i] > 0)
                {
                    rafalesCreeps[i]--;
                    var a = (Transform)Instantiate(creeps[rafalesTypes[i]]);
                    a.gameObject.GetComponent<Ennemy>().type = rafalesTypes[i];
                    a.transform.position = new Vector3(rafalesPoints[i], 6);
                    creepSpecificity(a);
                }
            }
        }
    }

	public void VerifSiFinDeZone()
	{
		//Si l'ennemyManager à envoyer toutes ses ia alors il a finit sont boulot

		if( killed >= hasToSpawn)
		{
			isFini = true;
		}

		//Si la logique du niveau est finit, alors on informe le gameStateManager
		if(isFini == true)
		{
			GameStateManager.Instance.GameState = GameStateManager.GameStates.MenuDeCraft;
		}
	}

    void spawnCreeps(float dt)
    {
        lastSpawn += dt;
        if (spawned < hasToSpawn)
        {
            if (lastSpawn >= spawnRate)
            {
                lastSpawn = 0;
                var ran = Random.Range(0, 100);
                if (ran > rafaleChance)
                {
                    spawnSingle();
                    spawned++;
                }
                else
                {
                    var rafNb = Random.Range(rafMin, rafMax);
                    spawnRafale(rafNb);
                    spawned += rafNb;
                }
            }
        }
    }

    void spawnSingle()
    {
        var t = gimmeACreep();
        var a = (Transform)Instantiate(creeps[t]);
        a.gameObject.GetComponent<Ennemy>().type = t;
        a.transform.position = new Vector3(Random.Range(-6,6), 6);
    }

    void spawnRafale(int nb)
    {
        //Debug.Log("Rafale !");
        rafalesTypes.Add(gimmeACreep());
        rafalesPoints.Add(Random.Range(-5, 5));
        rafalesCreeps.Add(nb);
    }

    CreepType gimmeACreep()
    {
        var x = Random.Range(0, 4);
        if (x < 1) return CreepType.Cross;
        if (x >= 1 && x < 2) return CreepType.Looper;
        if (x >= 2 && x < 3) return CreepType.Slow;
        else return CreepType.ZigZag;
    }

    public void creepSpecificity(Transform creep)
    {
    }
}

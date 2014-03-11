using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	/*public Transform a_prefabBoulettePapier;
	public Transform a_prefabPatate;
	public Transform a_prefabOeuf;
	public Transform a_prefabPetard;*/

    public float x;
    public float y;

    public float v;
    public Vector3 dir;

    public bool hostile;

	// Use this for initialization
    void Start()
    {
    }
	
	// Update is called once per frame
	void Update () 
	{
		if(GameStateManager.Instance.GameState != GameStateManager.GameStates.JeuDeShoot)
			return;

        var dt = Time.deltaTime;
        transform.position += dir * v * dt;
        if (Mathf.Abs(transform.position.y) > 10 || Mathf.Abs(transform.position.x) > 10)
        {
            Destroy(gameObject);
        }
        if (hostile && Vector3.Distance(ControlPlayer.player.transform.position, transform.position) < 0.5)
        {
			GameStateManager.Instance.setVie(GameStateManager.Instance.getVie() - 10);
            Destroy(gameObject);
        }
        if(!hostile){
            foreach (var a in FindObjectsOfType<Ennemy>())
            {
                if (Vector3.Distance(a.transform.position, transform.position) < 0.9/*a.rayonCollision*/)
                {
                    Destroy(a.gameObject);
					EnnemyManager.killed++;
					GameStateManager.Instance.setScore(GameStateManager.Instance.getScore() + 100);
					CreeationdeLoot.Instance.CreerLoot(this.transform.position,(int)(Random.value*5+1));
                    Destroy(gameObject);
                }
            }
        }
	}

	void OnTriggerEnter(Collider other)
	{
		/*if(!hostile){
			Debug.Log ("coucou");
			Destroy(other.gameObject);
			EnnemyManager.killed++;
			GameStateManager.Instance.setScore(GameStateManager.Instance.getScore() + 100);
			CreeationdeLoot.Instance.CreerLoot(this.transform.position,(int)(Random.value*5+1));
			Destroy(gameObject);
		}*/

	}

}
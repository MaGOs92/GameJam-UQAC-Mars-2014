using UnityEngine;
using System.Collections;

public class Ennemy : MonoBehaviour
{
    public Transform projectile;


    public float v;
    public Vector3 dir;

    public bool die;

    public float time;

    public float fireRate;
    public float lastShoot;
    public float rayonCollision;

    public CreepType type;


    // Use this for initialization
    void Start()
    {
        dir = new Vector3(Mathf.Sin(2 * transform.position.y), -1);
        dir.Normalize();
        v = 1;
        fireRate = 1.8f;
        lastShoot = 0;
        rayonCollision = 0.3f;
        time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
		if(GameStateManager.Instance.GameState != GameStateManager.GameStates.JeuDeShoot)
			return;

        var dt = Time.deltaTime;
        lastShoot += dt;
        if (lastShoot >= fireRate)
        {
            shoot();
            lastShoot = 0;
        }
        move(dt);
        checkDeath();
    }

    void shoot()
    {
        var a = (Transform)Instantiate(projectile);
        a.position = transform.position;
        var b = a.GetComponent<Projectile>();
        b.dir = new Vector3(0, -1);
        b.v = 3;
        b.hostile = true;
    }

    void OnTriggerEnter(Collider other)
    {
        Die();
    }

	void OnCollisionEnter(Collision colli)
	{
		Die ();
	}

    void move(float dt)
    {
        time += dt;
        switch (type)
        {
            case CreepType.ZigZag:
                dir = new Vector3(Mathf.Sin(3*time), -1);
                if (Mathf.Abs((transform.position + (dir * v * dt)).x) > 6)
                {
                    dir = new Vector3(-Mathf.Sin(3*time), -1);
                }
                break;
            case CreepType.Slow:
                dir = new Vector3(0, -1);
                break;
            //case CreepType.Cross:

                break;
            case CreepType.Looper:
                
                dir = new Vector3(2 * Mathf.Sin(4*time), 2 * Mathf.Cos(2*time) - 1);
                if (Mathf.Abs((transform.position + (dir * v * dt)).x) > 6)
                {
                    dir = new Vector3(-2 * Mathf.Sin(4*time), 2 * Mathf.Cos(2*time) - 1);
                }
                break;
            default:
                dir = new Vector3(0, -1);

                break;
        }

        dir.Normalize();
        transform.position += dir * v * dt;
    }

    void checkDeath()
    {

        if (transform.position.y < -5)
        {
            Die();
        }
    }

	public void Die()
	{
		EnnemyManager.killed++;
		//CreeationdeLoot.Instance.CreerLoot(this.transform.position,(int)(Random.value*5+1));
		Destroy(gameObject);
	}
}

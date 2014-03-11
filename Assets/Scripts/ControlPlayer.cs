using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ArmeEnum { LanceBoulette, LancePatate, LancePetard, LanceOeuf }
public enum ArmureEnum { Aucune, ArmureCarton, ArmureBois, ArmureMetal }
public enum TypeMunition { BoulettePapier, Patate, Petard, Oeuf }

public class ControlPlayer : MonoBehaviour
{

    //rendre le player disponible à tous
    public static GameObject player;

    public Transform a_prefabBoulette;
    public Transform a_prefabPatate;
    public Transform a_prefabPetard;
    public Transform a_prefabOeuf;

    // Vitesse du vaisseau
    public float decay = 0.9f;
    public Vector3 dir;
    public float v;
    public float a;

    //Projectile
    public float vProj = 10;

    public float lastAngl;

    // Position de départ
    public float playerPosHStart = 0f;
    public float playerPosVStart = 0f;

    //Paramètres de tir
    public float fireRate;
    public float nextFire;
    public Transform bullet;



    // Gestion des armes

    private ArmeEnum armeSelected;
    private ArmureEnum armureSelected;

    //Gestion de la vie du joueur
    private bool isAlive;


    //sons
    public AudioClip pew;
    public AudioClip patate;
    public AudioClip oeuf;
    public AudioClip petard;



    void Start()
    {
        isAlive = true;
        player = gameObject;
        fireRate = 0.3f;
        armeSelected = ArmeEnum.LanceBoulette;
        armureSelected = ArmureEnum.Aucune;

        dir = new Vector3(1, 0);
        v = 5;
        a = 70;

        transform.FindChild("ArmureBois_A").active = false;
        transform.FindChild("ArmureCartonl_A").active = false;
        transform.FindChild("GunAPattate_W").active = false;
        transform.FindChild("catapulte_a_petard_W").active = false;
        transform.FindChild("SlingShoot_a_OeufPourris_W").active = false;


        //rotations : x,z,y
        transform.Rotate(new Vector3(1, 0, 0), 90);
        //transform.Rotate(new Vector3(0, 1, 0), 180);
        transform.Rotate(new Vector3(0, 0, 1), 180);
        lastAngl = 90;




    }


    // Update is called once per frame
    void Update()
    {
        if (GameStateManager.Instance.GameState != GameStateManager.GameStates.JeuDeShoot)
            return;

        var dt = Time.deltaTime;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 mousePos = Input.mousePosition;
        Vector3 targetPos = mousePos - objectPos;
        targetPos.z = transform.position.z;
        var vec = -(targetPos - transform.position);
        var angl = Vector3.Angle(vec, new Vector3(1, 0));
        transform.Rotate(new Vector3(0, 1, 0), angl - lastAngl);
        lastAngl = angl;

        v *= decay;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            v += a * dt;
            //Debug.Log("Droite !");
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            v -= a * dt;
        };

        // Bordures de l'écran limite en x


        transform.position += dir * dt * v;

        if (Mathf.Abs(transform.position.x) > 6)
        {
            transform.position = new Vector3(Mathf.Sign(transform.position.x) * 6, -4);
            v = -1 * v;
        }


        changementDArme();

        visuelArmure();

        shootPlayer(dt, targetPos);

        VerifIsAlive();
    }

    public void changementDArme()
    {

        // Boulette papier

        if (Input.GetKeyDown("a") || Input.GetKeyDown("q"))
        {
            armeSelected = ArmeEnum.LanceBoulette;
            transform.FindChild("ArmeDeBase_W").active = true;
            transform.FindChild("GunAPattate_W").active = false;
            transform.FindChild("SlingShoot_a_OeufPourris_W").active = false;
            transform.FindChild("catapulte_a_petard_W").active = false;
        }

        // Patate

        if (Input.GetKeyDown("w") || Input.GetKeyDown("z"))
        {
            if (GameStateManager.Instance.isPossessed(ArmeEnum.LancePatate))
            {
                armeSelected = ArmeEnum.LancePatate;
                transform.FindChild("ArmeDeBase_W").active = false;
                transform.FindChild("GunAPattate_W").active = true;
                transform.FindChild("SlingShoot_a_OeufPourris_W").active = false;
                transform.FindChild("catapulte_a_petard_W").active = false;
            }

        }


        // Oeuf

        if (Input.GetKeyDown("e"))
        {
            if (GameStateManager.Instance.isPossessed(ArmeEnum.LanceOeuf))
            {
                armeSelected = ArmeEnum.LanceOeuf;
                transform.FindChild("ArmeDeBase_W").active = false;
                transform.FindChild("GunAPattate_W").active = false;
                transform.FindChild("SlingShoot_a_OeufPourris_W").active = true;
                transform.FindChild("catapulte_a_petard_W").active = false;
            }
        }

        // Pétard

        if (Input.GetKeyDown("r"))
        {
            if (GameStateManager.Instance.isPossessed(ArmeEnum.LancePetard))
            {
                armeSelected = ArmeEnum.LancePetard;
                transform.FindChild("ArmeDeBase_W").active = false;
                transform.FindChild("GunAPattate_W").active = false;
                transform.FindChild("SlingShoot_a_OeufPourris_W").active = false;
                transform.FindChild("catapulte_a_petard_W").active = true;
            }
        }
    }


    public void visuelArmure()
    {
        if (GameStateManager.Instance.isPossessed(ArmureEnum.ArmureMetal))
        {
            armureSelected = ArmureEnum.ArmureMetal;
            transform.FindChild("ArmureCartonl_A").active = false;
            transform.FindChild("ArmureBois_A").active = false;
            transform.FindChild("ArmureMetal_A").active = true;
        }

        else if (GameStateManager.Instance.isPossessed(ArmureEnum.ArmureBois))
        {
            armureSelected = ArmureEnum.ArmureBois;
            transform.FindChild("ArmureCartonl_A").active = false;
            transform.FindChild("ArmureBois_A").active = true;
            transform.FindChild("ArmureMetal_A").active = false;
        }

        else if (GameStateManager.Instance.isPossessed(ArmureEnum.ArmureCarton))
        {
            armureSelected = ArmureEnum.ArmureCarton;
            transform.FindChild("ArmureCartonl_A").active = true;
            transform.FindChild("ArmureBois_A").active = false;
            transform.FindChild("ArmureMetal_A").active = false;
        }

        else
        {
            armureSelected = ArmureEnum.Aucune;
            transform.FindChild("ArmureCartonl_A").active = false;
            transform.FindChild("ArmureBois_A").active = false;
            transform.FindChild("ArmureMetal_A").active = false;
        }

    }


    public void VerifIsAlive()
    {
        if (GameStateManager.Instance.getVie() <= 0)
        {
            GameStateManager.Instance.setVie(0);
            isAlive = false;
            GameStateManager.Instance.GameState = GameStateManager.GameStates.Dead;
        }
    }

    public void ResetPosition()
    {
        float z = transform.position.z;
        this.transform.position = new Vector3(playerPosHStart, playerPosVStart, z);
    }

    public Vector3 positionPlayer()
    {
        return new Vector3(transform.position.x, transform.position.y, 0);
    }

    public void shootPlayer(float dt, Vector3 targetPos)
    {


        nextFire += dt;

        if (Input.GetButton("Fire1") && nextFire > fireRate)
        {

            nextFire = 0;
            Transform tir = null;
            switch (getSelectedArme())
            {
                case ArmeEnum.LanceBoulette:
                    tir = (Transform)Instantiate(a_prefabBoulette);
                    AudioSource.PlayClipAtPoint(pew, transform.position);
                    break;
                case ArmeEnum.LancePatate:
                    if (GameStateManager.Instance.getQuantite(Loot_Enum.Pattate) > 0)
                    {
                        tir = (Transform)Instantiate(a_prefabPatate);
                        AudioSource.PlayClipAtPoint(patate, transform.position);
                    }
                    break;
                case ArmeEnum.LanceOeuf:
                    if (GameStateManager.Instance.getQuantite(Loot_Enum.Oeuf_Pourri) > 0)
                    {
                        tir = (Transform)Instantiate(a_prefabOeuf);
                        AudioSource.PlayClipAtPoint(oeuf, transform.position);
                    }
                    break;
			case ArmeEnum.LancePetard:
                    if (GameStateManager.Instance.getQuantite(Loot_Enum.Petard) > 0)
                    {
                        tir = (Transform)Instantiate(a_prefabPetard);
                        AudioSource.PlayClipAtPoint(petard, transform.position);
                    }
                    break;

            }

            tir.transform.position = transform.position + new Vector3(-Mathf.Cos((lastAngl + 10) * Mathf.Deg2Rad), Mathf.Sin((lastAngl + 10) * Mathf.Deg2Rad));
            var proj = tir.GetComponent<Projectile>();
            proj.v = vProj;
            proj.hostile = false;
            proj.dir = targetPos;
            proj.dir.Normalize();
            proj.transform.LookAt(targetPos);
            proj.transform.Rotate(new Vector3(1, 0, 0), 90);

        }

		if (Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
		}
    }

    public ArmeEnum getSelectedArme()
    {
        return armeSelected;
    }

    public ArmureEnum getSelectedArmure()
    {
        return armureSelected;
    }



}

using UnityEngine;
using System.Collections;

public class Protector : PlayerManager {
  
	Animator myAnim;
	ParticleSystem taunt;
	public static bool protectorDocked;
	public static bool shielded = false; 
	GameObject shield;
	EnemyBehaviour enemyAI;
	PlayerControls pControls;
	public float drainTime, drainTimer;
	public int shieldUsage;
	
	void Awake()
	{
		shield = GameObject.Find ("Shield");
	}

	new void Start()
	{
		base.Start();
		pControls = GameObject.Find ("PlayerManager").GetComponent<PlayerControls>();
		currentFood = 0;
		health = 15;
		maxHealth = 15;
		myAnim = transform.GetChild (0).GetComponent<Animator>();
		healthTextGO = GameObject.Find ("P2HealthText");
		foodTextGO = GameObject.Find ("P2FoodText");
	}

	void Update()
	{
		Shield ();

		//will move later
		healthTextGO.GetComponent<GUIText>().text = health.ToString("0");
		foodTextGO.GetComponent<GUIText>().text = currentFood.ToString("0");
	}

	public void Taunt(Vector3 center, float radius, int foodCost)
	{
		protAnim.Play ("ProtTaunt");
		Collider[] hitColliders = Physics.OverlapSphere(center, radius);
		int i = 0;
		while (i < hitColliders.Length) 
		{
			if(hitColliders[i].tag == ("Enemy"))
			hitColliders[i].GetComponent<EnemyBehaviour>().currentState = EnemyBehaviour.EnemyStates.fleeing;
			i++;
		}
		currentFood += foodCost;
		myAnim.Play ("Taunt");
	}
	
	void Shield()
	{
		if(currentFood > 0)
		{
			if(pControls.playerDistanceReal() <= .75f)
			{
				shield.GetComponent<SphereCollider>().enabled = true;//child gameobject
				shield.GetComponent<SpriteRenderer>().enabled = true;
                shielded = true;
				drainTime += Time.deltaTime;
				if(drainTime >= drainTimer)
				{
					currentFood -= shieldUsage;
					drainTime = 0;
					drainTime += Time.deltaTime;
					if(drainTime >= drainTimer)
					{
						currentFood -= shieldUsage;
						drainTime = 0;
					}
				}
			}
			else
			{
				shield.GetComponent<SphereCollider>().enabled = false;//child gameobject
				shield.GetComponent<SpriteRenderer>().enabled = false;
			}

		}
		else
		{
			shield.GetComponent<SphereCollider>().enabled = false; //child gameobject
			shield.GetComponent<SpriteRenderer>().enabled = false;
            shielded = false;
		}
		if(currentFood <= 0)
		{
			currentFood = 0;
		}
	}

	public void AdjustHealth(int adj)
	{
		health += adj;
		
		if(health >= maxHealth)
		{
			health = maxHealth;
		}
		if(health <= 0)
		{
			Application.LoadLevel(Application.loadedLevelName);
		}
	}
}

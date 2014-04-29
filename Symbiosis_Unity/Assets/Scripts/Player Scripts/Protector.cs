using UnityEngine;
using System.Collections;

public class Protector : PlayerManager {
  
	Animator myAnim;
	ParticleSystem taunt;
	public static bool protectorDocked; 
	GameObject shield;
	EnemyBehaviour enemyAI;
	PlayerControls pControls;
	public float drainTime, drainTimer;
	public int shieldUsage;
	public GameObject tauntEffect;
	public static bool shieldActive;
	
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
		healthTextGO.GetComponent<GUIText>().text = health.ToString("0");
		foodTextGO.GetComponent<GUIText>().text = currentFood.ToString("0");
		if(shieldActive)
		{
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
			if(currentFood <= 0)
			{
				shieldActive = false;
				shield.GetComponent<SphereCollider>().enabled = false;//child gameobject
				shield.GetComponent<SpriteRenderer>().enabled = false;
				currentFood = 0;
			}
		}
	}

	public void Taunt(Vector3 center, float radius, int foodCost)
	{
		//PARTICLE
		//protAnim.CrossFade ("ProtTaunt", .4f);
		Collider[] hitColliders = Physics.OverlapSphere(center, radius);
		int i = 0;
		while (i < hitColliders.Length) 
		{
			if(hitColliders[i].tag == ("Enemy"))
			hitColliders[i].GetComponent<EnemyBehaviour>().currentState = EnemyBehaviour.EnemyStates.fleeing;
			i++;
		}
		currentFood += foodCost;
		GameObject tauntFX = Instantiate(tauntEffect, transform.position, Quaternion.identity)as GameObject;
		tauntFX.transform.parent = transform;
		audio.Play ();
		myAnim.Play ("Taunt");
	}
	
	public void Shield()
	{
		shieldActive = !shieldActive;
		if(shieldActive)
		{
			shield.GetComponent<SphereCollider>().enabled = true;//child gameobject
			shield.GetComponent<SpriteRenderer>().enabled = true;
		}
		else
		{
			shield.GetComponent<SphereCollider>().enabled = false;//child gameobject
			shield.GetComponent<SpriteRenderer>().enabled = false;
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

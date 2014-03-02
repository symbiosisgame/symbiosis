﻿using UnityEngine;
using System.Collections;

public class Protector : PlayerManager {
  
	Animator myAnim;
	ParticleSystem taunt;
	public static bool protectorDocked;
	public static bool shielded = false; //stores whether the player is pressing the key to use the shield or not. 
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
		myAnim = transform.GetChild (0).GetComponent<Animator>();
	}

	void Update()
	{
		Shield ();
	}

	public void Taunt(Vector3 center, float radius, int foodCost)
	{
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
			if(pControls.playerDistanceReal() <= .4f)
			{
				shield.GetComponent<SphereCollider>().enabled = true;//child gameobject
				shield.GetComponent<SpriteRenderer>().enabled = true;
			}
			drainTime += Time.deltaTime;
			if(drainTime >= drainTimer)
			{
				currentFood -= shieldUsage;
				drainTime = 0;
			}
		}
		else
		{
			shield.GetComponent<SphereCollider>().enabled = false;//child gameobject
			shield.GetComponent<SpriteRenderer>().enabled = false;
		}
		if(currentFood <= 0)
		{
			currentFood = 0;
		}
	}
}

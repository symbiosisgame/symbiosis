using UnityEngine;
using System.Collections;

public class Protector : PlayerManager {
  
	Animator myAnim;
	ParticleSystem taunt;
	public static bool protectorDocked;
	EnemyBehaviour enemyAI;

	new void Start()
	{
		currentFood = 0;
		myAnim = transform.GetChild (0).GetComponent<Animator>();
		taunt = GameObject.Find ("Taunt").GetComponent<ParticleSystem>();
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
		taunt.Play ();
	}
}

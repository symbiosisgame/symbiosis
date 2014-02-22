using UnityEngine;
using System.Collections;

public class Protector : PlayerManager {
  
	Animator myAnim;
	ParticleSystem taunt;
	public static bool protectorDocked;

	new void Start()
	{
		currentFood = 5;
		myAnim = transform.GetChild (0).GetComponent<Animator>();
		taunt = GameObject.Find ("Taunt").GetComponent<ParticleSystem>();
	}

	public void Taunt(Vector3 center, float radius, int foodCost)
	{
		Collider[] hitColliders = Physics.OverlapSphere(center, radius);
		int i = 0;
		while (i < hitColliders.Length) 
		{
			hitColliders[i].BroadcastMessage("TakeDamage", -1, SendMessageOptions.DontRequireReceiver);
			i++;
		}
		currentFood += foodCost;
		myAnim.Play ("Taunt");
		taunt.Play ();
	}
}

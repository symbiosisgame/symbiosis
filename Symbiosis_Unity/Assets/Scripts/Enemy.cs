using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public int health = 2;

	void TakeDamage(int dmg)
	{
		health += dmg;
		if(health <= 0)
		{
			Destroy(gameObject);
		}
	}
}

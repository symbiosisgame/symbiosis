using UnityEngine;
using System.Collections.Generic;

//Looks after the level. Checks husk cleanliness, handles timers, anything level related

public class LevelManager : MonoBehaviour {

	public int huskClean = 40;
	public int howMuchCharge;
	public float huskCleanRate = 1f;
	public float cleanTime, cleanTimer = 1f;
	public int husk1Clean, husk2Clean, husk3Clean, husk4Clean, husk5Clean;
	public int foodSpawnRate;
	public GameObject[] barriers;
	public GameObject[] chargeNodes;
	FoodSpawner foodSpawner;
	EnemySpawner enemySpawner;
	bool invoked;

	void Start () 
	{
		foodSpawner = GetComponent<FoodSpawner>();
		enemySpawner = GetComponent<EnemySpawner>();
	}

	void Update () 
	{
		CheckDocking();
		HuskCleaned();
	}

	void HuskCleaned()
	{
		if(huskClean == husk1Clean)
		{
			Destroy (barriers[0].gameObject);
			Destroy (chargeNodes[0].gameObject);
			Destroy (chargeNodes[1].gameObject);
			foodSpawner.whichHusk = FoodSpawner.Husk.second;
			enemySpawner.whichHusk = EnemySpawner.Husk.second;
			UnDock();
		}
		else if(huskClean == husk2Clean)
		{
			Destroy (barriers[1].gameObject);
			Destroy (chargeNodes[2].gameObject);
			Destroy (chargeNodes[3].gameObject);
			foodSpawner.whichHusk = FoodSpawner.Husk.third;
			enemySpawner.whichHusk = EnemySpawner.Husk.third;
			UnDock();
		}
		else if(huskClean == husk3Clean)
		{
			Destroy (barriers[2].gameObject);
			Destroy (chargeNodes[4].gameObject);
			Destroy (chargeNodes[5].gameObject);
			foodSpawner.whichHusk = FoodSpawner.Husk.forth;
			enemySpawner.whichHusk = EnemySpawner.Husk.forth;
			UnDock();
		}
		else if(huskClean == husk4Clean)
		{
			Destroy (barriers[3].gameObject);
			Destroy (chargeNodes[6].gameObject);
			Destroy (chargeNodes[7].gameObject);
			UnDock();
		}
		else if(huskClean == husk5Clean)
		{
			Destroy (barriers[4].gameObject);
			Destroy (chargeNodes[8].gameObject);
			Destroy (chargeNodes[9].gameObject);
			UnDock();
		}
	}

	void CheckDocking()
	{
		if(Feeder.feederDocked && Protector.protectorDocked)
		{
			cleanTime += huskCleanRate * Time.deltaTime;

			if(cleanTime >= cleanTimer)
			{
				audio.Play ();
				huskClean --;
				cleanTime = 0;
			}
			if(!invoked)
			{
				InvokeRepeating("CreateFood", 3f, 8f / huskCleanRate); //repeat rate set quite high for testing
				if(enemySpawner.whichHusk != EnemySpawner.Husk.first)
				{
					Debug.Log ("LOL ENEMY");
					InvokeRepeating("CreateEnemy", 2f, 4f / huskCleanRate); //repeat rate set quite high for testing
				}
				invoked = true;
			}
		}
		else
		{
			invoked = false;
			CancelInvoke("CreateFood");
			CancelInvoke("CreateEnemy");
		}
	}

	void CreateFood()
	{
		foodSpawner.SpawnFood();
	}

	void CreateEnemy()
	{
		enemySpawner.SpawnEnemy();
	}

	void UnDock()
	{
		Feeder.feederDocked = false;
		Protector.protectorDocked = false;
	}
}

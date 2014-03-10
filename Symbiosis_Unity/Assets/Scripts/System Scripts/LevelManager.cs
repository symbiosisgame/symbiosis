using UnityEngine;
using System.Collections.Generic;

//Looks after the level. Checks husk cleanliness, handles timers, anything level related

public class LevelManager : MonoBehaviour {

	public int huskClean = 40;
	public int howMuchCharge;
	public float huskCleanRate = 1f;
	public float cleanTime, cleanTimer = 1f;
	public int foodSpawnRate;
	GameObject barrier;
	FoodSpawner foodSpawner;
	EnemySpawner enemySpawner;
	bool invoked;

	void Start () 
	{
		barrier = GameObject.Find ("Barrier");
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
		if(huskClean == 0)
		{
			Destroy (barrier.gameObject);
			huskClean = 0;
		}
	}

	void CheckDocking()
	{
		if(Feeder.feederDocked && Protector.protectorDocked)
		{
			cleanTime += huskCleanRate * Time.deltaTime;
			if(cleanTime >= cleanTimer)
			{
				huskClean --;
				cleanTime = 0;
			}
			if(!invoked)
			{
				InvokeRepeating("CreateFood", 3f, 8f / huskCleanRate); //repeat rate set quite high for testing
				InvokeRepeating("CreateEnemy", 2f, 3f / huskCleanRate); //repeat rate set quite high for testing
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
}

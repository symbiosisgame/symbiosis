using UnityEngine;
using System.Collections.Generic;

//Looks after the level. Checks husk cleanliness, handles timers, anything level related

public class LevelManager : MonoBehaviour {

	static public int huskClean = 100;
	public float huskCleanRate = 1f;
	public float cleanTime, cleanTimer = 1f;
	public int foodSpawnRate;
	FoodSpawner foodSpawner;
	bool invoked;

	void Start () 
	{
		foodSpawner = GetComponent<FoodSpawner>();
	}

	void Update () 
	{
		CheckDocking();
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
			InvokeRepeating("CreateFood", 5f, 5f / huskCleanRate); //repeat rate set quite high for testing
			invoked = true;
		}
		else
		{
			invoked = false;
			CancelInvoke("CreateFood");
		}
	}

	void CreateFood()
	{
		foodSpawner.SpawnFood();
	}
}

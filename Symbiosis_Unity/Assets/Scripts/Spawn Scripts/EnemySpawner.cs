using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {

	public GameObject[] enemyPoints1, enemyPoints2, enemyPoints3, enemyPoints4;    
	Vector3[] enemyPointsPos1, enemyPointsPos2, enemyPointsPos3, enemyPointsPos4;
	public GameObject enemy;
	float[] range1, range2, range3, range4; 
	public List<GameObject> enemyList = new List<GameObject>();
	[HideInInspector]public enum Husk {first, second, third, forth};
	[HideInInspector]public Husk whichHusk;
	
	void Start ()
	{
		enemyPointsPos1 = new Vector3[enemyPoints1.Length];
		enemyPointsPos2 = new Vector3[enemyPoints2.Length];
		enemyPointsPos3 = new Vector3[enemyPoints3.Length];
		enemyPointsPos4 = new Vector3[enemyPoints4.Length];
		
		range1 = new float[enemyPoints1.Length];
		range2 = new float[enemyPoints2.Length];
		range3 = new float[enemyPoints3.Length];
		range4 = new float[enemyPoints4.Length];
		
		for(int i = 0; i < enemyPoints1.Length; i++)
		{
			//Gets the spawnradius for each FoodPoint
			range1[i] = enemyPoints1[i].GetComponent<FoodRange>().spawnRadius;
			enemyPointsPos1[i] = enemyPoints1[i].transform.position;
		}
		for(int i = 0; i < enemyPoints2.Length; i++)
		{
			//Gets the spawnradius for each FoodPoint
			range2[i] = enemyPoints2[i].GetComponent<FoodRange>().spawnRadius;
			enemyPointsPos2[i] = enemyPoints2[i].transform.position;
		}
		for(int i = 0; i < enemyPoints3.Length; i++)
		{
			//Gets the spawnradius for each FoodPoint
			range3[i] = enemyPoints3[i].GetComponent<FoodRange>().spawnRadius;
			enemyPointsPos3[i] = enemyPoints3[i].transform.position;
		}
		for(int i = 0; i < enemyPoints4.Length; i++)
		{
			//Gets the spawnradius for each FoodPoint
			range4[i] = enemyPoints4[i].GetComponent<FoodRange>().spawnRadius;
			enemyPointsPos4[i] = enemyPoints4[i].transform.position;
		}
	}

	//Method that calculates an outer limit for every foodPoint
	Vector3 RandomCircle ( Vector3 center ,  float radius )
	{
		// create random angle between 0 to 360 degrees
		float ang= Random.value * 360;
		Vector3 pos;
		pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
		pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
		pos.z = center.z;
		return pos;
	}
	
	//Method  to instantiate the food
	public void SpawnEnemy()
	{
		if(whichHusk == Husk.second)
		{
			Vector3 [] pos;
			pos = new Vector3[enemyPoints1.Length];
			for (int i = 0; i < enemyPoints1.Length ; i++)
			{
				float spawnRange = range1[i];
				Vector3 center = enemyPointsPos1[i];
				Vector3 outer = RandomCircle(center, spawnRange);
				//Get a random point between the center of the foodPoints and the outer limit of their radius
				pos [i] = new Vector3(Random.Range(center.x, outer.x), Random.Range(center.y, outer.y), outer.z);
				
			}
			//Instantiate the food at a random point in the range of a random foodPoint
			GameObject enemyGO = Instantiate(enemy, pos[Random.Range(0, enemyPointsPos1.Length)], Quaternion.identity) as GameObject;
			enemyList.Add(enemyGO);
		}
		else if(whichHusk == Husk.third)
		{
			Vector3 [] pos;
			pos = new Vector3[enemyPoints2.Length];
			for (int i = 0; i < enemyPoints2.Length ; i++)
			{
				float spawnRange = range2[i];
				Vector3 center = enemyPointsPos2[i];
				Vector3 outer = RandomCircle(center, spawnRange);
				//Get a random point between the center of the foodPoints and the outer limit of their radius
				pos [i] = new Vector3(Random.Range(center.x, outer.x), Random.Range(center.y, outer.y), outer.z);
				
			}
			//Instantiate the food at a random point in the range of a random foodPoint
			GameObject enemyGO = Instantiate(enemy, pos[Random.Range(0, enemyPointsPos2.Length)], Quaternion.identity) as GameObject;
			enemyList.Add(enemyGO);
		}
		else if(whichHusk == Husk.forth)
		{
			Vector3 [] pos;
			pos = new Vector3[enemyPoints3.Length];
			for (int i = 0; i < enemyPoints3.Length ; i++)
			{
				float spawnRange = range3[i];
				Vector3 center = enemyPointsPos3[i];
				Vector3 outer = RandomCircle(center, spawnRange);
				//Get a random point between the center of the foodPoints and the outer limit of their radius
				pos [i] = new Vector3(Random.Range(center.x, outer.x), Random.Range(center.y, outer.y), outer.z);
				
			}
			//Instantiate the food at a random point in the range of a random foodPoint
			GameObject enemyGO = Instantiate(enemy, pos[Random.Range(0, enemyPointsPos3.Length)], Quaternion.identity) as GameObject;
			enemyList.Add(enemyGO);
		}
	}
}
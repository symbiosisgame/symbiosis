using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {

	GameObject[] enemyPoints;    
	Vector3[] enemyPointsPos;
	public GameObject enemy;
	float[] range; 
	public List<GameObject> enemyList = new List<GameObject>();
	
	void Start ()
	{
		enemyPoints = GameObject.FindGameObjectsWithTag("EnemyPoint"); 
		enemyPointsPos = new Vector3[enemyPoints.Length];
		range = new float[enemyPoints.Length];
		
		for(int i = 0; i < enemyPoints.Length; i++)
		{
			//Gets the spawnradius for each FoodPoint
			range[i] = enemyPoints[i].GetComponent<FoodRange>().spawnRadius;
			enemyPointsPos[i] = enemyPoints[i].transform.position;
		}
	}
	
	//Method that calculates an outer limit for every foodPoint
	Vector3 RandomCircle ( Vector3 center ,  float radius  )
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
		Vector3 [] pos;
		pos = new Vector3[enemyPoints.Length];
		for (int i = 0; i < enemyPoints.Length ; i++)
		{
			float spawnRange = range[i];
			Vector3 center = enemyPointsPos[i];
			Vector3 outer = RandomCircle(center, spawnRange);
			//Get a random point between the center of the foodPoints and the outer limit of their radius
			pos [i] = new Vector3(Random.Range(center.x, outer.x), Random.Range(center.y, outer.y), outer.z);
			
		}
		//Instantiate the food at a random point in the range of a random foodPoint
		GameObject enemyGO = Instantiate(enemy, pos[Random.Range(0, enemyPointsPos.Length)], Quaternion.identity) as GameObject;
		enemyList.Add(enemyGO);
	}
}
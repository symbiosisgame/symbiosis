using UnityEngine;
using System.Collections.Generic;

public class FoodSpawner : MonoBehaviour {

	public GameObject[] foodPoints1, foodPoints2, foodPoints3, foodPoints4, foodPoints5;  //groups of food points for each husk / level
	Vector3[] foodPointsPos1, foodPointsPos2, foodPointsPos3, foodPointsPos4, foodPointsPos5;
	public GameObject food;
	float[] range1, range2, range3, range4, range5; 
	[HideInInspector]public List<GameObject> foodList = new List<GameObject>();
	[HideInInspector]public enum Husk {first, second, third, forth, fifth};
	[HideInInspector]public Husk whichHusk;
 
	void Start ()
	{
		whichHusk = Husk.first;

		foodPointsPos1 = new Vector3[foodPoints1.Length];
		foodPointsPos2 = new Vector3[foodPoints2.Length];
		foodPointsPos3 = new Vector3[foodPoints3.Length];
		foodPointsPos4 = new Vector3[foodPoints4.Length];
		foodPointsPos5 = new Vector3[foodPoints5.Length];

		range1 = new float[foodPoints1.Length];
		range2 = new float[foodPoints2.Length];
		range3 = new float[foodPoints3.Length];
		range4 = new float[foodPoints4.Length];
		range5 = new float[foodPoints5.Length];
	           
		for(int i = 0; i < foodPoints1.Length; i++)
		{
	        //Gets the spawnradius for each FoodPoint
			range1[i] = foodPoints1[i].GetComponent<SpawnerRange>().spawnRadius;
			foodPointsPos1[i] = foodPoints1[i].transform.position;
	    }
		for(int i = 0; i < foodPoints2.Length; i++)
		{
			//Gets the spawnradius for each FoodPoint
			range2[i] = foodPoints2[i].GetComponent<SpawnerRange>().spawnRadius;
			foodPointsPos2[i] = foodPoints2[i].transform.position;
		}
		for(int i = 0; i < foodPoints3.Length; i++)
		{
			//Gets the spawnradius for each FoodPoint
			range3[i] = foodPoints3[i].GetComponent<SpawnerRange>().spawnRadius;
			foodPointsPos3[i] = foodPoints3[i].transform.position;
		}
		for(int i = 0; i < foodPoints4.Length; i++)
		{
			//Gets the spawnradius for each FoodPoint
			range4[i] = foodPoints4[i].GetComponent<SpawnerRange>().spawnRadius;
			foodPointsPos4[i] = foodPoints4[i].transform.position;
		}
		for(int i = 0; i < foodPoints5.Length; i++)
		{
			//Gets the spawnradius for each FoodPoint
			range5[i] = foodPoints5[i].GetComponent<SpawnerRange>().spawnRadius;
			foodPointsPos5[i] = foodPoints5[i].transform.position;
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
    public void SpawnFood()
	{
		if(whichHusk == Husk.first)
		{
			Vector3 [] pos;
			pos = new Vector3[foodPoints1.Length];
			for (int i = 0; i < foodPoints1.Length ; i++)
			{
				float spawnRange = range1[i];
				Vector3 center = foodPointsPos1[i];
				Vector3 outer = RandomCircle(center, spawnRange);
				//Get a random point between the center of the foodPoints and the outer limit of their radius
				pos [i] = new Vector3(Random.Range(center.x, outer.x), Random.Range(center.y, outer.y), outer.z);
				
			}
			//Instantiate the food at a random point in the range of a random foodPoint
			GameObject foodGO = Instantiate(food, pos[Random.Range(0, foodPoints1.Length)], Quaternion.identity) as GameObject;
			foodList.Add(foodGO);
		}
		else if(whichHusk == Husk.second)
		{
			Vector3 [] pos;
			pos = new Vector3[foodPoints2.Length];
			for (int i = 0; i < foodPoints2.Length ; i++)
			{
				float spawnRange = range2[i];
				Vector3 center = foodPointsPos2[i];
				Vector3 outer = RandomCircle(center, spawnRange);
				//Get a random point between the center of the foodPoints and the outer limit of their radius
				pos [i] = new Vector3(Random.Range(center.x, outer.x), Random.Range(center.y, outer.y), outer.z);
				
			}
			//Instantiate the food at a random point in the range of a random foodPoint
			GameObject foodGO = Instantiate(food, pos[Random.Range(0, foodPoints2.Length)], Quaternion.identity) as GameObject;
			foodList.Add(foodGO);
		}
		else if(whichHusk == Husk.third)
		{
			Vector3 [] pos;
			pos = new Vector3[foodPoints3.Length];
			for (int i = 0; i < foodPoints3.Length ; i++)
			{
				float spawnRange = range3[i];
				Vector3 center = foodPointsPos3[i];
				Vector3 outer = RandomCircle(center, spawnRange);
				//Get a random point between the center of the foodPoints and the outer limit of their radius
				pos [i] = new Vector3(Random.Range(center.x, outer.x), Random.Range(center.y, outer.y), outer.z);
				
			}
			//Instantiate the food at a random point in the range of a random foodPoint
			GameObject foodGO = Instantiate(food, pos[Random.Range(0, foodPoints3.Length)], Quaternion.identity) as GameObject;
			foodList.Add(foodGO);
		}
		else if(whichHusk == Husk.forth)
		{
			Vector3 [] pos;
			pos = new Vector3[foodPoints4.Length];
			for (int i = 0; i < foodPoints4.Length ; i++)
			{
				float spawnRange = range4[i];
				Vector3 center = foodPointsPos4[i];
				Vector3 outer = RandomCircle(center, spawnRange);
				//Get a random point between the center of the foodPoints and the outer limit of their radius
				pos [i] = new Vector3(Random.Range(center.x, outer.x), Random.Range(center.y, outer.y), outer.z);
				
			}
			//Instantiate the food at a random point in the range of a random foodPoint
			GameObject foodGO = Instantiate(food, pos[Random.Range(0, foodPoints4.Length)], Quaternion.identity) as GameObject;
			foodList.Add(foodGO);
		}
		else if(whichHusk == Husk.fifth)
		{
			Vector3 [] pos;
			pos = new Vector3[foodPoints5.Length];
			for (int i = 0; i < foodPoints5.Length ; i++)
			{
				float spawnRange = range4[i];
				Vector3 center = foodPointsPos5[i];
				Vector3 outer = RandomCircle(center, spawnRange);
				//Get a random point between the center of the foodPoints and the outer limit of their radius
				pos [i] = new Vector3(Random.Range(center.x, outer.x), Random.Range(center.y, outer.y), outer.z);
				
			}
			//Instantiate the food at a random point in the range of a random foodPoint
			GameObject foodGO = Instantiate(food, pos[Random.Range(0, foodPoints5.Length)], Quaternion.identity) as GameObject;
			foodList.Add(foodGO);
		}
    }
}
using UnityEngine;
using System.Collections.Generic;

public class FoodSpawner : MonoBehaviour {

	GameObject[] foodPoints;    
	Vector3[] foodPointsPos;
	public GameObject food;
	float[] range; 
	public List<GameObject> foodList = new List<GameObject>();
 
	void Start ()
	{
	    foodPoints = GameObject.FindGameObjectsWithTag("FoodPoint"); 
		foodPointsPos = new Vector3[foodPoints.Length];
		range = new float[foodPoints.Length];
	           
	    for(int i = 0; i < foodPoints.Length; i++)
		{
	        //Gets the spawnradius for each FoodPoint
	        range[i] = foodPoints[i].GetComponent<FoodRange>().spawnRadius;
	        foodPointsPos[i] = foodPoints[i].transform.position;
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
        Vector3 [] pos;
        pos = new Vector3[foodPoints.Length];
	    for (int i = 0; i < foodPoints.Length ; i++)
		{
	    	float spawnRange = range[i];
	        Vector3 center = foodPointsPos[i];
	        Vector3 outer = RandomCircle(center, spawnRange);
	        //Get a random point between the center of the foodPoints and the outer limit of their radius
	        pos [i] = new Vector3(Random.Range(center.x, outer.x), Random.Range(center.y, outer.y), outer.z);
            
	    }
        //Instantiate the food at a random point in the range of a random foodPoint
        GameObject foodGO = Instantiate(food, pos[Random.Range(0, foodPoints.Length)], Quaternion.identity) as GameObject;
        foodList.Add(foodGO);
    }
}
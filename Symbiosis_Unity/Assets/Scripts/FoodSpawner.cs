using UnityEngine;
using System.Collections;

public class FoodSpawner : MonoBehaviour {

GameObject[] foodPoints;    
Vector3[] foodPointsPos;
GameObject food;
float[] range; 

 
void Start (){
    food = GameObject.Find("Food");
    foodPoints = GameObject.FindGameObjectsWithTag("FoodPoint"); 
foodPointsPos = new Vector3[foodPoints.Length];
range = new float[foodPoints.Length];
           
 
    for(int i = 0; i < foodPoints.Length; i++){
        //Gets the spawnradius for each FoodPoint
        range[i] = foodPoints[i].GetComponent<FoodRange>().spawnRadius;

        foodPointsPos[i] = foodPoints[i].transform.position;

    }
    SpawnFood();
}
 

   //Method that calculates an outer limit for every 
 Vector3 RandomCircle ( Vector3 center ,   float radius  ){
    // create random angle between 0 to 360 degrees
    float ang= Random.value * 360;
    Vector3 pos;
    pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
    pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
    pos.z = center.z - 1;
    return pos;
}


    //Method  to instantiate the food
    void SpawnFood(){
    for (int i = 0; i < foodPoints.Length ; i++)
{
    float spawnRange = range[i];
       Vector3 center = foodPointsPos[i];
        Vector3 outer = RandomCircle(center, spawnRange);
        // Get a random point between the center of the foodPoints and the outer limit of their radius
        Vector3 pos = new Vector3(Random.Range(center.x, outer.x), Random.Range(center.y, outer.y), outer.z);
        //  Instantiate the food in that point 
        Instantiate(food, pos, Quaternion.identity);
    }
    }
}
using UnityEngine;
using System.Collections;

public class Feeder : PlayerManager {

	public float currEatTime, eatTimer;
	public float eatRate = 1;
	[HideInInspector]public bool feeding;
	GameObject foodSource;
	Food food;

	new void Start()
	{
		currentFood = 0; 
	}
    
	void Update()
	{
		if(feeding)
		{
			if(food.foodStock > 0) //tracks to see if any food is left in Foods stock 
			{
				currEatTime += eatRate * Time.deltaTime;
				if(currEatTime >= eatTimer)
				{
					food.foodStock--;
					IncreaseFood(1);
					currEatTime = 0;
				}
			}
			else
			{
				Destroy(foodSource);
			}
		}
	}

	public void FoodSource(GameObject whatFood) //method to take gameobject of the food source the feeder is currently munching on, also store Food script component of the food gameobject
	{
		foodSource = whatFood;
		food = foodSource.GetComponent<Food>();
	}
}

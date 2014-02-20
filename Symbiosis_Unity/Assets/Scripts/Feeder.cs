using UnityEngine;
using System.Collections;

public class Feeder : PlayerManager {

	public float currEatTime, eatTimer;
	public float currTransferTime, transferTimer, transferMod;
	public float eatRate = 1;
	[HideInInspector]public bool feeding;
	[HideInInspector]public bool transferring;
	GameObject foodSource;
	Animator myAnim;
	Food food;

	new void Start()
	{
		base.Start ();
		currentFood = 0; 
		myAnim = transform.GetChild(0).GetComponent<Animator>();
	}
    
	void Update()
	{
		Feed();
		TransferFood();
	}

	void Feed()
	{
		if(feeding)
		{
			myAnim.SetBool("Feeding", true); //Feeding bool tracked in the animator for transition condition
			if(food.foodStock > 0) //tracks to see if any food is left in Foods stock 
			{
				currEatTime += eatRate * Time.deltaTime;
				if(currEatTime >= eatTimer)
				{
					food.foodStock--;
					AdjustFood(1);
					currEatTime = 0;
				}
			}
			else
			{
				Destroy(foodSource);
				feeding = false;
			}
		}
		else
		{
			myAnim.SetBool("Feeding", false);
		}
	}

	public void FoodSource(GameObject whatFood) //method to take gameobject of the food source the feeder is currently munching on, also store Food script component of the food gameobject
	{
		foodSource = whatFood;
		food = foodSource.GetComponent<Food>();
	}

	public void TransferFood() //input is called in PlayerControls
	{
		if(transferring)
		{
			if(this.currentFood > 0)
			{
				float playerDistance = GameObject.Find ("PlayerManager").GetComponent<PlayerControls>().playerDistanceReal();
				//Create a proper modifier for distance based transfer
				currTransferTime += Time.deltaTime; //add in modifier for * distance
				if(currTransferTime >= transferTimer)
				{	
					AdjustFood(-1);
					protector.AdjustFood(1);
					currTransferTime = 0;
				}
			}
			else
			{
				transferring = false;
			}
		}
		else
		{
			currTransferTime = 0;
		}
	}
}

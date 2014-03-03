using UnityEngine;
using System.Collections;

public class Food : Entities {

	public int foodStock;

	void OnTriggerEnter(Collider other)
	{
		if(other.name == "Player1")
		{
			feederGO.BroadcastMessage("FoodSource", gameObject, SendMessageOptions.DontRequireReceiver);
			feeder.feeding = true; //sets feeding
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.name == "Player1")
		{
			feeder.feeding = false;
		}
	}
}

using UnityEngine;
using System.Collections;

// script can be attached to PlayerManager

public class BelayScript : MonoBehaviour 
{
	void OnEnable()
	{
		ShipTriggerManager.Docked += Docked;
		ShipTriggerManager.UnDocked += UnDocked;
	}
	
	
	void OnDisable()
	{
		ShipTriggerManager.Docked -= Docked;
		ShipTriggerManager.UnDocked += UnDocked;
	}
	
	
	void Docked(Collider other)
	{

		if((other.name == "Player1"))
		{
			Debug.Log( "Docked Player1 via OnTriggerStay" );
		}

		if((other.name == "Player2"))
		{
			Debug.Log( "Docked Player2 via OnTriggerStay" );
		}

		//TODO two other players at same time 
	}




	void UnDocked(Collider other)
	{
		Debug.Log( other.name + " joyfully UnDocked via OnTriggerExit..." );
	}




}// end class
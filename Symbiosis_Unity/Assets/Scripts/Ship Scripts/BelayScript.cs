using UnityEngine;
using System.Collections;

// BelayScript should be attached to the PlayerManager whilst ShipTriggerManager goes onto any triggers
// Belay handles the logic for docked ships and other entities interacting the trigger and belays an action/order

public class BelayScript : MonoBehaviour 
{
	void OnEnable()
	{
		ShipTriggerManager.Docking += Docking;		// OnTriggerEnter
		ShipTriggerManager.IsDocked += Docked;		// OnTriggerStay
		ShipTriggerManager.HasUnDocked += UnDock;	// OnTriggerExit
	}
	
	
	void OnDisable()
	{
		ShipTriggerManager.Docking -= Docking;
		ShipTriggerManager.IsDocked -= Docked;
		ShipTriggerManager.HasUnDocked += UnDock;
	}


	// events


	void Docking(Collider other)
	{
		Debug.Log( "Docking..." + other.name );
	}
	

	
	void Docked(Collider other)
	{
		if((other.name == "Player1") || (other.name == "Player2"))
		{
			Debug.Log( "This Player Docked: " + other.name );
			//TODO set docked flag or case on player and invoke refuelling/energy transfer
		}
	}
	


	void UnDock(Collider other)
	{
		Debug.Log( "UnDock'ed..." + other.name );
	}




}// end class
//------------------------------------------------------

//-------- notes
// scenario:  if player docked and other enemy docking then set docked player shields up
// scenario:  if player docked and other enemy docking then set undocked player beacon alert

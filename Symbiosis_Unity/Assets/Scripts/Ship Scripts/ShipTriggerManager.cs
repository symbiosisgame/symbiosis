using UnityEngine;
using System.Collections;

// use this script with a trigger-able object or helper
// events are being used so the players docking and undocking status can be registered with other scripts
// should be useful with seek, enemy AI and charging

public class ShipTriggerManager : MonoBehaviour 
{
	// gui
	public Color triggerColor = Color.cyan;
	public bool hideTrigger = true;
	public bool showGizmos = true;
	
	// delegates, events
	public delegate void DockAction(Collider other);
	public static event DockAction Docked;
	public static event DockAction UnDocked;



	// trigger zone
	void OnTriggerStay( Collider other )
	{
		if( (other.name == "Player1") || (other.name == "Player2") )
		{
			if(Docked != null)
			{ 
				Docked(other);
			}
		}
	}



	// enter
	void OnTriggerEnter( Collider other )
	{
		//Debug.Log(other.name + " entered the trigger via ShipTriggerManager");
		//TODO check which type of trigger events are needed
	}


	
	// exit
	void OnTriggerExit( Collider other )
	{
		if( (other.name == "Player1") || (other.name == "Player2") )
		{
			if(UnDocked != null)
			{ 
				UnDocked(other);
			}
		}
	}





	// gui stuff

	void Start()
	{
		if( hideTrigger ){ renderer.enabled = false; }else{ renderer.enabled = true; }
	}

	
	void OnDrawGizmos()
	{
		if( showGizmos )
		{
			Gizmos.color = triggerColor;
			Gizmos.DrawWireCube( transform.position, transform.collider.bounds.extents * 2 );
			if( renderer.enabled ){transform.renderer.sharedMaterial.SetColor("_Emission", triggerColor);}
		}
	}

}//end class
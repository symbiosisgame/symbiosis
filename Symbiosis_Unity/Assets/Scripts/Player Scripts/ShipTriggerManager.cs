﻿using UnityEngine;
using System.Collections;

// events are being used so the players docking and undocking status can be registered within other scripts.
// should be used with seek, enemy AI and charging.

public class ShipTriggerManager : MonoBehaviour 
{
	// gui
	public Color triggerColor = Color.cyan;
	public bool hideTrigger = true;
	public bool showGizmos = true;
	public bool dockFull = false;
	
	// delegate events
	public delegate void DockAction( Collider _other );
	public static event DockAction Docking;
	public static event DockAction IsDocked;
	public static event DockAction HasUnDocked;



	// trigger zone
	// ------------

	// enter
	void OnTriggerEnter(Collider other)
	{
		if(!dockFull)
		{
			if(Docking != null)
			{ 
				Docking(other);
				dockFull = true;
			}
		}
	}


	// exit
	void OnTriggerExit(Collider other)
	{
		if(dockFull)
		{
			if(HasUnDocked != null)
			{ 
				HasUnDocked(other);
				dockFull = false;
			}
		}
	}






	// gui
	// ---

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
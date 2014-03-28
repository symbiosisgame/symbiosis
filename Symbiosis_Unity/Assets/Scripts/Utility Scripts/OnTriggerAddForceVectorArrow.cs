using UnityEngine;
using System.Collections;

public class OnTriggerAddForceVectorArrow : MonoBehaviour
{
	// gui
	public Color triggerColor = Color.cyan;
	public bool hideTrigger = true;
	public bool showGizmos = true;
	
	public float forceAmount = 3.0f;


	
	void OnTriggerStay( Collider other )
	{
		Vector3 arrowDirection = transform.TransformDirection(Vector3.up);

		if ( other.attachedRigidbody )
		{
			other.attachedRigidbody.AddForce( arrowDirection * forceAmount );
		}
	}
	
	
	void Start()
	{
		if( hideTrigger ){ renderer.enabled = false; }else{ renderer.enabled = true; }
	}


	//gui
	void OnDrawGizmos()
	{
		if(showGizmos)
		{
			Gizmos.color = triggerColor;
			Gizmos.DrawWireCube( transform.position, transform.collider.bounds.extents * 2 );
			if( renderer.enabled ){transform.renderer.sharedMaterial.SetColor("_Emission", triggerColor);}
		}
	}
	
	
}// end class
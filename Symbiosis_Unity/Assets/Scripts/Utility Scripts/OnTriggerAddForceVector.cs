using UnityEngine;
using System.Collections;

public class OnTriggerAddForceVector : MonoBehaviour
{
	public Color gizmoColor = Color.yellow;
	public bool hideTrigger = true;

	public float forceAmountUp = 3.5f;
	public float forceAmountDown = 2.5f;
	public float forceAmountLeft = 2.0f;
	public float forceAmountRight = 2.0f;
	public bool forceUp = false;
	public bool forceDown = false;
	public bool forceLeft = false;
	public bool forceRight = false;



	void OnTriggerStay( Collider other )
	{
		if ( other.attachedRigidbody )
		{
			if( forceUp )	{other.attachedRigidbody.AddForce( Vector3.up * forceAmountUp);}
			if( forceDown )	{other.attachedRigidbody.AddForce( Vector3.down * forceAmountDown);}
			if( forceLeft )	{other.attachedRigidbody.AddForce( Vector3.left * forceAmountLeft);}
			if( forceRight ){other.attachedRigidbody.AddForce( Vector3.right * forceAmountRight);}
		}
		
	}
	

	void Start()
	{
		if( hideTrigger ){ renderer.enabled = false; }else{ renderer.enabled = true; }
	}
	

	void OnDrawGizmos()
	{
		Gizmos.color = gizmoColor;
		Gizmos.DrawWireCube( transform.position, transform.collider.bounds.extents * 2 );
		if( renderer.enabled ){transform.renderer.sharedMaterial.SetColor("_Emission", gizmoColor);}
	}
	

}// end class
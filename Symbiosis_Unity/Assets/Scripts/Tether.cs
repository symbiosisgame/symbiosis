using UnityEngine;
using System.Collections;

public class Tether : MonoBehaviour
{
	// variables
	// ---------
	public Color gizmoColor = Color.blue;
	private bool showGizmos = false;

	public GameObject tetherEndObject;
	public GameObject tetherStartObject;
	public GameObject player1;
	public GameObject player2;
	public int numSegments = 6;
	public float colliderRadius = 0.25f;

	//public float massValue = 1.0f;
	//public float dragValue = 1.0f;
	//public float angularDragValue = 1.0f;

	private Rigidbody rigid_body;
	private HingeJoint hinge_joint;
	private SphereCollider sphere_collider;
	private GameObject[] go_links;


	// methods
	// -------
	
	void buildTetherLinks(int num_segments)
	{
		for( int i = 0; i < num_segments; i++ )
		{
			go_links[i] = new GameObject( "Tether Link " + i );

			// components
			sphere_collider = go_links[i].AddComponent<SphereCollider>();
			sphere_collider.radius = colliderRadius;

			rigid_body = go_links[i].AddComponent<Rigidbody>();
			hinge_joint = go_links[i].AddComponent<HingeJoint>();
		
			// transforms
			Vector3 seperation = tetherEndObject.transform.position - tetherStartObject.transform.position;
			go_links[i].transform.position = tetherStartObject.transform.position + seperation.normalized * i;

			// values
			go_links[i].rigidbody.useGravity = true;
			go_links[i].rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | 
												RigidbodyConstraints.FreezeRotationY |
												RigidbodyConstraints.FreezePositionZ;

			if(i == 0)
			{
				sphere_collider.isTrigger = true;
				go_links[i].transform.parent = tetherStartObject.transform;

				go_links[i].rigidbody.mass = 1.0f;
				go_links[i].rigidbody.drag = 0;
				go_links[i].rigidbody.angularDrag = 0;
				go_links[i].rigidbody.isKinematic = true;


			}else{

				//massValue = massValue * 1.1f;
				//dragValue = dragValue * 0.9f;

				go_links[i].rigidbody.mass = 2.0f;
				go_links[i].rigidbody.drag = 2.0f;
				go_links[i].rigidbody.angularDrag = 45.0f;

				go_links[i].hingeJoint.connectedBody = go_links[i-1].rigidbody;
				
			}


			if(i == num_segments-1)
			{
			}
	
		}//end for

		showGizmos = true;

	}//end buildTetherLinks



	void buildTether(int num_segments)
	{
		go_links = new GameObject[num_segments];
		buildTetherLinks(num_segments);
	}





	void Start ()
	{
		buildTether(numSegments);
	}


	void Update ()
	{
	}


	void FixedUpdate ()
	{
	}


	void LateUpdate()
	{
	}



	void OnDrawGizmos()
	{
		if(showGizmos)
		{
			Gizmos.color = gizmoColor;

			for( int i = 0; i < numSegments; i++ )
			{
				Gizmos.DrawWireSphere( go_links[i].transform.position, colliderRadius);
			}
		}
	}
	



}// end class

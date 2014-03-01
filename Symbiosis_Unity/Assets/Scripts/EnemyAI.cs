using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{

	// gui
	public Color gizmoColor = Color.yellow;
	public bool showGizmos = true;

	// enemy
	public float detectorRadius = 2.0f;
	public float enemySpeed = 2.0f;
	public float maxSpeed = 6.0f;

	// revise
	public Vector3 gravity = new Vector3(0, -9.8F, 0);
	public Vector3 velocity = Vector3.zero;
	public Vector3 forceAcc = Vector3.zero;
	
	// common vars
	Transform p1Transform, p2Transform; 
	GameObject player1, player2;

	// roles
	[HideInInspector]public Feeder feeder;
	[HideInInspector]public Protector protector;


	// start
	void Start ()
	{
		// common start
		player1 = GameObject.Find("Player1");
		player2 = GameObject.Find("Player2");
		p1Transform = player1.transform;
		p2Transform = player2.transform;

		// get roles
		feeder = player1.GetComponent<Feeder>();
		protector = player2.GetComponent<Protector>();
	}
	

	// update
	void Update ()
	{

		Vector3 toPlayer1 = p1Transform.position - transform.position;
		Vector3 toPlayer2 = p2Transform.position - transform.position;

		// enemies reaction to player 1 in range

		if (toPlayer1.magnitude > detectorRadius)
		{
			//Debug.Log("player1 out of detectorRadius");           
		}
		else
		{
			//Debug.Log("player1 inside detectorRadius");

			if(feeder.feeding)
			{
				Debug.Log("player1 is feeding... moving in...");

				Vector3 dest = p1Transform.position;
				Vector3 toDest = dest - transform.position;
				toDest.Normalize();
				transform.position += toDest * enemySpeed * Time.deltaTime;
				transform.forward = toDest;
			
			}else{

				Debug.Log(" ... not feeding...");
			}
		}


		// stub
		// enemies reaction to player 2 in range

		if (toPlayer2.magnitude > detectorRadius)
		{
			//Debug.Log("player2 out of detectorRadius");           
		}
		else
		{
			//Debug.Log("player2 inside detectorRadius");
		}

	
	}// end update



	// BEHAVIOURS

	// Seek
	Vector3 seek(Vector3 target)
	{
		Vector3 desired = target - transform.position;
		desired =  Vector3.Normalize(desired);
		desired = desired * maxSpeed;
		return desired - velocity;
	}


	// Flee
	Vector3 flee(Vector3 target)
	{
		return target;
	}







	// gui
	void OnDrawGizmos()
	{
		if(showGizmos)
		{
			Gizmos.color = gizmoColor;
			Gizmos.DrawWireSphere( transform.position, detectorRadius );

		}
	}

}// end class

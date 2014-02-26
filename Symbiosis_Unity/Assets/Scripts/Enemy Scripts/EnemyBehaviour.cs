using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour
{
	// gui
	public Color gizmoColor = Color.blue;
	public bool showGizmos = true;

	// enemy
	public GameObject enemy;
	public float detectorRadius = 2.0f;
	public float attackingRange = 1.0f;

	// common vars
	Transform p1Transform, p2Transform; 
	GameObject player1, player2;
	
	// roles
	[HideInInspector]public Feeder feeder;
	[HideInInspector]public Protector protector;

	// intergrator
	public Vector3 velocity = Vector3.zero;
	public Vector3 forceAcc = Vector3.zero;
	public float maxSpeed = 1f;
	public float mass = 1f;


	// enums
	public enum EnemyStates
	{
		idle 		= 0,
		following 	= 1,
		attacking 	= 2,
		feeding 	= 3,
		scared 		= 4,
		dying 		= 5
	}
	public EnemyStates currentState = EnemyStates.idle;

	// enemy
	Transform _transform; // for self collisions on trigger


	// testing
	CharacterController character_controller;
	public Vector3 target = new Vector3(0f, 1.2f, 0f);



	// Use this for initialization
	void Start ()
	{
		// common start
		player1 = GameObject.Find("Player1");
		player2 = GameObject.Find("Player2");
		p1Transform = player1.transform;
		p2Transform = player2.transform;
		
		// get components
		feeder = player1.GetComponent<Feeder>();
		protector = player2.GetComponent<Protector>();
		character_controller = GetComponent<CharacterController>();
	}


	// steering behaviours
	// -------------------

	// seek
	Vector3 Seek(Vector3 targetPos)
	{
		Vector3 desired = targetPos - transform.position;
		desired.Normalize();
		desired *= maxSpeed;
		// towards
		return desired - velocity;
	}


	// flee
	Vector3 Flee(GameObject other)
	{
		float fleeDist = 1.5f;
		
		Vector3 desired = other.transform.position - transform.position;
		
		if (desired.magnitude < fleeDist)
		{
			desired.Normalize();
			desired *= maxSpeed;
			// away
			return velocity - desired;
		}
		else
		{
			return Vector3.zero; // stay
		}
	}


	// arrive
	Vector3 Arrive(Vector3 target)
	{
		Vector3 toTarget = target - transform.position;
		float distance = toTarget.magnitude;
		float slowingDistance = 1.1f;

		if (distance == 0.0f)
		{
			return Vector3.zero;
		}
		const float DecelerationTweak = 1.25f;

		float rampedSpeed = maxSpeed * (distance / (slowingDistance * DecelerationTweak));
		float clampedSpeed = Mathf.Min(maxSpeed, rampedSpeed);
		Vector3 desiredVelocity = clampedSpeed * (toTarget / distance);
		
		return desiredVelocity - velocity;
	}


	// pursue
	Vector3 Pursuit(GameObject other)
	{
		Vector3 toTarget = other.transform.position - transform.position;
		float dist = toTarget.magnitude;
		float time = dist / maxSpeed;
		Vector3 targetPos = other.transform.position + (time * other.rigidbody.velocity);

		return Seek(targetPos);
	}

	// end steering behaviours
	


	// Update is called once per frame
	void Update ()
	{

		Vector3 toPlayer1 = p1Transform.position - transform.position;
		Vector3 toPlayer2 = p2Transform.position - transform.position;

		// testing move
		//character_controller.Move( ((toPlayer1).normalized / 2.2f) * Time.deltaTime );




		// testing behaviours
		// ------------------

		//forceAcc += Seek(p1Transform.position);		// Vector3
		//forceAcc += Flee(player1);					// GameObject

		//forceAcc += Arrive(p1Transform.position);		// Vector3
		//forceAcc += Arrive(target);					// Vector3
		//forceAcc += Pursuit(player1);					// GameObject



		// test cautious to player 1
	
		if ( toPlayer1.magnitude > Random.Range(detectorRadius, (detectorRadius*4) ) )
		{
			forceAcc += Seek(p1Transform.position);
		}
		else
		{
			forceAcc += Flee(player1);
		}


		// case and switch begins
		/***
		 * TODO player behaviours on arrival, feeding, attack, scared
		 *  


		switch(currentState)
		{
			case EnemyStates.idle:
				Debug.Log ("Enemy is idle");

				if( toPlayer1.magnitude > detectorRadius )
				{
					currentState = EnemyStates.following;
				}
			break;



			case EnemyStates.following:
				Debug.Log ("Enemy is following");

				// follow a nearby feeder
				if (feeder.feeding)
				{
					Debug.Log ("Enemy is seeking a feeder.feeding");
					forceAcc += Arrive(feeder.transform.position);
				}


				if(feeder.transform.position.magnitude < attackingRange)
				{
					forceAcc += Seek(feeder.transform.position); // try to get a better position
					currentState = EnemyStates.attacking;
				}

			break;



			case EnemyStates.attacking:
				Debug.Log ("Enemy is attacking");
			break;



			case EnemyStates.feeding:
				Debug.Log ("Enemy is feeding");
			break;


			case EnemyStates.scared:
				Debug.Log ("Enemy is scared");

				// beacon squawk 
				forceAcc += Flee(player1);
				Debug.Log ("Enemy is fleeing player1 squawk ");

			break;


			case EnemyStates.dying:
			Debug.Log ("Enemy is dying");
			break;
		}
		// end case and switch block

********/



		// force integrator

		Vector3 accel = forceAcc / mass;
		velocity = velocity + accel * Time.deltaTime;
		transform.position = transform.position + velocity * Time.deltaTime;

		forceAcc = Vector3.zero;

		if (velocity.magnitude > float.Epsilon)
		{
			transform.forward = Vector3.Normalize(velocity) ;
		}
		velocity *= 0.99f; // damping



	}// end update



	void OnDrawGizmos()
	{
		if( showGizmos )
		{
			Gizmos.color = Color.cyan;
			Gizmos.DrawWireSphere( transform.position, detectorRadius );
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere( transform.position, attackingRange );
		}
	}


}// end class

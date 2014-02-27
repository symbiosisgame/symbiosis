using UnityEngine;
using System.Collections;

public class EnemyBehaviour1 : MonoBehaviour
{
	// gui
	public Color gizmoColor = Color.blue;
	public bool showGizmos = true;

	// enemy
	public float detectorRadius = 2.25f;	// large radius, omni directional sense
	public float closeRange = 1.25f;		// close range radius, is slightly larger than enemy collider trigger (so colliders can do their own self collision checks etc)

	// common vars
	Transform p1Transform, p2Transform; 
	GameObject player1, player2;
	Vector3 toPlayer1, toPlayer2;
	
	// roles
	[HideInInspector]public Feeder feeder;
	[HideInInspector]public Protector protector;

	// intergrator
	public Vector3 velocity = Vector3.zero;
	public Vector3 forceAcc = Vector3.zero;
	public float maxSpeed = 0.5f;
	public float mass = 0.1f;
	
	// enums
	public enum EnemyStates
	{
		floating 	= 0,
		following 	= 1,
		closerange 	= 2,
		feeding 	= 3,
		fighting 	= 4,
		fleeing		= 5,
		dying 		= 6
	}
	public EnemyStates currentState;

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

		currentState = EnemyStates.floating;
	}

	// Update is called once per frame
	void Update ()
	{
		toPlayer1 = p1Transform.position - transform.position;
		toPlayer2 = p2Transform.position - transform.position;
		Debug.DrawLine(transform.position, p1Transform.position );

		// case and switch begins
		// TODO player behaviours on arrival, feeding, attack, scared

		switch(currentState)
		{
			case EnemyStates.floating:
				Float();
				break;
			case EnemyStates.following:
				Follow();
				break;
			case EnemyStates.closerange:
				CloseRange();
				break;
			case EnemyStates.feeding:
				Feeding();
				break;
			case EnemyStates.fighting:
				Fighting();
				break;
			case EnemyStates.fleeing:
				Fleeing();
				break;
			case EnemyStates.dying:
				Dying ();
				break;
		}
		// end case and switch block
		Movement();
		CheckPlayer2Dist(); //checks for player 2 distance and flees if too close
	}// end update

	void Movement() // force integrator
	{
		Vector3 accel = forceAcc / mass;
		velocity = velocity + accel * Time.deltaTime;
		transform.position = transform.position + velocity * Time.deltaTime;
		
		forceAcc = Vector3.zero;
		
		if (velocity.magnitude > float.Epsilon)
		{
			transform.forward = Vector3.Normalize(velocity) ;
		}
		velocity *= 0.99f; // damping
	}

	//Functions for behaviours, called in respective case
	void Float() //Enemy floats about until player is in it's detection range
	{
		Debug.Log ("Enemy is floating (green)");
		transform.renderer.material.SetColor("_Emission", Color.green);
		
		if( toPlayer1.magnitude > detectorRadius && toPlayer1.magnitude < 8f)
		{
			currentState = EnemyStates.following;
			Debug.Log("Won't change state to following...");
		}
		
		if ( feeder.feeding )
		{
			currentState = EnemyStates.following;
		}
	}

	void Follow() //Enemy follows player1, seeks the feeder if feeding, or 
	{
		Debug.Log ("Enemy is following (yellow)");
		transform.renderer.material.SetColor("_Emission", Color.yellow);


		if( (toPlayer1.magnitude < 8f))
		{
			forceAcc += Arrive(player1.transform.position);
		}
		else
		{
			currentState = EnemyStates.floating;
		}

		if (feeder.feeding)
		{
			forceAcc += Seek(feeder.transform.position);
		}

		if(toPlayer1.magnitude < closeRange)
		{
			currentState = EnemyStates.fighting;
		}
	}

	void CloseRange()
	{
		Debug.Log ("Enemy is in close range (red)");
		transform.renderer.material.SetColor("_Emission", Color.red);	
	}

	void Feeding()
	{
		Debug.Log ("Enemy is feeding");
	}

	void Fighting()
	{
		Debug.Log ("Enemy is fighting");
		transform.renderer.material.SetColor("_Emission", Color.red);	
		if(toPlayer1.magnitude > closeRange)
		{
			currentState = EnemyStates.following;
		}
	}

	void Fleeing()
	{
		transform.renderer.material.SetColor("_Emission", Color.red);	
		Debug.Log ("Enemy is fleeing");

		forceAcc += Flee(player2);	// scared
		if( toPlayer2.magnitude > Random.Range(6,12) )
		{
			currentState = EnemyStates.following;
		}
	}

	void Dying()
	{
		Debug.Log ("Enemy is dying");
	}

	void CheckPlayer2Dist()
	{
		if( toPlayer2.magnitude < closeRange)
		{
			currentState = EnemyStates.fleeing;
		}
	}
	//End of behaviours for case switch


	// steering behaviours
	// -------------------

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
		float fleeDist = 1.6f;
		
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

	Vector3 Arrive(Vector3 target)
	{
		Vector3 toTarget = target - transform.position;
		float distance = toTarget.magnitude;
		float slowingDistance = 1.1f;
		
		if (distance == 0.0f)
		{
			return Vector3.zero;
		}
		const float DecelerationTweak = 1.5f;
		
		float rampedSpeed = maxSpeed * (distance / (slowingDistance * DecelerationTweak));
		float clampedSpeed = Mathf.Min(maxSpeed, rampedSpeed);
		Vector3 desiredVelocity = clampedSpeed * (toTarget / distance);
		
		return desiredVelocity - velocity;
	}

	Vector3 Pursuit(GameObject other)
	{
		Vector3 toTarget = other.transform.position - transform.position;
		float dist = toTarget.magnitude;
		float time = dist / maxSpeed;
		Vector3 targetPos = other.transform.position + (time * other.rigidbody.velocity);
		
		return Seek(targetPos);
	}

	void OnDrawGizmos()
	{
		if( showGizmos )
		{
			Gizmos.color = Color.cyan;
			Gizmos.DrawWireSphere( transform.position, detectorRadius );
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere( transform.position, closeRange );
		}
	}

}// end class

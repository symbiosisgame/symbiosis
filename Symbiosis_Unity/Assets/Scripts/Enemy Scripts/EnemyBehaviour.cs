using UnityEngine;
using System.Collections;

public class EnemyBehaviour : Entities
{
	// gui
	public Color gizmoColor = Color.blue;
	public bool showGizmos = true;

	// enemy
	public float detectorRadius = 2.25f;	// large radius, omni directional sense
	public float closeRange = 1.25f;		// close range radius, is slightly larger than enemy collider trigger (so colliders can do their own self collision checks etc)
	public int damage;
	public float attackTimer;
	private float attackTime;

	public float detectDist;

	// common vars
	Vector3 toPlayer1, toPlayer2;
	Transform myTransform;
	bool createSFX = false;

	// intergrator
	public Vector3 velocity = Vector3.zero;
	public Vector3 forceAcc = Vector3.zero;
	public Vector2 myPos2D;
	public float maxSpeed;
	public float mass = 0.1f;

	public GameObject pointer;
	GameObject target;
	RaycastHit whatIHit;
	public AudioClip attack, flee;
	public GameObject fleeSound;
	
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
		// moved reference caches to Entity class, as this now inherits from it
		base.Start();
		target = GameObject.Find ("RaycastTarget");
		currentState = EnemyStates.floating;
		mainCamera = GameObject.Find ("Main Camera");
		myTransform = this.transform;
		maxSpeed = Random.Range (.8f, 1.2f);
//maxSpeed = Random.Range (.6f, 1.1f);
	}

	// Update is called once per frame
	void Update ()
	{


		toPlayer1 = p1Transform.position - transform.position;
		toPlayer2 = p2Transform.position - transform.position;
		//OffScreenIndicator(); //disabled until improved
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
		//Debug.Log ("Enemy is floating (green)");
		transform.renderer.material.SetColor("_Emission", Color.green);
		
		if( toPlayer1.magnitude > detectorRadius && toPlayer1.magnitude < 15f)
		{
			currentState = EnemyStates.following;
			//Debug.Log("Won't change state to following...");
		}
		
		if ( toPlayer1.magnitude < detectDist)
		{
			currentState = EnemyStates.following;
		}
	}

	void Follow() //Enemy follows player1, seeks the feeder if feeding, or 
	{
		//Debug.Log ("Enemy is following (yellow)");
		transform.renderer.material.SetColor("_Emission", Color.yellow);


		if( (toPlayer1.magnitude < 8f))
		{
			forceAcc += Arrive(feederGO.transform.position);
		}
		else
		{
			currentState = EnemyStates.floating;
		}

		if (toPlayer1.magnitude < detectDist)
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
	//	Debug.Log ("Enemy is in close range (red)");
		transform.renderer.material.SetColor("_Emission", Color.red);	
	}

	void Feeding()
	{
		//Debug.Log ("Enemy is feeding");
	}

	void Fighting()
	{
		//Debug.Log ("Enemy is fighting");
		transform.renderer.material.SetColor("_Emission", Color.red);	

		attackTime += Time.deltaTime;
		if(attackTime >= attackTimer)
		{
			//SoundClip(attack);
			audio.Play ();
			feederGO.BroadcastMessage("AdjustHealth", damage);
			attackTime = 0;
		}

		if(toPlayer1.magnitude > closeRange)
		{
			currentState = EnemyStates.following;
			attackTime = 0;
		}
	}

	void Fleeing()
	{
		transform.renderer.material.SetColor("_Emission", Color.red);	

		forceAcc += Flee(protectorGO);	// scared

		if(!createSFX)
		{
			GameObject fleeSFX = Instantiate(fleeSound, transform.position, transform.rotation) as GameObject;
			createSFX = true;

		}
		
		if( toPlayer2.magnitude > Random.Range(10,14) )
		{
			createSFX = false;
			currentState = EnemyStates.following;
		}
	}

	void Dying()
	{

	}

	void CheckPlayer2Dist()
	{
		if( toPlayer2.magnitude < closeRange)
		{
			//currentState = EnemyStates.fleeing;
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
		float fleeDist = Random.Range(1.2f, 1.8f);
		
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
		const float DecelerationTweak = 2.5f;
		
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

	public void TakeDamage(int dmg)
	{
		health += dmg;
		if(health <= 0)
		{
			Destroy(gameObject);
		}
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

	void OffScreenIndicator() //enemy indicators when offscreen
	{
		float camYTop = mainCamera.transform.position.y + 5.8f * pControls.playerDistance() / 4.32f;
		float camYBottom = mainCamera.transform.position.y - 5.8f * pControls.playerDistance() / 4.32f;
		float camXRight = mainCamera.transform.position.x + 9.6f * pControls.playerDistance() / 4.32f;
		float camXLeft = mainCamera.transform.position.x - 9.6f * pControls.playerDistance() / 4.32f;

		//Debug.DrawLine(rayCast.transform.position, target.transform.position, Color.magenta);

		if(Physics.Linecast(transform.position, target.transform.position, out whatIHit, 1 << LayerMask.NameToLayer("Bounding")))
		{
			pointer.transform.position = whatIHit.point; //

			//Rotate pointer towards enemy transform
			Quaternion newRotation = Quaternion.LookRotation(pointer.transform.position - myTransform.position, Vector3.forward);
			newRotation.x = 0f;
			newRotation.y = 0f;
			pointer.transform.rotation = Quaternion.Slerp(pointer.transform.rotation, newRotation, Time.deltaTime * 12);
			//end pointer rotation

			if(transform.position.y < camYBottom || transform.position.y > camYTop 
			   || transform.position.x < camXLeft || transform.position.x > camXRight)
			{
				pointer.GetComponent<SpriteRenderer>().enabled = true;
			}
			else
			{
				pointer.GetComponent<SpriteRenderer>().enabled = false;
			}
		}
	}

	public void SoundClip(AudioClip clip)
	{
		audio.audio.clip = clip; //TODO check audio in prefabs
		audio.Play ();
	}
}// end class

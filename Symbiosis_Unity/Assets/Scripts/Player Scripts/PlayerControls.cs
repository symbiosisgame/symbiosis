using UnityEngine;
using System.Collections;

public class PlayerControls : PlayerManager {

	public float acceleration, maxSpeed;
	public float turnAcceleration, maxTurnSpeed;
	public float decceleration = 0.95f, despin = 0.93f; //for slowing down move and turn speed

	private float currSpeedP1, currSpeedP2; //not used yet, just stores current speeds
	private float horiz1, vert1, horiz2, vert2; //stores value of axis, less or greater than 0 determines positive/negative direction
	public float maxVelocityChange = 10f;
	bool pushControls = true;

	new void Start () 
	{   
		base.Start ();
		feederGO = GameObject.Find("Player1");
		feeder = feederGO.GetComponent<Feeder>();
		protectorGO = GameObject.Find("Player2");
		protector = protectorGO.GetComponent<Protector>();
	
		protAnim["ProtTurnLeft"].speed = 0.5f;
		protAnim["ProtTurnRight"].speed = 0.5f ;
		feederAnim["FeederTurnLeft"].speed = 0.5f;
		feederAnim["FeederTurnRight"].speed = 0.5f;
	}

	void FixedUpdate () //better for physics calculations
	{
        Controls();
		CalculateDistance();
	}

	void Update()
	{
		Player1Ability();
		Player2Ability();
		InputScheme();
	}

	void Player1Ability()
	{
		if(Input.GetButtonDown ("TransferFood")) //add xbox button
		{
			if(feeder.currentFood > 0)
			{
				feeder.transferring = !feeder.transferring;
			}
		}
	}

	void Player2Ability()
	{
		if(Input.GetButtonDown ("Taunt")) //add xbox button
		{
			if(protector.currentFood > 0)
			{
				protAnim.CrossFade("ProtTaunt", .4f);
				protector.Taunt(protectorGO.transform.position, 2f, -1);
			}
		}
	}

	void InputScheme()
	{
		if(Input.GetKeyDown(KeyCode.J))
		{
			pushControls = !pushControls;
		}
	}

	void StoreAxis()
	{
		if(pushControls)
		{
			horiz1 = Input.GetAxis("HorizontalP1Push");
			horiz2 = Input.GetAxis("HorizontalP2Push");
			maxSpeed = 6f;
		}
		else
		{
			horiz1 = Input.GetAxis("HorizontalP1Joy");
			vert1 = Input.GetAxis("VerticalP1Joy");
			horiz2 = Input.GetAxis("HorizontalP2Joy");
			vert2 = Input.GetAxis("VerticalP2Joy");
		}
	}

    void Controls()
    {
		StoreAxis();
		MovePlayer1();
		MovePlayer2();
	}

	void MovePlayer1()
    {
		if(pushControls)
		{
			if(Input.GetButtonDown("P1Push"))
			{
				Vector3 targetVelocity = new Vector3(0, 1, 0f );
				targetVelocity *= maxSpeed;
				
				// Apply a force that attempts to reach our target velocity
				Vector3 velocity = player1.rigidbody.velocity;
				Vector3 velocityChange = (targetVelocity - velocity);
				velocityChange.x = 0;
				velocityChange.z = 0;
				velocityChange.y = Mathf.Clamp(velocityChange.y, -maxVelocityChange, maxVelocityChange);
				player1.rigidbody.AddRelativeForce(velocityChange, ForceMode.VelocityChange);
			}
		}
		else
		{
			if(player1.rigidbody.velocity.magnitude < maxSpeed)
			{
				player1.rigidbody.AddForce(p1Transform.up * vert1 * acceleration * Time.deltaTime);
			}
		}

		if(player1.rigidbody.angularVelocity.magnitude < maxTurnSpeed)
		{
			player1.rigidbody.AddRelativeTorque(-p1Transform.forward * horiz1 * turnAcceleration * Time.deltaTime);
		}

		currSpeedP1 = player1.rigidbody.velocity.magnitude;
		if(currSpeedP1 <= 0.01f)
		{
			currSpeedP1 = 0;
			feederAnim.CrossFade("FeederIdle", .2f);
		}
		
		if(horiz1 < 0)
		{
			//feederAnim.CrossFade("FeederTurnLeft", .2f); 
		}
		else if(horiz1 > 0)
		{
			//feederAnim.CrossFade("FeederTurnRight", .2f);
		}
		else
		{
			feederAnim.CrossFade("FeederIdle", .2f);
		}
    }

	void MovePlayer2()
	{
		if(pushControls)
		{
			if(Input.GetButtonDown("P2Push"))
			{
				Vector3 targetVelocity = new Vector3(0, 1, 0f );
				targetVelocity *= maxSpeed;
				
				// Apply a force that attempts to reach our target velocity
				Vector3 velocity = player2.rigidbody.velocity;
				Vector3 velocityChange = (targetVelocity - velocity);
				velocityChange.x = 0;
				velocityChange.z = 0;
				velocityChange.y = Mathf.Clamp(velocityChange.y, -maxVelocityChange, maxVelocityChange);
				player2.rigidbody.AddRelativeForce(velocityChange, ForceMode.VelocityChange);
			}
		}
		else
		{
			if(player2.rigidbody.velocity.magnitude < maxSpeed)
			player2.rigidbody.AddForce(p2Transform.up * vert2 * acceleration * Time.deltaTime);

		}


		if(player2.rigidbody.angularVelocity.magnitude < maxTurnSpeed)
		{
			player2.rigidbody.AddTorque(-p2Transform.forward * horiz2 * turnAcceleration * Time.deltaTime);
		}
		
		currSpeedP2 = player2.rigidbody.velocity.magnitude;
		if(currSpeedP2 <= 0.01f)
		{
			currSpeedP2 = 0;

		}

		if(vert2 < 0)
		{
			protAnim.CrossFade("ProtMove", .6f);
		}
		else if(vert2 > 0)
		{
			protAnim.CrossFade("ProtMove", .6f);
		}
		else
		{
			protAnim.CrossFade("ProtIdle", .4f); 
		}
		if(horiz2 < 0)
		{
			//protAnim.CrossFade("ProtTurnRight", .4f); 
		}
		if(horiz2 > 0)
		{
			//protAnim.CrossFade("ProtTurnLeft", .4f);
		}
	}
	
	void CalculateDistance() //used for camera tracking and distance checking between players
	{
		distanceVec = (p1Transform.position + p2Transform.position) / 2;
		distance = Vector3.Distance(p1Transform.position, p2Transform.position) / 2;
		distanceTo = Vector3.Distance(p1Transform.position, p2Transform.position);
	}
	
	public float playerDistance() //returns the distance float divided by 2 (only used for camera stuff)
	{
		return distance;
	}
	
	public Vector3 distanceVector() //returns the distance vector
	{
		return distanceVec;
	}

	public float playerDistanceReal() //returns the distance vector
	{
		return distanceTo;
	}

}

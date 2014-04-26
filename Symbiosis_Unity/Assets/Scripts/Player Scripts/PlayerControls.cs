using UnityEngine;
using System.Collections;
using XboxCtrlrInput;

public class PlayerControls : PlayerManager {

	public float acceleration, maxSpeed;
	public float turnAcceleration, maxTurnSpeed;
	//public float decceleration = 0.95f, despin = 0.93f; //for slowing down move and turn speed

	private float currSpeedP1, currSpeedP2; //not used yet, just stores current speeds
	private float forwardP1, forwardP2, reverseP1, reverseP2, rotateP1, rotateP2 ;//stores value of axis, less or greater than 0 determines positive/negative direction

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
		StoreAxis();
		CalculateDistance();
		MovePlayer1();
		MovePlayer2();
		Player1Ability();
		Player2Ability();
		InputScheme();
	}

	void InputScheme()
	{
		if(XCI.GetButtonDown(XboxButton.Back))
		{
			Debug.Log ("Lol");
			//triggerControls = !triggerControls;
		}
	}

	void StoreAxis()
	{
		//joypad controls
		//player 1
		forwardP1 = Input.GetAxis ("ForwardTriggerP1"); //right trigger move forward
		reverseP1 = Input.GetAxis("ReverseTriggerP1"); //left trigger move backward
		rotateP1 = Input.GetAxis("RotateStickP1"); //left stick for turning

		//player 2
		forwardP2 = Input.GetAxis ("ForwardTriggerP2"); //right trigger move forward
		reverseP2 = Input.GetAxis("ReverseTriggerP2"); //left trigger move backward
		rotateP2 = Input.GetAxis("RotateStickP2"); //left stick for turning
		
	}
	
	void MovePlayer1()
    {
		if(player1.rigidbody.velocity.magnitude < maxSpeed)
		{
			player1.rigidbody.AddForce(p1Transform.up * forwardP1 * acceleration * Time.deltaTime); //joypad
			player1.rigidbody.AddForce(-p1Transform.up * reverseP1 * acceleration * Time.deltaTime); //joypad
		}
		
		if(player1.rigidbody.angularVelocity.magnitude < maxTurnSpeed) //this is to handle the rotation, both joypad and keyboard
		{
			player1.rigidbody.AddRelativeTorque(-p1Transform.forward * rotateP1 * turnAcceleration * Time.deltaTime); //joypad
		}

		/*currSpeedP1 = player1.rigidbody.velocity.magnitude;
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
		}*/
    }

	void MovePlayer2()
	{
		if(player2.rigidbody.velocity.magnitude < maxSpeed)
		{
			player2.rigidbody.AddForce(p2Transform.up * forwardP2 * acceleration * Time.deltaTime);//joypad
			player2.rigidbody.AddForce(-p2Transform.up * reverseP2 * acceleration * Time.deltaTime); //joypad
		}

		if(player2.rigidbody.angularVelocity.magnitude < maxTurnSpeed) //rotation player 2
		{
			player2.rigidbody.AddRelativeTorque(-p2Transform.forward * rotateP2 * turnAcceleration * Time.deltaTime);//joypad
		}
		
		/*currSpeedP2 = player2.rigidbody.velocity.magnitude;
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
		}*/
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

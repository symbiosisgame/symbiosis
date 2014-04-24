using UnityEngine;
using System.Collections;

public class PlayerControls : PlayerManager {

	public float acceleration, maxSpeed;
	public float turnAcceleration, maxTurnSpeed;
	public float decceleration = 0.95f, despin = 0.93f; //for slowing down move and turn speed

	private float currSpeedP1, currSpeedP2; //not used yet, just stores current speeds
	private float horiz1, vert1, horiz2, vert2, horiz3, vert3, horiz4, vert4, reverse1, reverse2; //stores value of axis, less or greater than 0 determines positive/negative direction
	bool triggerControls = true;

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
		if(Input.GetButtonDown("Select"))
		{
			triggerControls = !triggerControls;
		}
	}

	void StoreAxis()
	{
		if(triggerControls)
		{
			//joypad controls
			//player 1
			vert1 = Input.GetAxis ("VerticalP1Trigger"); //right trigger move forward
			reverse1 = Input.GetAxis("ReverseP1Trigger"); //left trigger move backward
			horiz1 = Input.GetAxis("HorizontalP1Trigger"); //left stick for turning

			//player 2
			vert2 = Input.GetAxis ("VerticalP2Trigger"); //right trigger move forward
			reverse2 = Input.GetAxis("ReverseP2Trigger"); //left trigger move backward
			horiz2 = Input.GetAxis("HorizontalP2Trigger"); //left stick for turning

			//keyboard controls
			horiz3 = Input.GetAxis("HorizontalP1"); //P1 A & D rotate left & right
			vert3 = Input.GetAxis("VerticalP1"); //P1 W & S move forward & back
			horiz4 = Input.GetAxis("HorizontalP2"); //P2 left & right rotate left & right
			vert4 = Input.GetAxis("VerticalP2"); //P2 up & down move forward & back
			//maxSpeed = 6f;*/
		}
		else
		{
			//joypad controls
			vert1 = Input.GetAxis("VerticalP1Joy");
			horiz1 = Input.GetAxis("HorizontalP1Joy"); 
			vert2 = Input.GetAxis("VerticalP2Joy"); //right stick rotate
			horiz2 = Input.GetAxis("HorizontalP2Joy"); 

			//keyboard controls
			vert3 = Input.GetAxis("VerticalP1");
			horiz3 = Input.GetAxis("HorizontalP1"); 
			vert4 = Input.GetAxis("VerticalP2");
			horiz4 = Input.GetAxis("HorizontalP2");
		}
	}
	
	void MovePlayer1()
    {
		if(triggerControls) //this set of controls is push trigger to move and keyboard controls
		{
			player1.rigidbody.AddForce(p1Transform.up * vert1 * acceleration * Time.deltaTime); //joypad
			player1.rigidbody.AddForce(-p1Transform.up * reverse1 * acceleration * Time.deltaTime); //joypad

			//player1.rigidbody.AddForce(p1Transform.up * vert3 * acceleration * Time.deltaTime); //keyboard
		}
		else //this set of controls is the movement mapped to the analog sticks on control and keyboard controls
		{
			if(player1.rigidbody.velocity.magnitude < maxSpeed)
			{
				player1.rigidbody.AddForce(p1Transform.up * vert1 * acceleration * Time.deltaTime); //joypad
				//player1.rigidbody.AddForce(p1Transform.up * vert3 * acceleration * Time.deltaTime); //keyboard
			}
		}

		if(player1.rigidbody.angularVelocity.magnitude < maxTurnSpeed) //this is to handle the rotation, both joypad and keyboard
		{
			player1.rigidbody.AddRelativeTorque(-p1Transform.forward * horiz1 * turnAcceleration * Time.deltaTime); //joypad
			//player1.rigidbody.AddRelativeTorque(-p1Transform.forward * horiz3 * turnAcceleration * Time.deltaTime); //keyboard
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
		if(triggerControls) //this set of controls is push trigger to move and keyboard controls
		{
			player2.rigidbody.AddForce(p2Transform.up * vert2 * acceleration * Time.deltaTime);//joypad
			player2.rigidbody.AddForce(-p2Transform.up * reverse2 * acceleration * Time.deltaTime); //joypad
		//	player2.rigidbody.AddForce(p2Transform.up * vert4 * acceleration * Time.deltaTime);//keyboard
		}
		else //this set of controls is the movement mapped to the analog sticks on control and keyboard controls
		{
			if(player2.rigidbody.velocity.magnitude < maxSpeed)
			{
				player2.rigidbody.AddForce(p2Transform.up * vert2 * acceleration * Time.deltaTime);//joypad
				//player2.rigidbody.AddForce(p2Transform.up * vert4 * acceleration * Time.deltaTime);//keyboard
			}
		}


		if(player2.rigidbody.angularVelocity.magnitude < maxTurnSpeed) //rotation player 2
		{
			player2.rigidbody.AddRelativeTorque(-p2Transform.forward * horiz2 * turnAcceleration * Time.deltaTime);//joypad
			//player2.rigidbody.AddRelativeTorque(-p2Transform.forward * horiz4 * turnAcceleration * Time.deltaTime);//keyboard
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

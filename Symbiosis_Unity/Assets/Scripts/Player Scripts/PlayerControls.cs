﻿using UnityEngine;
using System.Collections;

public class PlayerControls : PlayerManager {

	public float acceleration, maxSpeed;
	public float turnAcceleration, maxTurnSpeed;
	//public float decceleration = 0.95f, despin = 0.93f; //for slowing down move and turn speed

	private float currSpeedP1, currSpeedP2; //not used yet, just stores current speeds
	private float forwardP1, forwardP2, reverseP1, reverseP2, rotateP1, rotateP2, moveKeyboardP1, moveKeyboardP2, rotateKeyboardP1, rotateKeyboardP2, moveJoyP1, moveJoyP2, rotateJoyP1, rotateJoyP2; //stores value of axis, less or greater than 0 determines positive/negative direction
	bool alternateControls = true;

	new void Start () 
	{   
		base.Start ();
		feederGO = GameObject.Find("Player1");
		feeder = feederGO.GetComponent<Feeder>();
		protectorGO = GameObject.Find("Player2");
		protector = protectorGO.GetComponent<Protector>();
	}

	void Update()
	{
		StoreAxis();
		CalculateDistance();
		Player1Ability();
		Player2Ability();
		InputScheme();
	}

	void FixedUpdate () //better for physics calculations
	{
		MovePlayer1();
		MovePlayer2();
	}

	void InputScheme()
	{
		if(Input.GetButtonDown("Select"))
		{
			alternateControls = !alternateControls;
		}
	}

	void StoreAxis()
	{
		if(alternateControls)
		{
			//joypad controls, button to move
			//player 1
			forwardP1 = Input.GetAxis ("ForwardTriggerP1"); //right trigger move forward
			reverseP1 = Input.GetAxis("ReverseTriggerP1"); //left trigger move backward
			rotateP1 = Input.GetAxis("RotateStickP1"); //left stick for turning
		//player 2
			forwardP2 = Input.GetAxis ("ForwardTriggerP2"); //right trigger move forward
			reverseP2 = Input.GetAxis("ReverseTriggerP2"); //left trigger move backward
			rotateP2 = Input.GetAxis("RotateStickP2"); //left stick for turning
		}
		else
		{
			//alternate joypad controls, tank
			//player 1
			forwardP1 = Input.GetAxis("VerticalP1Joy");
			rotateP1 = Input.GetAxis("HorizontalP1Joy"); 
			//player 2
			forwardP2 = Input.GetAxis("VerticalP2Joy"); //right stick rotate
			rotateP2 = Input.GetAxis("HorizontalP2Joy"); 
		}
		moveKeyboardP1 = Input.GetAxis("MoveKeyboardP1"); //P1 W & S move forward & back
		rotateKeyboardP1 = Input.GetAxis("RotateKeyboardP1"); //P1 A & D rotate left & right
		moveKeyboardP2 = Input.GetAxis("MoveKeyboardP2"); //P2 up & down move forward & back
		rotateKeyboardP2 = Input.GetAxis("RotateKeyboardP2"); //P2 left & right rotate left & right
	}
	
	void MovePlayer1()
    {
		if(alternateControls) //this set of controls is push trigger to move and keyboard controls
		{
			if(player1.rigidbody.velocity.magnitude < maxSpeed)
			{
				if(Input.GetButton("ForwardTriggerP1"))
				{
					player1.rigidbody.AddForce(p1Transform.up * acceleration * Time.deltaTime); 
				}
				if(Input.GetButton("ReverseTriggerP1"))
				{
					player1.rigidbody.AddForce(-p1Transform.up * acceleration * Time.deltaTime); 
				}
			}
			if(player1.rigidbody.angularVelocity.magnitude < maxTurnSpeed) //this is to handle the rotation, both joypad and keyboard
			{
				player1.rigidbody.AddRelativeTorque(-p1Transform.forward * rotateP1 * turnAcceleration * Time.deltaTime); //joypad
			}
		}
		else //this set of controls is the movement mapped to the analog sticks on control and keyboard controls
		{
			if(player1.rigidbody.velocity.magnitude < maxSpeed)
			{
				player1.rigidbody.AddForce(p1Transform.up * forwardP1 * acceleration * Time.deltaTime); //joypad
			}
			if(player1.rigidbody.angularVelocity.magnitude < maxTurnSpeed) //this is to handle the rotation, both joypad and keyboard
			{
				player1.rigidbody.AddRelativeTorque(-p1Transform.forward * rotateP1 * turnAcceleration * Time.deltaTime); //joypad
			}
		}
		//Keyboard controls
		if(player1.rigidbody.velocity.magnitude < maxSpeed)
		{
			player1.rigidbody.AddForce(p1Transform.up * moveKeyboardP1 * acceleration * Time.deltaTime); //keyboard movement
		}
		if(player1.rigidbody.angularVelocity.magnitude < maxTurnSpeed) //this is to handle the rotation, both joypad and keyboard
		{
			player1.rigidbody.AddRelativeTorque(-p1Transform.forward * rotateKeyboardP1 * turnAcceleration * Time.deltaTime); //keyboard movement
		}
		currSpeedP1 = player1.rigidbody.velocity.magnitude;
		if(currSpeedP1 <= 0.01f)
		{
			currSpeedP1 = 0;
			feederAnim.CrossFade("FeederIdle", .2f);
		}
		else
		{
			feederAnim.CrossFade("FeederMove", .2f);
		}
    }

	void MovePlayer2()
	{
		if(alternateControls) //this set of controls is push trigger to move and keyboard controls
		{
			if(player2.rigidbody.velocity.magnitude < maxSpeed)
			{
				if(Input.GetButton("ForwardTriggerP2"))
				{
					player2.rigidbody.AddForce(p2Transform.up * acceleration * Time.deltaTime); //joypad
				}
				if(Input.GetButton("ReverseTriggerP2"))
				{
					player2.rigidbody.AddForce(-p2Transform.up * acceleration * Time.deltaTime); //joypad
				}
			}
			if(player2.rigidbody.angularVelocity.magnitude < maxTurnSpeed) //this is to handle the rotation, both joypad and keyboard
			{
				player2.rigidbody.AddRelativeTorque(-p2Transform.forward * rotateP2 * turnAcceleration * Time.deltaTime); //joypad
			}
		}
		else //this set of controls is the movement mapped to the analog sticks on control and keyboard controls
		{
			if(player2.rigidbody.velocity.magnitude < maxSpeed)
			{
				player2.rigidbody.AddForce(p2Transform.up * forwardP2 * acceleration * Time.deltaTime); //joypad
			}
			if(player2.rigidbody.angularVelocity.magnitude < maxTurnSpeed) //this is to handle the rotation, both joypad and keyboard
			{
				player2.rigidbody.AddRelativeTorque(-p2Transform.forward * rotateP2 * turnAcceleration * Time.deltaTime); //joypad
			}
		}
		//Keyboard controls
		if(player2.rigidbody.velocity.magnitude < maxSpeed)
		{
			player2.rigidbody.AddForce(p2Transform.up * moveKeyboardP2 * acceleration * Time.deltaTime); //keyboard movement
		}
		if(player2.rigidbody.angularVelocity.magnitude < maxTurnSpeed) //this is to handle the rotation, both joypad and keyboard
		{
			player2.rigidbody.AddRelativeTorque(-p2Transform.forward * rotateKeyboardP2 * turnAcceleration * Time.deltaTime); //keyboard movement
		}
		
		currSpeedP2 = player2.rigidbody.velocity.magnitude;
		if(currSpeedP2 <= 0.01f)
		{
			currSpeedP2 = 0f;
			protAnim.CrossFade("ProtIdle", .6f);
		}
		else
		{
			protAnim.CrossFade("ProtMove", .6f);
		}
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
		if(Input.GetButtonDown ("Taunt")) 
		{
			if(protector.currentFood > 0)
			{
				protAnim.Play("ProtTaunt");
				protector.Taunt(protectorGO.transform.position, 2.5f, -1);
			}
		}
		if(Input.GetButtonDown ("Shield")) 
		{
			if(protector.currentFood > 0)
			{
				protector.Shield();
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

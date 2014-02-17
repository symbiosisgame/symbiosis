﻿using UnityEngine;
using System.Collections;

public class PlayerControls : PlayerManager {

	public float acceleration, maxSpeed;
	public float turnAcceleration, maxTurnSpeed;
	public float decceleration = 0.95f, despin = 0.93f; //for slowing down move and turn speed

	private float currSpeedP1, currSpeedP2; //not used yet, just stores current speeds
	private float horiz1, vert1, horiz2, vert2; //stores value of axis, less or greater than 0 determines positive/negative direction
    [HideInInspector] public bool protectkey; //stores whether the player is pressing the key to use the shield or not. 
	new void Start () 
	{   
		base.Start ();
	}

	void FixedUpdate () //better for physics calculations
	{
        Controls();
		CalculateDistance();
	}

    void Controls()
    {
		if(keyboard)
		{
			horiz1 = Input.GetAxis("HorizontalP1");
			vert1 = Input.GetAxis("VerticalP1");
			horiz2 = Input.GetAxis("HorizontalP2");
			vert2 = Input.GetAxis("VerticalP2");
            protectkey = Input.GetButton("Jump");
		}
		else
		{
			horiz1 = Input.GetAxis("HorizontalP1Joy");
			vert1 = Input.GetAxis("VerticalP1Joy");
			horiz2 = Input.GetAxis("HorizontalP2Joy");
			vert2 = Input.GetAxis("VerticalP2Joy");
		}
		MovePlayer1();
		MovePlayer2();
	}

	void MovePlayer1()
    {
		if(player1.rigidbody.velocity.magnitude < maxSpeed)
		{
			player1.rigidbody.AddForce(p1Transform.up * vert1 * acceleration * Time.deltaTime);
		}
		if(player1.rigidbody.angularVelocity.magnitude < maxTurnSpeed)
		{
			player1.rigidbody.AddTorque(-p1Transform.forward * horiz1 * turnAcceleration * Time.deltaTime);
		}

		currSpeedP1 = player1.rigidbody.velocity.magnitude;
		if(currSpeedP1 <= 0.01f)
		{
			currSpeedP1 = 0;
		}
    }

	void MovePlayer2()
	{
		if(player2.rigidbody.velocity.magnitude < maxSpeed)
		{
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
	}
	
	void CalculateDistance() //used for camera tracking and distance checking between players
	{
		distanceVec = (p1Transform.position + p2Transform.position) / 2;
		distance = Vector3.Distance(p1Transform.position, p2Transform.position) / 2;
	}

	public float playerDistance() //returns the distance float 
	{
		return distance;
	}

	public Vector3 distanceVector() //returns the distance vector
	{
		return distanceVec;
	}
}

using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	GameObject playerManager;
	PlayerControls playerControls;
	Transform myTransform;
	Vector3 distance;
	
	void Start () 
	{
		playerManager = GameObject.Find("PlayerManager");
		playerControls = playerManager.GetComponent<PlayerControls>();
		myTransform = this.transform;	
	}
	
	void Update () 
	{
		CalculateDistance();
	}

	void CalculateDistance() //Calculate distance between both players and half it
	{
		distance = (playerControls.p1Position() + playerControls.p2Position()) / 2;
		myTransform.position = distance;
	}

	public Vector3 playerDistance()
	{
		return distance;
	}
}

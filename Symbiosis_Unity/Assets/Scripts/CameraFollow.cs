using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	GameObject playerManager;
	PlayerControls playerControls;
	Transform myTransform;
	Vector3 distanceVec;
	float distance;
	
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
		distanceVec = (playerControls.p1Position() + playerControls.p2Position()) / 2;
		distance = Vector3.Distance(playerControls.p1Position(),playerControls.p2Position()) / 2;
		Debug.Log (distance);
		myTransform.position = distanceVec;
	}

	public float playerDistance()
	{
		return distance;
	}
}

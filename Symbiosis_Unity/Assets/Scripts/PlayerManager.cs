using UnityEngine;
using System.Collections;

//Player Manager is the base class for PlayerControls, Feeder, Protector. 

public class PlayerManager : MonoBehaviour {
	
	[HideInInspector]public bool keyboard = true;
	[HideInInspector]public float distance;
	[HideInInspector]public float maxPlayerDistance;
	
	[HideInInspector]public Transform p1Transform, p2Transform; //cache the transform components of both players
	[HideInInspector]public GameObject player1, player2; //cache player gameobjects
	[HideInInspector]public Vector3 distanceVec;
	
	[HideInInspector]public bool p1Alive;
	[HideInInspector]public bool p2Alive;

	public int currentFood;
	
	protected void Start () 
	{
		player1 = GameObject.Find("Player1");
		player2 = GameObject.Find("Player2");
		p1Transform = player1.transform;
		p2Transform = player2.transform;
	}

	void Update()
	{
		ControlScheme();
	}

	void ControlScheme()
	{
		if(Input.GetKeyDown(KeyCode.J))
		{
			keyboard = !keyboard;
		}
	}

	public void IncreaseFood(int addFood) //called by Feeder/Protector class when receiving food
	{
		currentFood += addFood;
	}
}

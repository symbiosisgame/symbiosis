using UnityEngine;
using System.Collections;

//Player Manager is the base class for PlayerControls, Feeder, Protector. 

public class PlayerManager : MonoBehaviour {
	
	[HideInInspector]public bool keyboard = true;
	[HideInInspector]public float distance, distanceTo;
	[HideInInspector]public float maxPlayerDistance;
	
	[HideInInspector]public Transform p1Transform, p2Transform; //cache the transform components of both players
	[HideInInspector]public GameObject player1, player2; //cache player gameobjects
	[HideInInspector]public Vector3 distanceVec;
	
	[HideInInspector]public bool p1Alive;
	[HideInInspector]public bool p2Alive;
	
	[HideInInspector]public GameObject feederGO, protectorGO;
	[HideInInspector]public Feeder feeder;
	[HideInInspector]public Protector protector;

	[HideInInspector]public GameObject healthTextGO, foodTextGO;

	public int currentFood;
	public int health, maxHealth;

	protected void Start () 
	{
		player1 = GameObject.Find("Player1");
		player2 = GameObject.Find("Player2");

		p1Transform = player1.transform;
		p2Transform = player2.transform;

		feeder = player1.GetComponent<Feeder>();
		protector = player2.GetComponent<Protector>();
	}

	public void AdjustHealth(int adj)
	{
		health += adj;
		if(health >= maxHealth)
		{
			health = maxHealth;
		}
		if(health <= 0)
		{
			Application.LoadLevel(Application.loadedLevelName);
		}
	}

	public void AdjustFood(int adj) //called by Feeder/Protector class when receiving food
	{
		currentFood += adj;
	}
}

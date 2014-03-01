using UnityEngine;
using System.Collections;

public class Entities : MonoBehaviour {

	[HideInInspector]public GameObject playerManager, mainCamera;
	[HideInInspector]public PlayerManager pManager;
	[HideInInspector]public GameObject feederGO, protectorGO;
	[HideInInspector]public Feeder feeder;
	[HideInInspector]public Protector protector;
	[HideInInspector]public Transform p1Transform, p2Transform;
	
	public int health = 2;
    
	protected void Start () 
	{
		feederGO = GameObject.Find ("Player1");
		protectorGO = GameObject.Find ("Player2");
		playerManager = GameObject.Find ("PlayerManager");

		feeder = feederGO.GetComponent<Feeder>();
		protector = feederGO.GetComponent<Protector>();
		pManager = playerManager.GetComponent<PlayerManager>();
	
		p1Transform = feederGO.transform;
		p2Transform = protectorGO.transform;
		mainCamera = GameObject.Find ("Main Camera");
	}
}

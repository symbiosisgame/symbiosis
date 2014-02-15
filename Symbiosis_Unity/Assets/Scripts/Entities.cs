using UnityEngine;
using System.Collections;

public class Entities : MonoBehaviour {

	[HideInInspector]public GameObject playerManager;
	[HideInInspector]public PlayerManager pManager;
	[HideInInspector]public GameObject feederGO;
	[HideInInspector]public Feeder feeder;

	void Start () 
	{
		playerManager = GameObject.Find ("PlayerManager");
		pManager = playerManager.GetComponent<PlayerManager>();
		feederGO = GameObject.Find ("Player1");
		feeder = feederGO.GetComponent<Feeder>();
	}
}

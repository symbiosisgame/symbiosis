using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {

	public GUISkin mySkin;
	GameObject feederGO, protectorGO;
	Feeder feeder;
	Protector protector;

	void Start () 
	{
		feederGO = GameObject.Find ("Player1");
		protectorGO = GameObject.Find ("Player2");
		feeder = feederGO.GetComponent<Feeder>();
		protector = protectorGO.GetComponent<Protector>();
	}

	void OnGUI()
	{
		GUI.skin = mySkin;
		GUI.Label(new Rect(10, 10, 300, 40), "Feeder Food: " +feeder.currentFood);
		GUI.Label(new Rect(Screen.width-240, 10, 300, 40), "Protector Food: " +protector.currentFood);
	}
}

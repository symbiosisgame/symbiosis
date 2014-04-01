using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {

	public GUISkin mySkin;
	GameObject feederGO, protectorGO, levelManager;
	public Texture huskBar;
	Feeder feeder;
	Protector protector;
	LevelManager lm;

	void Start () 
	{
		/*feederGO = GameObject.Find ("Player1");
		protectorGO = GameObject.Find ("Player2");
		feeder = feederGO.GetComponent<Feeder>();
		protector = protectorGO.GetComponent<Protector>();*/
		levelManager = GameObject.Find ("LevelManager");
		lm = levelManager.GetComponent<LevelManager>();
	}

	void OnGUI()
	{
		GUI.skin = mySkin;
		//GUI.Label(new Rect(Screen.width/2-50, 10, 300, 40), "Husk: " +lm.huskClean);
		GUI.DrawTexture(new Rect(Screen.width/2-50, 10, +lm.huskClean, 10), huskBar);
	}
}

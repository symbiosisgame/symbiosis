using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {

	public GUISkin mySkin;
	GameObject feederGO, protectorGO, levelManager;
	public Texture huskBar, huskBarBack, feederBar, protectorBar, protectorBar2;
	public float xPos, yPos, xPos1, xPos2;
	Feeder feeder;
	Protector protector;
	LevelManager lm;

	void Start () 
	{
		feederGO = GameObject.Find ("Player1");
		protectorGO = GameObject.Find ("Player2");
		feeder = feederGO.GetComponent<Feeder>();
		protector = protectorGO.GetComponent<Protector>();
		levelManager = GameObject.Find ("LevelManager");
		lm = levelManager.GetComponent<LevelManager>();

		// disable cursor
		Screen.showCursor = false;
	}

	void OnGUI()
	{
		GUI.skin = mySkin;
		//GUI.Label(new Rect(Screen.width/2-50, 10, 300, 40), "Husk: " +lm.huskClean);

		GUI.DrawTexture(new Rect(Screen.width/2-90, 10, +lm.levelCleaned * 3, 20), huskBar);
		//GUI.DrawTexture(new Rect(Screen.width/2-50, 10, +lm.levelCleaned * 2, 10), huskBarBack);
		GUI.DrawTexture(new Rect(Screen.width/2*0.57f, Screen.height-100f, feederBar.width, feederBar.height), feederBar);
		if(!Protector.shieldActive)
		{
			GUI.DrawTexture(new Rect(Screen.width/2*1.18f, Screen.height-100f, protectorBar.width, protectorBar.height), protectorBar);
		}
		else
		{
			GUI.DrawTexture(new Rect(Screen.width/2*1.18f, Screen.height-100f, protectorBar.width, protectorBar.height), protectorBar2);
		}

		GUI.Label (new Rect(Screen.width/2*xPos1, Screen.height-yPos, 50, 50), ""+feeder.currentFood);
		GUI.Label (new Rect(Screen.width/2*xPos2, Screen.height-yPos, 50, 50), ""+feeder.health);
		GUI.Label (new Rect(Screen.width/2*xPos, Screen.height-yPos, 50, 50), ""+protector.currentFood);
	}
}

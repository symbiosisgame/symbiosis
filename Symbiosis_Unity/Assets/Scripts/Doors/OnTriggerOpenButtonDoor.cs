using UnityEngine;
using System.Collections;

public class OnTriggerOpenButtonDoor : MonoBehaviour
{

	// doors
	public GameObject DoorLeftSide;
	public GameObject DoorRightSide;
	// anims
	private Animation AnimLeft; 
	private Animation AnimRight;

	private bool playerInTrigger = false;

	public bool opensWhenPlayer1_Entering = false;
	public bool closesWhenPlayer1_Entering = true;
	public bool opensWhenPlayer2_Entering = true;
	public bool closesWhenPlayer2_Entering = false;
	
	public bool opensWhenBoth_Entering = false;	// TODO

	public bool closesWhenPlayer1_Exiting = false;
	public bool closesWhenPlayer2_Exiting = false;

	public GameObject door;
	
	// Use this for initialization
	void Start ()
	{
		//DoorLeftSide = GameObject.Find ("Door-Left-Side");
		//DoorRightSide = GameObject.Find ("Door-Right-Side");
		AnimLeft = DoorLeftSide.GetComponent<Animation>();
		AnimRight = DoorRightSide.GetComponent<Animation>();

	}

	
	// Update is called once per frame
	void Update ()
	{
		if( playerInTrigger && !AnimLeft.IsPlaying("Door-Left-Open") )
		{
			playAnimForward();
			OpenDoor();
		}
	}


	// methods
	// -------
	
	// animation
	void playAnimForward()
	{
		AnimLeft.animation.wrapMode = WrapMode.ClampForever;
		AnimRight.animation.wrapMode = WrapMode.ClampForever;

		AnimLeft.animation["Door-Left-Open"].speed = 1.0f;	
		AnimRight.animation["Door-Right-Open"].speed = 1.0f;

		AnimLeft.animation["Door-Left-Open"].time = 0.0f;
		AnimRight.animation["Door-Right-Open"].time = 0.0f;

		AnimLeft.animation.CrossFade("Door-Left-Open");
		AnimRight.animation.CrossFade("Door-Right-Open");
	}

	void playAnimBackwards()
	{
		AnimLeft.animation.wrapMode = WrapMode.Once;
		AnimRight.animation.wrapMode = WrapMode.Once;

		AnimLeft.animation["Door-Left-Open"].speed = -1.5f;	// close faster
		AnimRight.animation["Door-Right-Open"].speed = -1.5f;

		AnimLeft.animation["Door-Left-Open"].time = AnimLeft.animation["Door-Left-Open"].length;
		AnimRight.animation["Door-Right-Open"].time = AnimRight.animation["Door-Right-Open"].length;

		AnimLeft.animation.CrossFade("Door-Left-Open");
		AnimRight.animation.CrossFade("Door-Right-Open");
	}
	/***
	void stopAnim()
	{
		AnimLeft.animation.Stop("Door-Left-Open");
		AnimRight.animation.Stop("Door-Right-Open");
		print ("STOP " + AnimLeft.isPlaying);
	}

	void rewindAnim()
	{
		AnimLeft.animation.Rewind("Door-Left-Open");
		AnimRight.animation.Rewind("Door-Right-Open");
		print ("REWIND " + AnimLeft.isPlaying);
	}
	***/

	void OpenDoor()
	{
		door.GetComponent<Animation>().CrossFade("DoorOpen", .4f);
		DoorLeftSide.SetActive(false);
		DoorRightSide.SetActive(false);
	}

	void CloseDoor()
	{
		door.GetComponent<Animation>().CrossFade("DoorClose", .4f);
		DoorLeftSide.SetActive(true);
		DoorRightSide.SetActive(true);
	}
	

	// triggers
	// --------

	void OnTriggerEnter( Collider other )
	{

		// open close for player 1 feeder
		if( other.name == "Player1" && opensWhenPlayer1_Entering && !playerInTrigger )
		{
			playerInTrigger = true;
		}
		else if( other.name == "Player1" && closesWhenPlayer1_Entering && AnimLeft.IsPlaying("Door-Left-Open") )
		{
			playerInTrigger = false;
			playAnimBackwards();
		}
		
		
		// open close for player 2 protector
		if( other.name == "Player2" && opensWhenPlayer2_Entering && !playerInTrigger )
		{
			playerInTrigger = true;
		}
		else if( other.name == "Player2" && closesWhenPlayer2_Entering && AnimLeft.IsPlaying("Door-Left-Open") )
		{
			playerInTrigger = false;
			playAnimBackwards();
		}
		


		// open for both players together
		if( opensWhenBoth_Entering && !playerInTrigger )
		{
			// TODO
		}
	}


	void OnTriggerExit( Collider other )
	{
		// close for player 1 feeder
		if( other.name == "Player1" && closesWhenPlayer1_Exiting && playerInTrigger && AnimLeft.IsPlaying("Door-Left-Open"))
		{
			playerInTrigger = false;
			playAnimBackwards();
		}

		// close for player 2 protector
		if( other.name == "Player2" && closesWhenPlayer2_Exiting && playerInTrigger && AnimLeft.IsPlaying("Door-Left-Open"))
		{
			playerInTrigger = false;
			playAnimBackwards();
		}
	}
	

	


}// end class

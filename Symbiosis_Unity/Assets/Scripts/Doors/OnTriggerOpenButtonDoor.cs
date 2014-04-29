using UnityEngine;
using System.Collections;

public class OnTriggerOpenButtonDoor : MonoBehaviour
{

	// doors
	public GameObject DoorLeftSide;
	public GameObject DoorRightSide;

	private bool playerInTrigger = false, doorOpened = false;

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
		door.GetComponent<Animation>()["DoorIdle"].speed = .1f;
	}

	void OpenDoor()
	{
		door.GetComponent<Animation>()["DoorOpen"].speed = 1f;
		door.GetComponent<Animation>().CrossFade("DoorOpen", .4f);
		DoorLeftSide.SetActive(false);
		DoorRightSide.SetActive(false);
	}

	void CloseDoor()
	{
		door.GetComponent<Animation>()["DoorOpen"].speed = -1f;
		door.GetComponent<Animation>().CrossFade("DoorOpen", .4f);
		DoorLeftSide.SetActive(true);
		DoorRightSide.SetActive(true);
	}
	

	// triggers
	// --------

	void OnTriggerEnter( Collider other )
	{
		if(!doorOpened)
		{
			if( other.name == "Player1" && opensWhenPlayer1_Entering)
			{
				playerInTrigger = true;
				doorOpened = true;
				if( playerInTrigger )
				{
					OpenDoor();
				}
			}
			
			if( other.name == "Player2" && opensWhenPlayer2_Entering)
			{
				playerInTrigger = true;
				doorOpened = true;
				if( playerInTrigger )
				{
					OpenDoor();
				}
			}
		}
	}


	/*void OnTriggerExit( Collider other )
	{
		if( other.name == "Player1" && opensWhenPlayer1_Entering)
		{
			CloseDoor();
			playerInTrigger = false;
		}
		
		if( other.name == "Player2" && opensWhenPlayer2_Entering)
		{
			CloseDoor();
			playerInTrigger = false;
		}
	}*/
	

	


}// end class

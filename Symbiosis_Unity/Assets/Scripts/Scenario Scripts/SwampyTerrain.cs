using UnityEngine;
using System.Collections;

public class SwampyTerrain : MonoBehaviour
{

    public Transform startPoint;
    public GameObject player;
	PlayerControls pControls;


    void Start()
    {
		pControls = GameObject.Find ("PlayerManager").GetComponent<PlayerControls>();
        startPoint = transform.Find("StartPoint");
    }
    void OnTriggerStay(Collider other)
    {
		if(pControls.playerDistanceReal() > 1.2f) 
		{
			if(!Protector.shieldActive)
			{
				if (other.tag == "Player")
				{
					other.GetComponent<Rigidbody>().drag += 0.1f;
					
					if (other.GetComponent<Rigidbody>().drag >= 15f)
					{
						other.gameObject.transform.position = startPoint.transform.position;
						other.GetComponent<Rigidbody>().drag = 3f;
					}
				}
			}
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {

            other.GetComponent<Rigidbody>().drag = 3f;

        }
    }
}
  

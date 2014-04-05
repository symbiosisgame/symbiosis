using UnityEngine;
using System.Collections;

public class BarrierFeederTrigger : Barrier {

 
	// Use this for initialization
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player1")
        {
            barrierFeederTrigger = true;
            Debug.Log("BarrierFeederTrigger" + barrierFeederTrigger);
        }
         
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player1")
            barrierFeederTrigger = false;        
    }
}
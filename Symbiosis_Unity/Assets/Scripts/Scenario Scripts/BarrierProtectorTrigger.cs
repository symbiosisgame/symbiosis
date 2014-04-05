using UnityEngine;
using System.Collections;

public class BarrierProtectorTrigger : Barrier
{


    // Use this for initialization
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player2")
        {
            barrierProtectorTrigger = true;
            Debug.Log("BarrierProtectorTrigger" + barrierProtectorTrigger);
        }
           
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player2")
            barrierProtectorTrigger = false;
    }
}
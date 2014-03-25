using UnityEngine;
using System.Collections;

public class BarrierProtectorTrigger : Barrier
{
    void Start()
    {
        barrierProtectorTrigger = false;
    }

    // Use this for initialization
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player2")
        {
            barrierProtectorTrigger = true;
            Debug.Log("BarrierProtectorTrigger" + barrierProtectorTrigger);
        }
           
    }

    void OnTriggerExit(Collider other)
    {
       // if (other.gameObject.name == "Player2")
           // barrierProtectorTrigger = false;
    }
}
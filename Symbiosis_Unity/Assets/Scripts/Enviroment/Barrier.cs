using UnityEngine;
using System.Collections;

public class Barrier : MonoBehaviour {
    [HideInInspector]
   public bool barrierFeederTrigger;
    [HideInInspector]
   public bool barrierProtectorTrigger;

    public enum whichTrigger { feeder, protector };
    public whichTrigger whichAmI;

    void OnTriggerEnter(Collider other)
    {
        if (whichAmI == whichTrigger.feeder)
        {
            if (other.gameObject.name == "Player1")
            {
                barrierFeederTrigger = true;
                Debug.Log("BarrierFeederTrigger" + barrierFeederTrigger);
            }
        }
        if (whichAmI == whichTrigger.protector)
        {
            if (other.gameObject.name == "Player2")
            {
                barrierProtectorTrigger = true;
                Debug.Log("BarrierProtectorTrigger" + barrierProtectorTrigger);
            }
        }
            if (barrierFeederTrigger && barrierProtectorTrigger)
            {
                Debug.Log("All activated");
                GameObject.Find("Barrier").renderer.enabled = false;
            }
        }
    

    void OnTriggerExit(Collider other)
    {
        if (whichAmI == whichTrigger.feeder)
        {
            if (other.gameObject.name == "Player1")
            {
                barrierFeederTrigger = false;
                Debug.Log("BarrierProtectorTrigger" + barrierProtectorTrigger);
            }
        }
        if (whichAmI == whichTrigger.protector)
        {
            if (other.gameObject.name == "Player2")
            {
                barrierProtectorTrigger = false;
                Debug.Log("BarrierProtectorTrigger" + barrierProtectorTrigger);
            }
        }
    }
}

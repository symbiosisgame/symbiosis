using UnityEngine;
using System.Collections;

public class Barrier : MonoBehaviour {
[HideInInspector]
   public bool barrierFeederTrigger;
    [HideInInspector]
   public bool barrierProtectorTrigger;

	void Update() {
        Debug.Log("Barrier Feeder:" + barrierFeederTrigger + " Barrier Protector:" + barrierProtectorTrigger);
      if (barrierFeederTrigger && barrierProtectorTrigger)
      { 
            Debug.Log("All activated");
           GameObject.Find("Barrier").renderer.enabled = false;
      }
	}
}

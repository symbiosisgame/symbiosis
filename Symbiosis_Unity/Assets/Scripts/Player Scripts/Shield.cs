using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {

	void OnTriggerStay(Collider other)
	{
		if(other.tag == "Enemy")
		{
			other.GetComponent<EnemyBehaviour>().currentState = EnemyBehaviour.EnemyStates.fleeing;
		}
	}

	void Update()
	{
		iTween.ScaleBy(gameObject, iTween.Hash("x", .9f, "y", .9f, "time", 1f, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));

	}
}

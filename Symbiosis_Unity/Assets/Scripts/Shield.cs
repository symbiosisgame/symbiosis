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
}

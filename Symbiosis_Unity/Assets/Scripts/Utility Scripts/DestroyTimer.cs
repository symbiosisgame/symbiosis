using UnityEngine;
using System.Collections;

public class DestroyTimer : MonoBehaviour {

	public float delayTime;
	
	void Start ()
	{
	StartCoroutine("delayDestroy");
	}

	IEnumerator delayDestroy()
	{
		yield return new WaitForSeconds(delayTime);
		Destroy(gameObject);
	}
	
}

using UnityEngine;
using System.Collections;

public class HilightByMaterial : MonoBehaviour {

	// switches materials slots on trigger
	
	public Material[] materials;
	
	void Start ()
	{
		collider.isTrigger = true;
		renderer.material = materials[0];
	}
	

	void OnTriggerEnter(Collider other)
	{
		renderer.material = materials[1];
	}
	

	void OnTriggerExit(Collider other)
	{
		renderer.material = materials[0];
	}
	

	void Update ()
	{

	}
	




}// end class

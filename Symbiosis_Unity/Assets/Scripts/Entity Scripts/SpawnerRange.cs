using UnityEngine;
using System.Collections;

public class SpawnerRange : MonoBehaviour {

 //This float determines how far from the foodspawner can food be instantiated.
    public float spawnRadius = 5.0F;

	void Start()
	{
		GetComponent<MeshRenderer>().enabled = false;
	}

    void OnDrawGizmosSelected() 
	{
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
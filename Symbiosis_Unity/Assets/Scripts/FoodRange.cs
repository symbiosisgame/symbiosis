using UnityEngine;
using System.Collections;

public class FoodRange : MonoBehaviour {

 //This float determines how far from the foodspawner can food be instantiated.
    public float spawnRadius = 5.0F;
    void OnDrawGizmosSelected() {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
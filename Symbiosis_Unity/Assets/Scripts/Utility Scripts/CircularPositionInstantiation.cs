using UnityEngine;
using System.Collections;

public class CircularPositionInstantiation : MonoBehaviour {
	
	// gui
	public Color gizmoColor = Color.cyan;
	public bool showGizmos = true;
	// vars
	public GameObject prefab;
	public int numberOfObjects = 3;
	public float radius = 0.15f;



	void Start()
	{
		for (int i = 0; i < numberOfObjects; i++) {
			float angle = i * Mathf.PI * 2 / numberOfObjects;
			Vector3 pos = new Vector3( Mathf.Cos(angle), Mathf.Sin(angle), 0  ) * radius;
			Instantiate(prefab, pos + transform.position, Quaternion.Euler(0,0,0));
		}
	}


	// gui
	void OnDrawGizmos()
	{
		if( showGizmos )
		{
			Gizmos.color = gizmoColor;
			Gizmos.DrawWireSphere( transform.position, radius );
		}
	}

	
}// end class
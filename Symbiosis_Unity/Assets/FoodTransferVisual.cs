using UnityEngine;
using System.Collections;

public class FoodTransferVisual : MonoBehaviour {

	Vector3 newPos;

	void Start()
	{
		newPos = transform.position;
	}
	// Update is called once per frame
	void Update ()
	{
		Vector3 p1Pos = GameObject.Find("Player1").transform.position;
		Vector3 p2Pos = GameObject.Find("Player2").transform.position;

		float speed = 1f * Time.deltaTime;

			newPos = p2Pos;


		transform.position = Vector3.Lerp(transform.position, newPos, speed);

	}
}

using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {

	public float acceleration;
	public float turnSpeed;
	public float currSpeed, maxSpeed;
	float horiz, vert;
    Transform myTransform;

	void Start () 
	{   
        myTransform = this.transform;
	}

	void Update () 
	{
        Controls();
	}

    void Controls()
    {
        horiz = Input.GetAxis("Horizontal");
        vert = Input.GetAxis("Vertical");
        Move();
    }

    void Move()
    {
        rigidbody.AddForce(myTransform.up * vert * acceleration * Time.deltaTime);
      //  rigidbody.AddTorque(-horiz * turnSpeed * Time.deltaTime);
    }
}

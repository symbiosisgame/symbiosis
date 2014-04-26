using UnityEngine;
using System.Collections.Generic;

public class Sucker : MonoBehaviour
{
    // enemy
    public float detectorRadius = 2.25f;

    public float suckPull;
    public float suckForce;
    // intergrator
    public Vector3 velocity = Vector3.zero;
    public Vector3 forceAcc = Vector3.zero;
    public float maxSpeed = 0.1f;
    public float mass = 0.1f;
	bool sucking;
	public Transform target;
  
    void Start()
    {
        suckPull = 7f;
        suckForce = 0.8f;
    }

    void Update()
    {
        Vector3 accel = forceAcc / mass;
        velocity = velocity + accel  * Time.deltaTime;
        forceAcc = Vector3.zero;

		if(sucking)
		{
			Suck();
			target.position = target.position + velocity * Time.deltaTime;
		}
    }

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player" || other.tag == "Enemy")
		{
			if(!sucking)
			{
				target = other.transform;
				sucking = true;
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.transform == target)
		{
			if(sucking)
			{
				target = null;
				sucking = false;

				GetComponent<LineRenderer>().SetPosition(0, transform.position); 
				GetComponent<LineRenderer>().SetPosition(1, transform.position);
				velocity = Vector3.zero;
			}
		}
	}


    void Suck()
    {
		Vector3 desired = transform.position - target.position;
        desired = desired * suckForce;
        Vector3 pullVector =  desired - velocity;

		GetComponent<LineRenderer>().SetPosition(0, transform.position);
		GetComponent<LineRenderer>().SetPosition(1, target.position);
		forceAcc+= pullVector;

    }
}

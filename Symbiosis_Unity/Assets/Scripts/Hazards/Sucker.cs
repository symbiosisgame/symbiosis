using UnityEngine;
using System.Collections;

public class Sucker : Entities
{
    // gui
    public Color gizmoColor = Color.blue;
    public bool showGizmos = true;

    // enemy
    public float detectorRadius = 2.25f;

    // common vars
    Vector3 toPlayer1, toPlayer2;

    public float suckForce = 2f;
    // intergrator
    public Vector3 velocity = Vector3.zero;
    public Vector3 forceAcc = Vector3.zero;
    public float maxSpeed = 500f;
    public float mass = 0.1f;
  
    void Start()
    {
        // common start
		base.Start ();
    }

    void Update()
    {
        toPlayer1 = p1Transform.position - transform.position;
        toPlayer2 = p2Transform.position - transform.position;
        Debug.DrawLine(transform.position, p1Transform.position);
        p1Transform.position = p1Transform.position + velocity * Time.deltaTime;
        Vector3 accel = forceAcc / mass;
        velocity = velocity + accel * 12f * Time.deltaTime;
        forceAcc = Vector3.zero;
        // case and switch begins
        // TODO player behaviours on arrival, feeding, attack, scared
        Suck();
        
    }

    void Suck()
    {
        if (toPlayer1.magnitude < detectorRadius)
        {
           forceAcc+= Arrive(transform.position);
            Debug.Log("Suck it!");
        }

        else if (toPlayer1.magnitude > detectorRadius)
        {
            velocity = Vector3.zero;
        }
    }

    Vector3 Arrive(Vector3 target)
    {
        Vector3 desired = target - p1Transform.position;
        desired.Normalize();    
        // towards
        return desired - velocity;
    }

    void OnDrawGizmos()
    {
        if (showGizmos)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, detectorRadius);
        }
    }
}

using UnityEngine;
using System.Collections;

public class Sucker : MonoBehaviour
{
    // gui
    public Color gizmoColor = Color.blue;
    public bool showGizmos = true;

    // enemy
    public float detectorRadius = 2.25f;

    // common vars
    Transform p1Transform, p2Transform;
    GameObject player1, player2;
    Vector3 toPlayer1, toPlayer2;

    // roles
    [HideInInspector]
    public Feeder feeder;
    [HideInInspector]
    public Protector protector;

    public float suckPull;
    public float suckForce;
    // intergrator
    public Vector3 velocity = Vector3.zero;
    public Vector3 forceAcc = Vector3.zero;
    public float maxSpeed = 0.1f;
    public float mass = 0.1f;

  
    void Start()
    {
        // common start
        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");
        p1Transform = player1.transform;
        p2Transform = player2.transform;
        suckPull = 7f;
        suckForce = 0.8f;

        // get components
        feeder = player1.GetComponent<Feeder>();
        protector = player2.GetComponent<Protector>(); 
    }

    void Update()
    {
        toPlayer1 = p1Transform.position - transform.position;
        toPlayer2 = p2Transform.position - transform.position;
        //Debug.DrawLine(transform.position, p1Transform.position); // disabled
        p1Transform.position = p1Transform.position + velocity * Time.deltaTime;
        Vector3 accel = forceAcc / mass;
        velocity = velocity + accel  * Time.deltaTime;
        forceAcc = Vector3.zero;

       Suck();
       // Pulling();
    }

    void Suck()
    {

        Vector3 desired = transform.position - p1Transform.position;
        desired = desired * suckForce;
        Vector3 pullVector =  desired - velocity;


        if (toPlayer1.magnitude < detectorRadius)
        {
			GetComponent<LineRenderer>().SetPosition(0, transform.position);
			GetComponent<LineRenderer>().SetPosition(1, p1Transform.position);
            forceAcc+= pullVector;
            Debug.Log("Suck it!");
        }

        else if (toPlayer1.magnitude > detectorRadius)
        {
			GetComponent<LineRenderer>().SetPosition(0, transform.position); 
			GetComponent<LineRenderer>().SetPosition(1, transform.position);
            velocity = Vector3.zero;
        }
    }


 void Pulling()
    {
        Vector3 desired = p2Transform.position - transform.position;
       

        if (desired.magnitude > suckPull)
        {
            Debug.Log("Pulling!");

            forceAcc += Seek(p2Transform.position);
        }
 }
    Vector3 Seek(Vector3 targetPos)
	{
		Vector3 desired = targetPos - p1Transform.position;
		desired *= maxSpeed;
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

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

    public float suckForce = 2f;
    // intergrator
    public Vector3 velocity = Vector3.zero;
    public Vector3 forceAcc = Vector3.zero;
    public float maxSpeed = 500f;
    public float mass = 0.1f;

  
    void Start()
    {
        // common start
        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");
        p1Transform = player1.transform;
        p2Transform = player2.transform;

   

        // get components
        feeder = player1.GetComponent<Feeder>();
        protector = player2.GetComponent<Protector>();

         
         
      

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

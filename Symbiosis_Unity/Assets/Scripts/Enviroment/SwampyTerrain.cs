using UnityEngine;
using System.Collections;

public class SwampyTerrain : MonoBehaviour
{

    public Transform startPoint;
    public GameObject player;

    void Start()
    {
        startPoint = transform.Find("StartPoint");
    }
    void OnTriggerStay(Collider other)
    {
        if (Protector.shielded != true)
        {
            if (other.tag == "Player")
            {
                other.GetComponent<Rigidbody>().drag += 0.1f;

                if (other.GetComponent<Rigidbody>().drag >= 15f)
                {
                    other.gameObject.transform.position = startPoint.transform.position;
                    other.GetComponent<Rigidbody>().drag = 3f;
                }
            }

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {

            other.GetComponent<Rigidbody>().drag = 3f;

        }
    }
}
  

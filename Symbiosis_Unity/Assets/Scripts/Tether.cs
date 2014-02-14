using UnityEngine;
using System.Collections;

public class Tether : MonoBehaviour {

	
	public int NUM_SEGMENTS = 13;
	public float WAVE_AMPLITUDE = 0.13F;
	public float LINE_WIDTH = 0.13F;
	public Color LINE_COLOR1 = Color.green;
	public Color LINE_COLOR2 = Color.red;

	LineRenderer tether_line;
	private Vector3[] segmentPos;
	private Vector3 wave;
	// common vars
	Transform p1Transform, p2Transform; 
	GameObject player1, player2;
	

	// Use this for initialization
	void Start () {

		// common start
		player1 = GameObject.Find("Player1");
		player2 = GameObject.Find("Player2");
		p1Transform = player1.transform;
		p2Transform = player2.transform;

		// tether
		LineRenderer tether_line = gameObject.AddComponent<LineRenderer>();
		tether_line.material = new Material(Shader.Find("Particles/Additive"));
		tether_line.SetColors(LINE_COLOR1, LINE_COLOR2);
		tether_line.SetWidth(LINE_WIDTH, LINE_WIDTH);
		tether_line.SetVertexCount(NUM_SEGMENTS);
		segmentPos = new Vector3[NUM_SEGMENTS];
	}


	// better for physics calculations
	void FixedUpdate () {

		makeTether();

	}
	
	
	void makeTether()
	{		
		LineRenderer tether_line = GetComponent<LineRenderer>();
		Vector3 normalDir = Vector3.Normalize( p1Transform.position - p2Transform.position );	
		var seperation = ( (p2Transform.position - p1Transform.position) / (NUM_SEGMENTS-1));

		// line begins 0
		segmentPos[0] = p1Transform.position;			
		tether_line.SetPosition(0, segmentPos[0]);
	
		for( int i = 1; i < NUM_SEGMENTS; i++ )
		{
			Vector3 pos = transform.position + ( seperation * i );	  					
			Vector3 wave = new Vector3( 0.0F, Mathf.Sin(i + Time.time) * WAVE_AMPLITUDE ,0.0F);
	
			segmentPos[i] = pos + wave;
			tether_line.SetPosition(i, segmentPos[i]);
		}
		// line ends -1
		segmentPos[NUM_SEGMENTS-1] = p2Transform.position;			

	}//end make tether



	
	void LateUpdate()
	{
		// tether renderings usually go in late update after physics are done calculating
	}
	
	
	

	
}// end class


/////////////// junk
////segmentPos[i] = pos;  //old	
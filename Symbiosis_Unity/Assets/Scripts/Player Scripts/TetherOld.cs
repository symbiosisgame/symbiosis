using UnityEngine;
using System.Collections;

// attach script to player

public class TetherOld : MonoBehaviour {

	
	public int NUM_SEGMENTS = 12;			// sensible 12
	public float WAVE_AMPLITUDE = 0.06F;  	// sensible 0.06F
	public float LINE_WIDTH_MAG = 1.6F;  	// sensible 1.6F
	public float TILE_MAG = 6.0F;			// sensible 6.0F
	public Texture TETHER_TEXTURE;
	//public Color LINE_COLOR1 = Color.green;
	//public Color LINE_COLOR2 = Color.red;

	Color LINE_COLOR1, LINE_COLOR2;
	LineRenderer tether_line;

	private Vector3 wave;
	private float LINE_WIDTH = 0.0F;
	private Vector3[] segmentPos;


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
		//Color LINE_COLOR1 = new Color(254.0F, 1.0F, 254.0F, 0.5F);	// pink
		//Color LINE_COLOR2 = new Color(1.0F, 200.0F, 254.0F, 0.5F);	// blue
		//tether_line.SetColors(LINE_COLOR1, LINE_COLOR2);
		tether_line.SetWidth(LINE_WIDTH, LINE_WIDTH);
		tether_line.SetVertexCount(NUM_SEGMENTS);
		segmentPos = new Vector3[NUM_SEGMENTS];
		tether_line.material = new Material(Shader.Find("Mobile/Particles/Alpha Blended"));
		tether_line.material.mainTexture = TETHER_TEXTURE;
		tether_line.material.color = Color.white;
		tether_line.castShadows = false;
		tether_line.receiveShadows = false;
	}


	// better for physics calculations
	void FixedUpdate () {

		doTether();
	}
	
	
	void doTether()
	{		
		LineRenderer tether_line = GetComponent<LineRenderer>();
		//Vector3 normalDir = Vector3.Normalize( p1Transform.position - p2Transform.position );	
		var seperation = ( (p2Transform.position - p1Transform.position) / (NUM_SEGMENTS-1));
		var d = seperation.magnitude;

		LINE_WIDTH = d / LINE_WIDTH_MAG;					// thick further away
		tether_line.SetWidth( LINE_WIDTH, LINE_WIDTH );
		//tether_line.SetWidth( LINE_WIDTH, -LINE_WIDTH );	// 0 cross in middle

		// line begins 0
		segmentPos[0] = p1Transform.position;			
		tether_line.SetPosition(0, segmentPos[0]);
	
		for( int i = 1; i < NUM_SEGMENTS; i++ )
		{
			float w = Mathf.Sin(i + Time.time) * WAVE_AMPLITUDE;

			Vector3 wave = new Vector3( 0, w, 0 );
			Vector3 pos = player1.transform.position + ( seperation * i );
			segmentPos[i] = pos + wave;
			tether_line.SetPosition(i, segmentPos[i]);
		}
		// line ends -1
		segmentPos[NUM_SEGMENTS-1] = p2Transform.position;

		// increase texture tile on distance
		//tether_line.material.mainTextureScale = new Vector2(1, d * TILE_MAG);	// test

		
	}//end make tether



	
	void LateUpdate()
	{
		// tether renderings usually go in late update after physics are done calculating

	}
	
	
	

	
}
// end class

/*
/////////////// junk
		// pingpong
		float t = Mathf.PingPong(Time.time, 2F);
		//color = Color.Lerp(Color.red, Color.green, t);
		LINE_COLOR1.a = Mathf.Lerp(0, 1, t);
		LINE_COLOR2.a = Mathf.Lerp(1, 0, t);
		tether_line.SetColors(LINE_COLOR1, LINE_COLOR2);

float t = Mathf.PingPong(Time.time, 1);
		
*////////////
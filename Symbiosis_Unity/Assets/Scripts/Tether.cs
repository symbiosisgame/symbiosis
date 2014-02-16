using UnityEngine;
using System.Collections;

// attach to player1

public class Tether : MonoBehaviour {

	public bool debug = false;
	public int NUM_SEGMENTS = 6;			// old 12
	public float WAVE_AMPLITUDE = 0.06F;  	// sensible 0.06F
	public float LINE_WIDTH_MAG = 1.6F;  	// sensible 1.6F
	public float TILE_MAG = 6.0F;			// sensible 6.0F
	public Texture TETHER_TEXTURE;			// assign material

	private float LINE_WIDTH = 0.0F;
	private Vector3 wavePos, linePos;		// updated line pos's
	private Vector3[] segmentPos;			// set pos of polygon segments on the renderline
	private Vector3[] linkPos;				// set pos of the links aligned with the segments
	private GameObject[] go_links;			// for tether physics/rigidbody components
	Color LINE_COLOR1, LINE_COLOR2;
	LineRenderer line_renderer;
	Rigidbody rigid_body;
	bool showgizmos = false;				// debug
	bool tethering = false;					// sanity
	SphereCollider sphere_collider;
	CharacterJoint character_joint;
	

	// --------

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

		// --------
		
		// tether
		line_renderer = gameObject.AddComponent<LineRenderer>();
		line_renderer.SetWidth(LINE_WIDTH, LINE_WIDTH);
		line_renderer.SetVertexCount(NUM_SEGMENTS);
		// shader
		line_renderer.material = new Material(Shader.Find("Mobile/Particles/Alpha Blended"));
		line_renderer.material.mainTexture = TETHER_TEXTURE;
		line_renderer.material.color = Color.white;
		line_renderer.castShadows = false;
		line_renderer.receiveShadows = false;
		// arrays
		linkPos = new Vector3[NUM_SEGMENTS];
		segmentPos = new Vector3[NUM_SEGMENTS];
		go_links = new GameObject[NUM_SEGMENTS];

		// init tether components
		for( int k = 0; k < NUM_SEGMENTS; k++ )
		{
			//var num = k + 1;
			go_links[k] = new GameObject( "Tether Link # " + k );
			rigid_body = go_links[k].AddComponent<Rigidbody>();
			sphere_collider = go_links[k].AddComponent<SphereCollider>();
			sphere_collider.radius = 0.03F;
			character_joint = go_links[k].AddComponent<CharacterJoint>();

			// TODO// character_joint.swingAxis

			if(debug)	// toggle for editor
			{

					// linking links
					// first and last anchor links will be repositioned on update, locked player xyz for testing

					if( k == 0 ) // first anchor node
					{	
						character_joint.connectedBody = p1Transform.rigidbody;
						p1Transform.parent = transform;
						//test
						go_links[0].transform.position = segmentPos[0];

					}
					else if( k == NUM_SEGMENTS-1 ) // end anchor node
					{
						character_joint.connectedBody = p2Transform.rigidbody;
						p2Transform.parent = transform;
						//test
						go_links[k].transform.position = segmentPos[k];
					}
					else
					{	// middle nodes
						character_joint.connectedBody = go_links[k-1].rigidbody;
						// test
						// incorrect? //go_links[k].transform.parent = transform;
					}


			}// end enabled

		}//end for

		// debug gizmos
		showgizmos = true;

	}// end start




	// better for physics calculations
	void FixedUpdate () {

	}

	
	void LateUpdate()
	{
		for( int i = 1; i < NUM_SEGMENTS; i++ )
		{
			line_renderer.SetPosition( i, segmentPos[i] );   // rendered last
		}
	}

	
	// update
	void Update () {
		
		doTether();
	}
	
	
	void doTether()
	{		
<<<<<<< HEAD
		LineRenderer line_renderer = GetComponent<LineRenderer>();
		Vector3 normalDir = Vector3.Normalize( p1Transform.position - p2Transform.position );	
		var seperation = ( (p2Transform.position - p1Transform.position) / (NUM_SEGMENTS-1) );
=======
		LineRenderer tether_line = GetComponent<LineRenderer>();
		//Vector3 normalDir = Vector3.Normalize( p1Transform.position - p2Transform.position );	
		var seperation = ( (p2Transform.position - p1Transform.position) / (NUM_SEGMENTS-1));
>>>>>>> f9b59667b0fb64bcccad371bd9ea08a133c1dfb8
		var d = seperation.magnitude;

		// size efx
		LINE_WIDTH = d / LINE_WIDTH_MAG;											// bigger lines test
		line_renderer.SetWidth( LINE_WIDTH, LINE_WIDTH );
		line_renderer.material.mainTextureScale = new Vector2( 1, d * TILE_MAG ); // scale texture test

		// cache positions
		segmentPos[0] = p1Transform.position 		+ new Vector3(0,0,1); // adding a tmp offset for debug and to keep away from players ship
		line_renderer.SetPosition(0, segmentPos[0]);

		segmentPos[NUM_SEGMENTS-1] = p2Transform.position	+ new Vector3(0,0,1);
		line_renderer.SetPosition(NUM_SEGMENTS-1, segmentPos[NUM_SEGMENTS-1]);

		for( int i = 1; i < NUM_SEGMENTS-1; i++ )
		{
			float w = Mathf.Sin(i + Time.time) * WAVE_AMPLITUDE;
			Vector3 wavePos = new Vector3( 0, w, 0 );
			linePos = transform.position + ( seperation * i );
			segmentPos[i] = linePos + wavePos;
			//segmentPos[i] = linePos;
		}

<<<<<<< HEAD
		// test
		for( int j = 0; j < NUM_SEGMENTS; j++ )
		{
			//go_links[j].transform.position = segmentPos[j];
=======
			Vector3 wave = new Vector3( 0, w, 0 );
			Vector3 pos = player1.transform.position + ( seperation * i );
			segmentPos[i] = pos + wave;
			tether_line.SetPosition(i, segmentPos[i]);
>>>>>>> f9b59667b0fb64bcccad371bd9ea08a133c1dfb8
		}

		// test caching link positions
		//go_links[0].transform.position = segmentPos[0];
		//go_links[1].transform.position = segmentPos[1];
		//go_links[2].transform.position = segmentPos[2];
		//go_links[NUM_SEGMENTS-1].transform.position = segmentPos[NUM_SEGMENTS-1];

	}//end make tether




	// visualize joints
	void OnDrawGizmos()
	{
		if(showgizmos)
		{
			// linerender segment gizmos, cyan
			for( int g = 0; g < NUM_SEGMENTS; g++ )
			{
				Gizmos.DrawWireSphere( segmentPos[g], 0.05F);
				Gizmos.color = Color.cyan;
			}

			// link position gizmos, blue
			for( int h = 0; h < NUM_SEGMENTS; h++ )	// needs 1 to -1 count
			{
				Gizmos.DrawWireSphere( go_links[h].transform.position,0.10F);
				Gizmos.color = Color.blue;
			}
		}
	}

	
	
	

	
}// end class
// --------------------------------------------------------------------



/************
 * scratchpad

// test
//go_links[k].transform.position = segmentPos[k];



*/

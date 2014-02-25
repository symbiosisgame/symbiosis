using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{


	// gui
	public Color triggerColor = Color.magenta;
	public bool hideTrigger = true;
	public bool showGizmos = true;

	public GameObject theSpawner;

	public bool canSpawnEnemy = false;
	public bool canSpawnFood = true;


	// food
	//Food food;

	public GameObject foodPrefab;
	public int minFoodAmount = 1;
	public int maxFoodAmount = 3;
	private int spawnedFood = 0;	// counter



	// enemy
	public GameObject enemyPrefab;
	public int minEnemyAmount = 1;
	public int maxEnemyAmount = 6;




	// test1, spawn the food on player trigger
	// test2, spawn an enemy on player trigger 
	// hook up FSM and delegates


	
	void Start()
	{
		// gui
		if( hideTrigger ){ renderer.enabled = false; }else{ renderer.enabled = true; }


	}



	// methods

	private void spawnFood()
	{
		Vector3 randomPos = new Vector3( 	Random.Range(theSpawner.transform.collider.bounds.min.x, theSpawner.transform.collider.bounds.max.x), 
		                                	Random.Range(theSpawner.transform.collider.bounds.min.y, theSpawner.transform.collider.bounds.max.y), 0.0f  );
		GameObject Food = (GameObject) Instantiate(foodPrefab, randomPos, Quaternion.identity);
		spawnedFood++;
	}







	// test 1
	void OnTriggerEnter(Collider other)
	{
		print("entered spawner");


		if (canSpawnFood)
		{
			if( spawnedFood < maxFoodAmount )
			{
				spawnFood();
				Debug.Log ("Spawned " +spawnedFood+ "food");
			}



		}




	}// end ontriggerenter



	// Update is called once per frame
	void Update () {
		
	}




	// gui
	void OnDrawGizmos()
	{
		if( showGizmos )
		{
			Gizmos.color = triggerColor;
			Gizmos.DrawWireCube( transform.position, transform.collider.bounds.extents * 2 );
			if( renderer.enabled ){transform.renderer.sharedMaterial.SetColor("_Emission", triggerColor);}
		}
	}

}// end class



// ---- notes
// GameObject Food = (GameObject) Instantiate(foodPrefab, randomXY + gameObject.transform.position, Quaternion.identity);

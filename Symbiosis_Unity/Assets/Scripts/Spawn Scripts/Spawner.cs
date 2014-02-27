using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Collider))]

public class Spawner : MonoBehaviour
{
	// simple spawner area, spawns prefabs randomly within trigger bounds

	// gui
	public Color triggerColor = Color.magenta;
	public bool hideTrigger = true;
	public bool showGizmos = true;

	// spawner
	public GameObject theSpawner;
	public bool canSpawnEnemy = true;
	public bool canSpawnFood = true;

	// food
	public GameObject foodPrefab;
	public int minFoodAmount = 1;
	public int maxFoodAmount = 3;
	private int foodCount = 0;

	// enemy
	public GameObject enemyPrefab;
	public int minEnemyAmount = 1;
	public int maxEnemyAmount = 6;
	private int enemyCount = 0;


	
	void Start()
	{
		// gui
		if( hideTrigger ){ renderer.enabled = false; }else{ renderer.enabled = true; }
		// trigger
		collider.isTrigger = true;
	}



	// methods
	// -------
	private void spawnFood()
	{
		Vector3 randomPos = new Vector3( 	Random.Range(theSpawner.transform.collider.bounds.min.x, theSpawner.transform.collider.bounds.max.x), 
		                                	Random.Range(theSpawner.transform.collider.bounds.min.y, theSpawner.transform.collider.bounds.max.y), 0.0f  );
		GameObject Food = (GameObject) Instantiate(foodPrefab, randomPos, Quaternion.identity);
		foodCount++;
	}

	private void spawnEnemy()
	{
		Vector3 randomPos = new Vector3( 	Random.Range(theSpawner.transform.collider.bounds.min.x, theSpawner.transform.collider.bounds.max.x), 
		                                	Random.Range(theSpawner.transform.collider.bounds.min.y, theSpawner.transform.collider.bounds.max.y), 0.0f  );
		GameObject Enemy = (GameObject) Instantiate(enemyPrefab, randomPos, Quaternion.identity);
		enemyCount++;
	}



	// trigger
	// -------

	void OnTriggerEnter(Collider other)
	{
		// make food
		if ((canSpawnFood) && (other.name == "Player1"))
		{
			if(foodCount < maxFoodAmount)
			{
				spawnFood();
				Debug.Log("foodCount: " + foodCount);
			}
		}

		// make enemy
		if ((canSpawnEnemy) && (other.name == "Player2"))
		{
			if(enemyCount < maxEnemyAmount)
			{
				spawnEnemy();
				Debug.Log("enemyCount: " + enemyCount);
			}
		}
	}



	// Update is called once per frame
	void Update ()
	{		
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


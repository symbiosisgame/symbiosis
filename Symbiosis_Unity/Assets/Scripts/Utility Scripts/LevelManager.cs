using UnityEngine;
using System.Collections.Generic;
using XboxCtrlrInput;

public class LevelManager : MonoBehaviour {

	//-This class is used for husk cleaning checks and updates a gameobject list with 
	// the gameobjects holding husk logic necessary to complete the husk ie. charge nodes, barrier, spawners
	//-To add new husks to the level, simply create an empty gameobject and name it in husk number order eg. "Husk1", "Husk2" and so on and 
	// child on charge nodes, food/enemy spawners and barrier/s along with the rest of the gameobjects belonging to that husk to keep things tidy.

	//-Lastly, the array size of huskCleanValues needs to match the amount of husks in the level. 
	//-Example: 6 husks in level. In the inspector, I set the array size of huskCleanValues to 6. LevelCleaned starts at 60. Goal is to reach 0.
	// In each element in the array, I set a huskCleaned value for each husk. Element 0 = 50, Element 1 = 40 etc. 
	// The last element in the array should be 0, meaning the level has been fully cleaned and is complete!

	//Any confusion, have a look at LevelManager scene in Scenes folder. You will see the structure there!

	public int[] huskCleanValues; //add a new element for each husk in inspector and assign a value for when husk is cleaned
	public float levelCleaned = 10;
	public float foodSpawnRepeatRate = 6f;
	public float enemySpawnRepeatRate = 5f;
	public float spawnRateMultiplier = 1f;

	public GameObject food;
	public GameObject enemy;
	public Texture pauseScreen;

	private int huskCount = 1;
	private GameObject currHusk;
	private bool invoked = false;
	private float cleanTime = 0f, cleanTimer = 1f, huskCleanRate = 1f;
	private List<GameObject> huskLogic = new List<GameObject>(); //stores all husk logic ie.charge nodes, spawners and barrier
	private List<GameObject> enemySpawners = new List<GameObject>();
	private List<GameObject> foodSpawners = new List<GameObject>();
	private List<GameObject> chargeNodes = new List<GameObject>();

	public bool paused;
	
	void Start () 
	{
		// init pausing
		paused = false;
		Time.timeScale = 1;
		UpdateHuskLogic();
	}
	
	void Update()
	{
		CheckHuskCleaning();
		CheckDocking();
		Pause();
	}

	void Pause()
	{
		if( Input.GetButtonDown("Pause") )	// hotfix
		{
			paused = !paused;

			if(paused)
			{
				Time.timeScale = 0;
			}
			else
			{
				Time.timeScale = 1;
			}
		}
	}

	void CheckHuskCleaning() //checks to see if the overall level health is less than current husk, then moves onto next husk
	{
		if(levelCleaned > 0)
		{
			if(levelCleaned <= huskCleanValues[huskCount-1])
			{
				HuskCleared();
				/*foreach (GameObject chargeNode in chargeNodes) FIGURE THIS OUT!
				{
					GameObject nodeMesh = chargeNode.transform.parent.FindChild("NodeMesh").gameObject;
					nodeMesh.animation.CrossFade("NodeDepleted", 0.5f);
				}*/
			}
		}
		if(levelCleaned <= 0)
		{
			levelCleaned = 0;
		}
	}

	public void HuskCleared() //function is called once a husk is cleared
	{
		foreach (GameObject chargeNode in chargeNodes) 
		{
			GameObject nodeMesh = chargeNode.transform.parent.FindChild("NodeMesh").gameObject;
			nodeMesh.animation.CrossFade("NodeDepleted", 0.5f);
		}
		foreach (GameObject chargeNode in chargeNodes)
		{
			Destroy(chargeNode.gameObject);
		}
		Destroy(currHusk.transform.FindChild("Barrier").gameObject);
		huskCount++;
		huskLogic.Clear();
		foodSpawners.Clear();
		enemySpawners.Clear();
		chargeNodes.Clear();
		UpdateHuskLogic();
		UnDock();
	}

	void UpdateHuskLogic() //Updates the HuskLogic GameObject list with logic from next husk
	{
		currHusk = GameObject.Find ("Husk" +huskCount);
		foreach (Transform child in currHusk.transform)
		{
			if(child.tag == "HuskLogic") //only fills the list with gameobjects with HuskLogic tag (barrier, food/enemy spawners etc, charge nodes)
			{
				huskLogic.Add(child.gameObject); //adds all logic of current husk to an array
			}
			if(child.name.StartsWith("FoodSpawner"))
			{
				foodSpawners.Add(child.gameObject); //adds spawners of current husk to an array
			}
			if(child.name.StartsWith("EnemySpawner"))
			{
				enemySpawners.Add(child.gameObject); //adds spawners of current husk to an array
			}
			if(child.name.StartsWith("ChargeNode")) 
			{
				chargeNodes.Add(child.gameObject.transform.FindChild("ChargeNodeTrigger").gameObject); //adds chargenode triggers of current husk to an array
			}
		}
	}

	void CheckDocking() //check to see if players are docked, then clean husk and spawn food/enemies
	{
		if(Feeder.feederDocked && Protector.protectorDocked)
		{
			foreach (GameObject go in chargeNodes)
			{
				//Animation myAnim = go.transform.FindChild("NodeMesh").GetComponent<Animation>();
				GameObject nodeMesh = go.transform.parent.FindChild("NodeMesh").gameObject;
				nodeMesh.animation["NodeCharge"].speed = 1.55f;	// speed adjustment
				nodeMesh.animation.CrossFade("NodeCharge", 0.5f);
			}
			cleanTime += huskCleanRate * Time.deltaTime;
			if(cleanTime >= cleanTimer)
			{
				audio.Play ();	//TODO safety check
				levelCleaned --;
				cleanTime = 0;
			}
			if(!invoked)
			{
				InvokeRepeating("SpawnFood", foodSpawnRepeatRate, foodSpawnRepeatRate * spawnRateMultiplier); //repeat rate set quite high for testing
				if(huskCount != 1)
				{
					InvokeRepeating("SpawnEnemy", enemySpawnRepeatRate, enemySpawnRepeatRate * spawnRateMultiplier); //repeat rate set quite high for testing
				}
				invoked = true;
			}
		}
		else
		{
			invoked = false;
			CancelInvoke("SpawnFood");
			CancelInvoke("SpawnEnemy");
		}
	}

	public void SpawnFood() //food spawner
	{
		Vector3[] pos = new Vector3[foodSpawners.Count];
		GameObject foodGO = Instantiate(food, foodSpawners[Random.Range(0, foodSpawners.Count)].transform.position, Quaternion.identity) as GameObject;
	}
	public void SpawnEnemy() //enemy spawner
	{
		Vector3[] pos = new Vector3[enemySpawners.Count];
		GameObject enemyGO = Instantiate(enemy, enemySpawners[Random.Range (0, enemySpawners.Count)].transform.position, Quaternion.identity) as GameObject;
	}

	public void UnDock()
	{
		Feeder.feederDocked = false;
		Protector.protectorDocked = false;
		foreach (GameObject go in chargeNodes)
		{
			GameObject nodeMesh = go.transform.parent.FindChild("NodeMesh").gameObject;
			nodeMesh.animation.Stop();
		}
	}

	void OnGUI()
	{
		if(paused)
		{
			GUI.DrawTexture(new Rect(20, 20, Screen.width-40, Screen.height-40), pauseScreen);
		}
	}
}

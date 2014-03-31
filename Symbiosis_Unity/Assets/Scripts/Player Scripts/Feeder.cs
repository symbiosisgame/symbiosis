using UnityEngine;
using System.Collections;

public class Feeder : PlayerManager {

	public float currEatTime, eatTimer;
	public float currTransferTime, transferTimer, transferMod;
	public float eatRate = 1;
	[HideInInspector]public bool feeding;
	[HideInInspector]public bool transferring;
	public static bool feederDocked;
	GameObject foodSource;
	public GameObject foodBall;
	bool createFood;
	Animator myAnim;
	Food food;
	GameObject foodPiece;
	public AudioClip hurt, eat;
	int myHealth = 15;

	new void Start()
	{
		base.Start ();
		currentFood = 0; 
		health = 15;
		maxHealth = 15;
		myAnim = transform.GetChild(0).GetComponent<Animator>();
		healthTextGO = GameObject.Find ("P1HealthText");
		foodTextGO = GameObject.Find ("P1FoodText");
	}
    
	void Update()
	{
		Feed();
		TransferFood();

		//will move later
		healthTextGO.GetComponent<GUIText>().text = health.ToString("0");
		foodTextGO.GetComponent<GUIText>().text = currentFood.ToString("0");
	}

	void Feed()
	{
		if(feeding)
		{
			feederAnim.Play ("FeederFeed");
			//myAnim.SetBool("Feeding", true); //Feeding bool tracked in the animator for transition condition
			if(food.foodStock > 0) //tracks to see if any food is left in Foods stock 
			{
				currEatTime += eatRate * Time.deltaTime;
				if(currEatTime >= eatTimer)
				{
					SoundEffects(eat);
					food.foodStock--;
					AdjustFood(1);
					currEatTime = 0;
				}
			}
			else
			{
				Destroy(foodSource);
				feeding = false;
			}
		}
		else
		{

		}
	}

	public void FoodSource(GameObject whatFood) //method to take gameobject of the food source the feeder is currently munching on, also store Food script component of the food gameobject
	{
		foodSource = whatFood;
		food = foodSource.GetComponent<Food>();
	}

	public void TransferFood() //input is called in PlayerControls
	{
		if(transferring)
		{
			if(this.currentFood > 0)
			{
				float playerDistance = GameObject.Find ("PlayerManager").GetComponent<PlayerControls>().playerDistanceReal();
				
				if(!createFood) //for visual transfer of food
				{
					GameObject foodie = Instantiate(foodBall, transform.position, Quaternion.identity)as GameObject;
					foodPiece = foodie;
					createFood = true;
				}

				Vector3 newPos;
				float speed = 2f * Time.deltaTime;
				newPos = p2Transform.position;
				foodPiece.transform.position = Vector3.Lerp(foodPiece.transform.position, newPos, speed); 
				//Create a proper modifier for distance based transfer
				currTransferTime += Time.deltaTime; //add in modifier for * distance
				if(currTransferTime >= transferTimer)
				{	
					AdjustFood(-1);
					protector.AdjustFood(1);
					createFood = false;
					currTransferTime = 0;
					Destroy (foodPiece.gameObject);
				}
			}
			else
			{
				transferring = false;
			}
		}
		else
		{
			currTransferTime = 0;
		}
	}

	public void SoundEffects(AudioClip clip)
	{
		audio.audio.clip = clip;
		audio.Play ();
	}

	public void AdjustHealth(int adj)
	{
		Debug.Log (health);
		health -= adj;
		SoundEffects(hurt);
		
		if(health >= maxHealth)
		{
			health = maxHealth;
		}
		if(health <= 0)
		{
			Application.LoadLevel(Application.loadedLevelName);
		}
	}
}

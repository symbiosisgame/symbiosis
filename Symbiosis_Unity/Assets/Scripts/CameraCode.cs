using UnityEngine;
using System.Collections;

public class CameraCode : MonoBehaviour {

	GameObject playerManager;
	CameraFollow pManager;
	Transform myTransform;
	public float zoomThres;	
	public float timer;

	void Start()
	{
		playerManager = GameObject.Find ("PlayerManager");
		pManager = playerManager.GetComponent<CameraFollow>();
		myTransform = this.transform;
	}

	void Update()
	{
		myTransform.position = new Vector3(playerManager.transform.position.x, playerManager.transform.position.y, -10f);
	}

	void Zooming()
	{
		if(pManager.playerDistance().magnitude >= zoomThres)
		{
			if(Camera.main.orthographicSize <= 6.5f)
			{
				timer += Time.deltaTime;
				Camera.main.orthographicSize = Mathf.Lerp (5f, 6.5f, timer/1f);
				if(timer >= .99f)
				{
					timer = .99f;
				}
			}
		}
		else
		{
			if(Camera.main.orthographicSize >= 5f)
			{
				timer += Time.deltaTime;
				Camera.main.orthographicSize = Mathf.Lerp (6.5f, 5f, timer/1f);
				if(timer >= 0f)
				{
					timer = 0f;
				}
			}
		}
	}
}

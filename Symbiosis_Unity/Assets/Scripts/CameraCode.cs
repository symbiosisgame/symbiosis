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
		Zooming();
	}

	void Zooming()
	{
		if(pManager.playerDistance() >= zoomThres)
		{
			Camera.main.orthographicSize = pManager.playerDistance();
			if(Camera.main.orthographicSize >= 6.5f)
			{
				Camera.main.orthographicSize = 6.5f;
			}
		}
	}
}

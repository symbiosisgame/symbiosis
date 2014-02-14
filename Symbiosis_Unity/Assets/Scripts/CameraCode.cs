using UnityEngine;
using System.Collections;

public class CameraCode : MonoBehaviour {

	GameObject playerManager;
	PlayerControls pControls;
	Transform myTransform;
	public float zoomThres;	

	void Start()
	{
		playerManager = GameObject.Find ("PlayerManager");
		pControls = playerManager.GetComponent<PlayerControls>();
		myTransform = this.transform;
	}

	void Update()
	{
		Tracking();
		Zooming();
	}

	void Tracking() //tracks distance between players
	{
		myTransform.position = new Vector3(pControls.distanceVector().x, pControls.distanceVector().y, -10f);
	}

	void Zooming() //zoom out for when players move away at a certain distance
	{
		if(pControls.playerDistance() >= zoomThres)
		{
			Camera.main.orthographicSize = pControls.playerDistance();
			if(Camera.main.orthographicSize >= 6.5f)
			{
				Camera.main.orthographicSize = 6.5f;
			}
		}
	}
}

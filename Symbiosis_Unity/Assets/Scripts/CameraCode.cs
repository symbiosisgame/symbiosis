using UnityEngine;
using System.Collections;

public class CameraCode : MonoBehaviour {

	GameObject playerManager;
	PlayerControls pControls;
	Transform myTransform;
	public float zoomThreshold, maxCamSize;	

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
		if(pControls.playerDistance() >= zoomThreshold)
		{
			Camera.main.orthographicSize = pControls.playerDistance()+1f;
			if(Camera.main.orthographicSize >= maxCamSize)
			{
				Camera.main.orthographicSize = maxCamSize;
			}
		}
	}
}

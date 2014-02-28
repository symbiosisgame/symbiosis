using UnityEngine;
using System.Collections;

public class CameraCode : MonoBehaviour {

	GameObject playerManager;
	PlayerControls pControls;
	Transform myTransform;
	public float zoomThreshold, maxCamSize;	
	private Vector3 bottomLock, topLock, leftLock, rightLock;
	public float bottom, top, left, right;

	void Start()
	{
		playerManager = GameObject.Find ("PlayerManager");
		pControls = playerManager.GetComponent<PlayerControls>();
		myTransform = this.transform;
		bottomLock = new Vector3 (transform.position.x, bottom, transform.position.z);
		topLock = new Vector3 (transform.position.x, top, transform.position.z);
		leftLock = new Vector3 (left, transform.position.y, transform.position.z);
		rightLock = new Vector3 (right, transform.position.y, transform.position.z);
	}

	void Update()
	{
		Tracking();
		Zooming();
		LockCamera();
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

	void LockCamera()
	{
		if(transform.position.y <= bottom)
		{

			myTransform.position = bottomLock;
		}
	}
}

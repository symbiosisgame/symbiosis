using UnityEngine;
using System.Collections;

public class Barrier : MonoBehaviour {

	public iTween.EaseType easeType;
	public iTween.LoopType loopType;

	void Start()
	{
		iTween.ScaleBy(gameObject, iTween.Hash("x", 0.92f, "time", 1.2f, "easetype", easeType, "looptype", loopType));
	}


	public void Destroy()
	{
		iTween.ScaleBy(gameObject, iTween.Hash("x", 0f, "time", 1f, "easetype", iTween.EaseType.easeInOutSine));
		Destroy(gameObject, 1f);
	}
}

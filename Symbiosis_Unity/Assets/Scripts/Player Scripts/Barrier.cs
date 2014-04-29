using UnityEngine;
using System.Collections;

public class Barrier : MonoBehaviour {

	public iTween.EaseType easeType;
	public iTween.LoopType loopType;

	void Update()
	{
		iTween.ScaleBy(gameObject, iTween.Hash("x", 0.92f, "time", 1.2f, "easetype", easeType, "looptype", loopType));
	}
}

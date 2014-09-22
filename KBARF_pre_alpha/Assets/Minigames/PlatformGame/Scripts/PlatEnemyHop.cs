using UnityEngine;
using System.Collections;

public class PlatEnemyHop : MonoBehaviour {

	private TwoCommon 		pCommon;

	[SerializeField] private float 		jumpHeight = 2.0f;
	[SerializeField] private float 		fallSpeed = 0.05f;

	void Awake ()
	{
		pCommon 	= GetComponent<TwoCommon> ();
		//GetComponent<Animator>().
	}

	void Update()
	{
		pCommon.YSpeed -= fallSpeed;
		if (pCommon.YSpeed < -2.0f) pCommon.YSpeed = -2.0f;
	}
}
using UnityEngine;
using System.Collections;

public class PlatBounce : MonoBehaviour {

	private PlatCommon 		pCommon;
	private PlatCollision 	pCollisions;

	void Awake ()
	{
		pCommon = GetComponent<PlatCommon> ();
		pCollisions = GetComponent<PlatCollision> ();
	}


	void Update()
	{
		if (pCommon.YSpeed != 0.0f)
		{
			if (pCollisions.IsColDown() || pCollisions.IsColUp())
				pCommon.YSpeed = -pCommon.YSpeed;
		}

		if (pCommon.XSpeed != 0.0f)
		{
			if (pCollisions.IsColLeft() || pCollisions.IsColRight())
				pCommon.XSpeed = -pCommon.XSpeed;
		}
	}
}

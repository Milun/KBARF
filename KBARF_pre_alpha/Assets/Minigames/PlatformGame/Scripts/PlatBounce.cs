using UnityEngine;
using System.Collections;

public class PlatBounce : MonoBehaviour {

	private PlatCommon 		pCommon;
	private PlatCollisions 	pCollisions;

	void Awake ()
	{
		pCommon = GetComponent<PlatCommon> ();
		pCollisions = GetComponent<PlatCollisions> ();
	}

	void Update()
	{
		if (pCollisions.ColSides(pCommon.XSpeed))
		{
			pCommon.XSpeed *= -1.0f;
		}

		if (pCollisions.ColTop(pCommon.YSpeed) || pCollisions.ColBot(pCommon.YSpeed))
		{
			pCommon.YSpeed *= -1.0f;
		}
	}
}

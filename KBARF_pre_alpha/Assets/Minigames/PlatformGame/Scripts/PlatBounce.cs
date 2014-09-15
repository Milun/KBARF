using UnityEngine;
using System.Collections;

public class PlatBounce : MonoBehaviour {

	private TwoCommon 		pCommon;

	void Awake ()
	{
		pCommon = GetComponent<TwoCommon> ();
		//pCollisions = GetComponent<PlatCollision> ();
	}


	void Update()
	{
		if (pCommon.YSpeed != 0.0f)
		{
			//if (pCollisions.IsColDown() || pCollisions.IsColUp())
				//pCommon.YSpeed = -pCommon.YSpeed;
		}

		if (pCommon.XSpeed != 0.0f)
		{
			//if (pCollisions.IsColLeft() || pCollisions.IsColRight())
				//pCommon.XSpeed = -pCommon.XSpeed;
		}
	}
}

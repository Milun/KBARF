using UnityEngine;
using System.Collections;

public class PlatGravity : MonoBehaviour {

	private PlatCommon pCommon;
	private PlatCollision pc;

	[SerializeField] private float gravity = 0.1f;
	[SerializeField] private float ySpeedMax = 1.0f;
	private bool onGround = false;

	private float anchorLength = 1.0f;

	void Awake ()
	{
		pCommon = GetComponent<PlatCommon> ();
		pc = GetComponent<PlatCollision> ();
	}

	private void Update()
	{
		// If there is a collision below, stop.
		/*if (pc.ColBot (-anchorLength))
		{
			pCommon.YSpeed = 0.0f;
			onGround = true;
		
			return;
		}*/

		// Otherwise, fall with gravity.
		pCommon.YSpeed -= gravity;
		onGround = false;
		
		if (pCommon.YSpeed < -ySpeedMax)
		{
			pCommon.YSpeed = -ySpeedMax;
		}
	}

	public bool OnGround
	{
		get
		{
			return onGround;
		}
	}
}

using UnityEngine;
using System.Collections;

public class PlatGravity : MonoBehaviour {

	private PlatCommon pCommon;
	private PlatCollisions pc;

	[SerializeField] private float gravity = 0.1f;
	[SerializeField] private float ySpeedMax = 1.0f;
	private bool onGround = false;

	void Awake ()
	{
		pCommon = GetComponent<PlatCommon> ();
		pc = GetComponent<PlatCollisions> ();
	}

	private void Update()
	{
		// If there is a collision below, stop.
		if (pc.ColBot (pCommon.Vel.y))
		{
			pCommon.YSpeed = 0.0f;
			onGround = true;
			print ("TRUE");
			return;
		}

		// Otherwise, fall with gravity.
		pCommon.YSpeed -= gravity;
		onGround = false;
		
		/*if (mc.YSpeed < -ySpeedMax)
		{
			mc.YSpeed = -ySpeedMax;
		}*/
	}

	public bool OnGround
	{
		get
		{
			return onGround;
		}
	}
}

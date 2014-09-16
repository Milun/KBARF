using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TwoColPhysics))]
public class PlatGravity : MonoBehaviour {

	private TwoCommon 		pCommon;
	private TwoColPhysics 	tColPhys;

	[SerializeField] private float gravity = 0.1f;
	[SerializeField] private float ySpeedMax = 1.0f;

	private bool onGround = false;

	private float anchorLength = 1.0f;

	void Awake ()
	{
		pCommon = GetComponent<TwoCommon> ();
		tColPhys = GetComponent<TwoColPhysics> ();
	}

	void Update()
	{
		if (tColPhys.Move.y > 0.0f && pCommon.YSpeed <= 0.0f )
		{
			pCommon.YSpeed = -1.0f;
			onGround = true;
			
			return;
		}

		print ("noGravity");

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

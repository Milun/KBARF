using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TwoColPhysics))]
public class PlatGravity : MonoBehaviour {

	private TwoCommon 		pCommon;
	private TwoColPhysics 	tColPhys;

	[SerializeField] private float gravity = 0.1f;
	[SerializeField] private float ySpeedMax = 1.0f;

	private int onGround = 0;

	private bool on = true;

	private float anchorLength = 1.0f;

	void Awake ()
	{
		pCommon = GetComponent<TwoCommon> ();
		tColPhys = GetComponent<TwoColPhysics> ();
	}

	void Update()
	{
		if (!on) return;

		if (tColPhys.Move.y > 0.0f && pCommon.YSpeed <= 0.0f )
		{
			pCommon.YSpeed = 0.0f;
			onGround = 2;
			
			return;
		}

		// Otherwise, fall with gravity.
		pCommon.YSpeed -= gravity;
		if (onGround > 0) onGround--;
		
		if (pCommon.YSpeed < -ySpeedMax)
		{
			pCommon.YSpeed = -ySpeedMax;
		}
	}

	public bool On
	{
		set
		{
			on = value;
		}
	}

	public bool MaxSpeed
	{
		get
		{
			return (pCommon.YSpeed <= -ySpeedMax);
		}
	}

	public bool OnGround
	{
		get
		{
			return (onGround > 0);
		}
	}
}

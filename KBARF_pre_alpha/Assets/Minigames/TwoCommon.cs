using UnityEngine;
using System.Collections;

public class TwoCommon : TwoCommonBasic {
	
	// The players position on the screen.
	private Vector2 vel;				// The objects REAL velocity.

	// Use this for initialization
	public override void Awake ()
	{
		base.Awake ();
	}

	public Vector2 Vel
	{
		get
		{
			return vel;
		}
		
		set
		{
			vel = value;
		}
	}

	public float XSpeed
	{
		get
		{
			return vel.x;
		}

		set
		{
			vel = new Vector2(value, vel.y);
		}
	}

	public float YSpeed
	{
		get
		{
			return vel.y;
		}

		set
		{
			vel = new Vector2(vel.x, value);
		}
	}

	public void SnapToGrid()
	{
		transform.localScale = scale;

		// Snap to the fake "pixel grid".
		transform.position = new Vector3 (posOffsetX + Mathf.Round (pos.x/pGlobal.PIXEL_JUMP)
		                                  *pGlobal.PIXEL_JUMP,
		                                  Mathf.Ceil (pos.y/pGlobal.PIXEL_JUMP)
		                                  *pGlobal.PIXEL_JUMP,
		                                  transform.position.z);
	}

	// Update is called once per frame
	void Update ()
	{
		pos += vel;

		// Make everything move at the exact same intervals.
		if (!Frame) return;

		SnapToGrid ();
	}
}

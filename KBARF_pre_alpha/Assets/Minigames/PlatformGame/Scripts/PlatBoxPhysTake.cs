using UnityEngine;
using System.Collections;

public class PlatBoxPhysTake : PlatBox {

	private PlatCommon 			pCommon;
	
	private Vector2 cols = Vector2.zero;

	public enum Behavior
	{
		STOP,
		BOUNCE
	};

	public Behavior behavior;

	// Use this for initialization
	protected override void Awake ()
	{
		base.Awake ();

		pCommon = GetComponent<PlatCommon> ();
	}

	public void Update()
	{
		// Only check collisions if the object is moving.
		// If the object is still, such as a wall, it won't have PlatformCommon at all, so Update() won't execute.
		if (!pCommon) return;

		// Move the bound to be relative to your (real) position.
		UpdateBox ();

		cols = pColManager.CheckPhysCol (this);

		// Don't do anything if this object is a "ghost".
		if (behavior == Behavior.STOP)
		{
			if (IsColLeft() || IsColRight())
			{
				pCommon.XSpeed = 0.0f;
			}

			if (IsColDown() || IsColUp())
			{
				pCommon.YSpeed = 0.0f;
			}
		}
		else if (behavior == Behavior.BOUNCE)
		{
			if (IsColLeft() || IsColRight())
			{
				pCommon.XSpeed = -pCommon.XSpeed;
			}
			
			if (IsColDown() || IsColUp())
			{
				pCommon.YSpeed = -pCommon.YSpeed;
			}


			if (pCommon.XSpeed > 0.0f && pCommon.X + oTR.x > pCommon.PGlobal.ROOM_SIZE.x)
			{
				pCommon.X = pCommon.PGlobal.ROOM_SIZE.x - oTR.x;
				pCommon.XSpeed = -pCommon.XSpeed;
			}
			else if (pCommon.XSpeed < 0.0f && pCommon.X + oBL.x < 0.0f)
			{
				pCommon.X = oBL.x;
				pCommon.XSpeed = -pCommon.XSpeed;
			}
			
			if (pCommon.YSpeed > 0.0f && pCommon.Y + oTR.y > pCommon.PGlobal.ROOM_SIZE.y)
			{
				pCommon.Y = pCommon.PGlobal.ROOM_SIZE.y - oTR.y;
				pCommon.YSpeed = -pCommon.YSpeed;
			}
			else if (pCommon.YSpeed < 0.0f && pCommon.Y + oBL.y < 0.0f)
			{
				pCommon.Y = oBL.y;
				pCommon.YSpeed = -pCommon.YSpeed;
			}
		}
	}

	public bool IsColLeft()
	{
		return (cols.x > 0.0f);
	}

	public bool IsColRight()
	{
		return (cols.x < 0.0f);
	}

	public bool IsColDown()
	{
		return (cols.y > 0.0f);
	}

	public bool IsColUp()
	{
		return (cols.y < 0.0f);
	}

	public PlatCommon PCommon
	{
		get
		{
			return pCommon;
		}
	}
}

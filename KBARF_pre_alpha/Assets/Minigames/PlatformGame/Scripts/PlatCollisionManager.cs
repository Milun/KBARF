using UnityEngine;
using System.Collections.Generic;

public class PlatCollisionManager : MonoBehaviour {
	
	public List<PlatBoxPhysGive> pBoxPhysGive = new List<PlatBoxPhysGive>();

	public Vector2 CheckPhysCol(PlatBoxPhysTake pBoxPhysTake)
	{
		Vector2 op = Vector2.zero;

		foreach (PlatBoxPhysGive e in pBoxPhysGive)
		{
			op += CompareBoxPhys (e, pBoxPhysTake);
		}

		// No collisions.
		return op;
	}

	void Update()
	{
		//foreach (PlatBound e in pBounds)
		//{
		//	e.draw();
		//}
	}

	public void AddPhysCol(PlatBoxPhysGive pass)
	{
		pBoxPhysGive.Add (pass);
	}

	public void DestroyPhysCol(PlatBoxPhysGive pass)
	{
		pBoxPhysGive.Remove (pass);
	}

	// Compares the boxes if they are colliding. No prejudice used.
	private Vector2 CompareBoxNorm(PlatBox pBox, PlatBox pOther)
	{

		return Vector2.zero;
	}

	private Vector2 CompareBoxPhys(PlatBoxPhysGive pGive, PlatBoxPhysTake pTake)
	{
		// WARNING: Some kind of glitch introduced when changed out of pBL and pTR in PlatCollision.
		// Fixed by increasing "safe", but still weird.
		// OH! It was caused by fixing the math error where I subtracted offset twice...
		// Wait... That wouldn't have done anything?
		// Call em hitboxes...? Defensive/offensive different?
		float safe = 2.0f;

		// Check for Y collision.
		if (!pGive.Solid)
		{
			if (
				pTake.PCommon.YSpeed < 0.0f &&
				(pTake.PBL.x - pTake.PCommon.XSpeed + safe < pGive.PTR.x) &&
				(pTake.PTR.x - pTake.PCommon.XSpeed - safe > pGive.PBL.x) &&
				(pTake.PBL.y + pTake.PCommon.YSpeed - 1.0f < pGive.PTR.y) &&
				(pTake.PBL.y + 1.0f			 			   > pGive.PTR.y)
				)
			{
				pTake.PCommon.Y = pGive.PTR.y;

				return -Vector2.up;
			}

			return Vector2.zero;
		}
		else
		{
			// Note: Two squares can only touch on one side.
			// Also, assumes you can't be touched from opposite sides.

			// Check for X collision.
			if (
				pTake.PCommon.XSpeed != 0.0f &&
				(pTake.PBL.x + pTake.PCommon.XSpeed < pGive.PTR.x) &&
				(pTake.PTR.x + pTake.PCommon.XSpeed > pGive.PBL.x) &&
				(pTake.PBL.y - pTake.PCommon.YSpeed + safe < pGive.PTR.y) &&
				(pTake.PTR.y - pTake.PCommon.YSpeed - safe > pGive.PBL.y)
				)
			{
				if (pTake.PCommon.XSpeed < 0.0f)
				{
					pTake.PCommon.X = pGive.PTR.x - pTake.oBL.x;

					return -Vector2.right;
				}
				else if (pTake.PCommon.XSpeed > 0.0f)
				{
					pTake.PCommon.X = pGive.PBL.x - pTake.oTR.x;

					return Vector2.right;
				}
			}

			// Check for Y collision.
			if (
				pTake.PCommon.YSpeed != 0.0f &&
				(pTake.PBL.x - pTake.PCommon.XSpeed + safe < pGive.PTR.x) &&
				(pTake.PTR.x - pTake.PCommon.XSpeed - safe > pGive.PBL.x) &&
				(pTake.PBL.y + pTake.PCommon.YSpeed < pGive.PTR.y) &&
				(pTake.PTR.y + pTake.PCommon.YSpeed > pGive.PBL.y)
				)
			{
				if (pTake.PCommon.YSpeed < 0.0f)
				{
					pTake.PCommon.Y = pGive.PTR.y - pTake.oBL.y;

					return -Vector2.up;
				}
				else if (pTake.PCommon.YSpeed > 0.0f)
				{
					pTake.PCommon.Y = pGive.PBL.y - pTake.oTR.y;

					return Vector2.up;
				}
			}
		}

		return Vector2.zero;
	}
}

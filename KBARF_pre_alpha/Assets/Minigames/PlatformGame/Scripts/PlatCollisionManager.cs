using UnityEngine;
using System.Collections.Generic;

public class PlatCollisionManager : MonoBehaviour {
	
	public List<PlatBound> pBounds = new List<PlatBound>();

	public Vector2 CheckCol(PlatBound pBound, PlatCollision pCollision)
	{
		Vector2 op = Vector2.zero;

		foreach (PlatBound e in pBounds)
		{
			// Ignore your own collision if it's in the list.
			if (pBound.IsEqual(e)) continue;

			op += CompareBB (pBound, e, pCollision);
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

	public void AddCol(PlatBound passBB)
	{
		pBounds.Add (passBB);
	}

	public void DestroyCol(PlatBound passBB)
	{
		pBounds.Remove (passBB);
	}

	private Vector2 CompareBB(PlatBound pBound, PlatBound pOther, PlatCollision pCollision)
	{
		// WARNING: Some kind of glitch introduced when changed out of pBL and pTR in PlatCollision.
		// Fixed by increasing "safe", but still weird.
		// OH! It was caused by fixing the math error where I subtracted offset twice...
		// Wait... That wouldn't have done anything?
		// Call em hitboxes...? Defensive/offensive different?
		float safe = 2.0f;

		// Check for Y collision.
		if (!pOther.solid)
		{
			if (
				pCollision.PCommon.YSpeed < 0.0f &&
				(pBound.pBL.x - pCollision.PCommon.XSpeed + safe < pOther.pTR.x) &&
				(pBound.pTR.x - pCollision.PCommon.XSpeed - safe > pOther.pBL.x) &&
				(pBound.pBL.y + pCollision.PCommon.YSpeed - 1.0f < pOther.pTR.y) &&
				(pBound.pBL.y + 1.0f			 				 > pOther.pTR.y)
				)
			{
				pCollision.PCommon.Y = pOther.pTR.y;

				return Vector2.up;
			}

			return Vector2.zero;
		}
		else
		{
			// Note: Two squares can only touch on one side.
			// Also, assumes you can't be touched from opposite sides.

			// Check for X collision.
			if (
				pCollision.PCommon.XSpeed != 0.0f &&
				(pBound.pBL.x + pCollision.PCommon.XSpeed < pOther.pTR.x) &&
				(pBound.pTR.x + pCollision.PCommon.XSpeed > pOther.pBL.x) &&
				(pBound.pBL.y - pCollision.PCommon.YSpeed + safe < pOther.pTR.y) &&
				(pBound.pTR.y - pCollision.PCommon.YSpeed - safe > pOther.pBL.y)
				)
			{
				if (pCollision.PCommon.XSpeed < 0.0f)
				{
					pCollision.PCommon.X = pOther.pTR.x - pCollision.Offset.x;

					return Vector2.right;
				}
				else if (pCollision.PCommon.XSpeed > 0.0f)
				{
					pCollision.PCommon.X = pOther.pBL.x - pCollision.Offset.x - pCollision.Bounds.x;

					return -Vector2.right;
				}
			}

			// Check for Y collision.
			if (
				pCollision.PCommon.YSpeed != 0.0f &&
				(pBound.pBL.x - pCollision.PCommon.XSpeed + safe < pOther.pTR.x) &&
				(pBound.pTR.x - pCollision.PCommon.XSpeed - safe > pOther.pBL.x) &&
				(pBound.pBL.y + pCollision.PCommon.YSpeed < pOther.pTR.y) &&
				(pBound.pTR.y + pCollision.PCommon.YSpeed > pOther.pBL.y)
				)
			{
				if (pCollision.PCommon.YSpeed < 0.0f)
				{
					pCollision.PCommon.Y = pOther.pTR.y - pCollision.Offset.y;

					return Vector2.up;
				}
				else if (pCollision.PCommon.YSpeed > 0.0f)
				{
					pCollision.PCommon.Y = pOther.pBL.y - pCollision.Offset.y - pCollision.Bounds.y;

					return -Vector2.up;
				}
			}
		}

		return Vector2.zero;
	}
}

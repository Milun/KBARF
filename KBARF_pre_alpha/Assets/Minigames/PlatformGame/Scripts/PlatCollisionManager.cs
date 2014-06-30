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

	private Vector2 CompareBB(PlatBound pBound, PlatBound pOther, PlatCollision pCollision)
	{
		float safe = 1.0f;

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
					pCollision.PCommon.X = pOther.pTR.x;

					return Vector2.right;
				}
				else if (pCollision.PCommon.XSpeed > 0.0f)
				{
					pCollision.PCommon.X = pOther.pBL.x - pCollision.pTR.x - pCollision.pBL.x;

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
					pCollision.PCommon.Y = pOther.pTR.y;

					return Vector2.up;
				}
				else if (pCollision.PCommon.YSpeed > 0.0f)
				{
					pCollision.PCommon.Y = pOther.pBL.y - pCollision.pTR.y - pCollision.pBL.y;

					return -Vector2.up;
				}
			}
		}

		return Vector2.zero;
	}
}

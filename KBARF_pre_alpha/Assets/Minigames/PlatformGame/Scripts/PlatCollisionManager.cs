using UnityEngine;
using System.Collections.Generic;

public class PlatCollisionManager : MonoBehaviour {
	
	public List<PlatBound> pBounds = new List<PlatBound>();

	public bool CheckCol(PlatBound pBound, PlatCollisions pCollision)
	{
		foreach (PlatBound e in pBounds)
		{
			// Ignore your own collision if it's in the list.
			if (pBound.IsEqual(e)) continue;

			if (CompareBB (pBound, e, pCollision)) return true;
		}

		// No collisions.
		return false;
	}

	void Update()
	{
		foreach (PlatBound e in pBounds)
		{
			e.draw();
		}
	}

	public void AddCol(PlatBound passBB)
	{
		pBounds.Add (passBB);
	}

	private bool CompareBB(PlatBound pBound, PlatBound pOther, PlatCollisions pCollision)
	{
		float safe = 1.0f;


		// Check for X collision.
		if (
			(pBound.pBL.x > pOther.pTR.x) ||
			(pBound.pTR.x < pOther.pBL.x) ||
			(pBound.pBL.y + safe > pOther.pTR.y) ||
			(pBound.pTR.y - safe < pOther.pBL.y)
			)
		{
			
		}
		else // Collision!
		{
			if (pCollision.pCommon.XSpeed < 0.0f)
			{
				pCollision.pCommon.X = pOther.pTR.x; 
			}
			else if (pCollision.pCommon.XSpeed > 0.0f)
			{
				pCollision.pCommon.X = pOther.pBL.x - pCollision.pTR.x - pCollision.pBL.x; 
			}
		}

		// Check for Y collision.
		if (
			(pBound.pBL.x + safe > pOther.pTR.x) ||
			(pBound.pTR.x - safe < pOther.pBL.x) ||
			(pBound.pBL.y > pOther.pTR.y) ||
			(pBound.pTR.y < pOther.pBL.y)
			)
		{
			
		}
		else // Collision!
		{
			if (pCollision.pCommon.YSpeed < 0.0f)
			{
				pCollision.pCommon.Y = pOther.pTR.y;
				pCollision.pCommon.YSpeed = 0.0f;
			}
			else if (pCollision.pCommon.YSpeed > 0.0f)
			{
				pCollision.pCommon.Y = pOther.pBL.y - pCollision.pTR.y - pCollision.pBL.y; 
			}
		}



		return true;
	}
}

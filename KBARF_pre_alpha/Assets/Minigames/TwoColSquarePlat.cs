using UnityEngine;
using System.Collections;

[System.Serializable]
public class TwoColSquarePlat : TwoColSquare {

	public enum ColSides {
		ALL,
		UP,
		DOWN,
		LEFT,
		RIGHT
	};

	[SerializeField] private ColSides sides = ColSides.ALL; 

	public ColSides Sides
	{
		get
		{
			return sides;
		}
	}

	public Vector2 CheckColSquarePhys(TwoColSquarePlat other, ref TwoCommon tCommon)
	{
		if (!CheckColBounds(other)) return Vector2.zero;

		float safe = 1.5f;

		// Check for X collision.
		if (other.Sides == ColSides.ALL ||
		    other.Sides == ColSides.LEFT ||
		    other.Sides == ColSides.RIGHT)
		{
			if (
				(BL.x + tCommon.Vel.x < other.TR.x) &&
				(TR.x + tCommon.Vel.x > other.BL.x) &&
				(BL.y - tCommon.Vel.y + safe < other.TR.y) &&
				(TR.y - tCommon.Vel.y - safe > other.BL.y)
				)
			{
				if (Center.x < other.Center.x &&
				    	(other.Sides == ColSides.ALL ||
				 		 other.Sides == ColSides.LEFT)
				    )
				{
					tCommon.X = other.BL.x - localTR.x;
					return -Vector2.right;
				}
				else if (other.Sides == ColSides.ALL ||
				         other.Sides == ColSides.RIGHT)
				{
					tCommon.X = other.TR.x - localBL.x;
					return Vector2.right;
				}
			}
		}

		if (other.Sides == ColSides.ALL ||
		    other.Sides == ColSides.UP ||
		    other.Sides == ColSides.DOWN)
		{
			// Check for Y collision.
			if (
				(BL.x - tCommon.Vel.x + safe < other.TR.x) &&
				(TR.x - tCommon.Vel.x - safe > other.BL.x) &&
				(BL.y + tCommon.Vel.y < other.TR.y) &&
				(TR.y + tCommon.Vel.y > other.BL.y)
				)
			{
				if (Center.y < other.Center.y &&
				    	(other.Sides == ColSides.ALL ||
				 		 other.Sides == ColSides.DOWN)
				    )
				{
					tCommon.Y = other.BL.y - localTR.y;
					return -Vector2.up;
				}
				/* LEAVE IT! It works for the one way platforms! */
				else if (BL.y > other.TR.y - safe + tCommon.YSpeed &&
				         tCommon.YSpeed < 0.0f &&
							(other.Sides == ColSides.ALL ||
				         	 other.Sides == ColSides.UP)
						)
				{
					tCommon.Y = other.TR.y + localBL.y;
					return Vector2.up;
				}
			}
		}

		return Vector2.zero;
	}
}

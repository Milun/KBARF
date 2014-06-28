using UnityEngine;
using System.Collections;

public class PlatBound : MonoBehaviour {

	// REMEMBER! The textures pivot MUST be the bottom-left corner!
	public Vector2 pBL 	= Vector2.zero;
	public Vector2 pTR 	= Vector2.zero;
	
	public bool IsEqual(PlatBound other)
	{
		return (other.pBL == pBL && other.pTR == pTR);
	}

	// Only draws the opposite corners for the moment.
	public void draw()
	{
		Debug.DrawRay (pBL,  Vector2.right 	* 8.0f);
		Debug.DrawRay (pBL,  Vector2.up 	* 8.0f);
		Debug.DrawRay (pTR, -Vector2.right 	* 8.0f);
		Debug.DrawRay (pTR, -Vector2.up 	* 8.0f);
	}
}

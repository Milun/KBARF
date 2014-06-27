using UnityEngine;
using System.Collections;

public class PlatBound : MonoBehaviour {

	public Vector2 p0 = Vector2.zero;	// TOP LEFT
	public Vector2 p1 = Vector2.zero;	// BOTTOM RIGHT
	
	public bool IsEqual(PlatBound myBb)
	{
		return (myBb.p0 == p0 && myBb.p1 == p1);
	}

	public void draw()
	{
		Debug.DrawRay (p0, Vector2.right * 8.0f);
		Debug.DrawRay (p0, -Vector2.up * 8.0f);
		Debug.DrawRay (p1, -Vector2.right * 8.0f);
		Debug.DrawRay (p1, Vector2.up * 8.0f);
	}
}

using UnityEngine;
using System.Collections.Generic;

public class PlatCollisionManager : MonoBehaviour {
	
	public List<PlatBound> bb = new List<PlatBound>();

	public Vector2 CheckCol(PlatBound myBb, Vector2 vel)
	{
		foreach (PlatBound e in bb)
		{
			if (myBb.IsEqual(e)) continue;

			Vector2 op = CompareBB (myBb, e, vel);

			if (op != Vector2.zero) return op;
		}

		return Vector2.zero;
	}

	void Update()
	{
		foreach (PlatBound e in bb)
		{
			e.draw();
		}
	}

	public void AddCol(PlatBound passBB)
	{
		bb.Add (passBB);
	}

	private Vector2 CompareBB(PlatBound bb0, PlatBound bb1, Vector2 vel)
	{
		// Check for no collision.
		if (bb0.p0.x > bb1.p1.x) return Vector2.zero;
		if (bb0.p1.x < bb1.p0.x) return Vector2.zero;
		if (bb0.p0.y < bb1.p1.y) return Vector2.zero;
		if (bb0.p1.y > bb1.p0.y) return Vector2.zero;

		float fx = 0.0f;
		float fy = 0.0f;

		// Move to the location.
		if (vel.x < 0.0f)
		{
			fx = bb1.p0.x; 
		}
		else if (vel.x > 0.0f)
		{
			fx = bb1.p1.x; 
		}

		if (vel.y < 0.0f)
		{
			fy = bb1.p1.y; 
		}
		else if (vel.y > 0.0f)
		{
			fy = bb1.p0.y; 
		}

		return new Vector2 (fx, fy);
	}
}

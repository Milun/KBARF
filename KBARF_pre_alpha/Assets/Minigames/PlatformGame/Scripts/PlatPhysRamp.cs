using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(TwoColLine))]
[RequireComponent(typeof(TwoCommon))]
public class PlatPhysRamp : MonoBehaviour {

	private TwoColLine		tColLine;
	private TwoCommon		tCommon;
	private TwoColPhysics	tColPhys;
	private PlatGravity		pGrav;

	private bool onRamp 	= false;
	private bool ignoreCol 	= false;
	private bool forceRamp  = false;

	private float anchorLength = 7.0f;

	private Vector2 anchor = Vector2.zero;

	// Use this for initialization
	void Awake () {
		tColLine = GetComponent<TwoColLine> ();
		tCommon = GetComponent<TwoCommon> ();

		tColPhys = GetComponent<TwoColPhysics> ();
		pGrav = GetComponent<PlatGravity> ();
	}
	
	// Update is called once per frame
	void Update () {

		// If you're moving upwards, or if implicitly told to ignore the ramp.
		if (tCommon.YSpeed > 0.0f || ignoreCol)
		{
			ignoreCol = false;
			onRamp = false;
			anchor = Vector2.up * -1.0f;
			return;
		}

		tColLine.P2 = anchor;

		List<TwoColManager.Col> other = tColLine.ColManager.CheckCol (tColLine);
		Vector2 col = Vector2.zero;

		foreach (TwoColManager.Col e in other)
		{
			// If there is a collision
			if (e.move != Vector2.zero)
			{
				// And it's a collision with a line.
				if (e.col.GetType() == typeof(TwoColLine))
				{
					// If we're in the air, or if we're ABOVE the line, go on it.
					if (forceRamp || !pGrav || !pGrav.OnGround ||
						    (
								((TwoColLine)e.col).P2.y <= tCommon.Y &&
								((TwoColLine)e.col).P1.y <= tCommon.Y
							)
					   )
					{
						if (col.magnitude < e.move.magnitude) col = e.move;
					}
				}
			}
		}

		// Apply the transforms.
		if (col != Vector2.zero)
		{
			float y = col.y + anchor.y;
			anchor = Vector2.up * -anchorLength;
			
			tCommon.Y += y;
			tCommon.YSpeed = 0.0f;
			
			onRamp = true;
			
			if (tColPhys) tColPhys.DontUpdate();
		}
		else if (pGrav && pGrav.OnGround)
		{
			anchor = Vector2.up * -anchorLength;
		}
		else
		{
			onRamp = false;
			
			anchor = Vector2.up * -1.0f;
		}

		forceRamp = false;
	}

	public void IgnoreCol()
	{
		ignoreCol = true;
	}

	public void ForceRamp()
	{
		forceRamp = true;
	}

	public bool OnRamp
	{
		get
		{
			return onRamp;
		}
	}
}

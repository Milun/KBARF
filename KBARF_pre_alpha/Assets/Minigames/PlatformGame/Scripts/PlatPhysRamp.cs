using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TwoColLine))]
[RequireComponent(typeof(TwoCommon))]
public class PlatPhysRamp : MonoBehaviour {

	private TwoColLine		tColLine;
	private TwoCommon		tCommon;
	private TwoColPhysics	tColPhys;
	private PlatGravity		pGrav;

	private bool onRamp 	= false;
	private bool ignoreCol 	= false;

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
			return;
		}

		Vector2 col = tColLine.ColManager.CheckColMove (tColLine, TwoCol.ColType.T3_DEF);

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

		tColLine.P2 = anchor;
	}

	public void IgnoreCol()
	{
		ignoreCol = true;
	}

	public bool OnRamp
	{
		get
		{
			return onRamp;
		}
	}
}

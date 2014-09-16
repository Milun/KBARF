using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TwoColLine))]
[RequireComponent(typeof(TwoCommon))]
public class PlatPhysRamp : MonoBehaviour {

	private TwoColLine		tColLine;
	private TwoCommon		tCommon;
	private TwoColPhysics	tColPhys;
	private PlatGravity		pGrav;

	private bool onRamp = false;
	private bool ignore = false;

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

		if (tCommon.YSpeed > 0.0f || ignore || (pGrav && pGrav.OnGround))
		{
			ignore = false;
			onRamp = false;
			return;
		}

		Vector2 stairCol = tColLine.ColManager.CheckColMove (tColLine, TwoCol.ColType.T3_DEF);

		if (stairCol != Vector2.zero)
		{
			float y = stairCol.y + anchor.y;
			anchor = Vector2.up * -3.0f;

			tCommon.Y += y;
			tCommon.YSpeed = 0.0f;

			onRamp = true;

			if (tColPhys) tColPhys.DontUpdate();
		}
		else
		{
			onRamp = false;

			anchor = Vector2.up * -1.0f;
		}

		tColLine.P2 = anchor;
	}

	public void Ignore()
	{
		ignore = true;
	}

	public bool OnRamp
	{
		get
		{
			return onRamp;
		}
	}
}

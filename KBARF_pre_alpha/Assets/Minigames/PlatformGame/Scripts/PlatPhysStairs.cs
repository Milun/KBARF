using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TwoColPhysics))]
public class PlatPhysStairs : MonoBehaviour {

	private TwoCommon 		pCommon;
	private TwoColLine 		tColLine;

	private float anchorLength = 8.0f;

	void Awake ()
	{
		pCommon 	= GetComponent<TwoCommon> ();
		tColLine 	= GetComponent<TwoColLine> ();
	}

	void Update()
	{
		tColLine.ColManager.CheckColMove (tColLine, TwoCol.ColType.T3_DEF);
	}
}

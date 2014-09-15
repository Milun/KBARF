using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(TwoColSquare))]
public class TwoColPhysics : MonoBehaviour {

	// Note: Allows only for a single collider to be physics.
	private TwoCommon tCommon;
	private TwoColSquare tColSquare;
	private TwoColLine[]   tColLine;

	private Vector2	  move = Vector2.zero;

	private enum ReactType {
		STOP,
		BOUNCE,
		IGNORE
	};

	[SerializeField] private ReactType reactType = ReactType.STOP;

	void Awake ()
	{
		TwoColSquare[] list = GetComponents<TwoColSquare> ();

		foreach (TwoColSquare e in list)
		{
			if (e.HasType(TwoCol.ColType.PHYSICS_OFF))
			{
				tColSquare = e;
				break;
			}
		}

		tCommon = GetComponent<TwoCommon> ();
		tColLine = GetComponents<TwoColLine> ();
	}

	public Vector2 Move
	{
		get
		{
			return move;
		}
	}

	void Update()
	{
		// Check all physics collisions (the ones most likely to make you move).
		move = tColSquare.ColManager.CheckColPhys (tColSquare, ref tCommon);

		foreach(TwoColLine e in tColLine)
		{
			tCommon.Pos += e.ColManager.CheckColMove (e, TwoCol.ColType.T3_DEF);
			move += e.ColManager.CheckColMove (e, TwoCol.ColType.T3_DEF);
		}

		if (move != Vector2.zero && reactType != ReactType.IGNORE)
		{
			if (tCommon)
			{
				if (
					(
						(move.x < 0.0f && tCommon.XSpeed > 0.0f) ||
						(move.x > 0.0f && tCommon.XSpeed < 0.0f)
					)
				   )
				{
					if 		(reactType == ReactType.BOUNCE) tCommon.XSpeed = -tCommon.XSpeed;
					else if (reactType == ReactType.STOP) 	tCommon.XSpeed = 0.0f;
				}

				if (
					(
						(move.y < 0.0f && tCommon.YSpeed > 0.0f) ||
						(move.y > 0.0f && tCommon.YSpeed < 0.0f)
					)
				   )
				{
					if 		(reactType == ReactType.BOUNCE) tCommon.YSpeed = -tCommon.YSpeed;
					else if (reactType == ReactType.STOP) 	tCommon.YSpeed = 0.0f;
				}
			}
		}
	}
}

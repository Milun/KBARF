using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(TwoColSquare))]
public class TwoColPhysics : MonoBehaviour {

	// Note: Allows only for a single collider to be physics.
	private TwoCommon tCommon;
	private TwoColSquare tColSquare;
	
	private Vector2	move = Vector2.zero;
	private bool	noUpdate = false;

	[SerializeField] bool noStopX = false;
	[SerializeField] bool noStopY = false;

	void Awake ()
	{
		tColSquare = GetComponent<TwoColSquare> ();

		tCommon = GetComponent<TwoCommon> ();
	}

	public Vector2 Move
	{
		get
		{
			return move;
		}
	}

	public void DontUpdate()
	{
		noUpdate = true;
	}

	void Update()
	{
		if (noUpdate)
		{
			noUpdate = false;
			return;
		}

		// Check all physics collisions (the ones most likely to make you move).
		move = tColSquare.ColManager.CheckColPhys (tColSquare, ref tCommon);

		if (move != Vector2.zero)
		{

			if (tCommon)
			{
				if (!noStopX &&
					(
						(move.x < 0.0f && tCommon.XSpeed > 0.0f) ||
						(move.x > 0.0f && tCommon.XSpeed < 0.0f)
					)
				   )
				{
					tCommon.XSpeed = 0.0f;
				}

				if (!noStopY &&
					(
						(move.y < 0.0f && tCommon.YSpeed > 0.0f) ||
						(move.y > 0.0f && tCommon.YSpeed < 0.0f)
					)
				   )
				{
					tCommon.YSpeed = 0.0f;
				}
			}
		}
	}
}

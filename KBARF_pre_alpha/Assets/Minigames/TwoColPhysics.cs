using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(TwoColSquare))]
public class TwoColPhysics : MonoBehaviour {

	// Note: Allows only for a single collider to be physics.
	private TwoCommon tCommon;
	private TwoColSquare tColSquare;

	private Vector2	move = Vector2.zero;
	private bool	noUpdate = false;

	private enum ReactType {
		STOP,
		BOUNCE,
		IGNORE
	};

	[SerializeField] private ReactType reactType = ReactType.STOP;

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
		if (noUpdate || reactType == ReactType.IGNORE)
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

﻿using UnityEngine;
using System.Collections;

public class PlatPhys: MonoBehaviour {

	TwoCol tCol;
	TwoCommon tCommon;

	private enum ReactType {
		STOP,
		BOUNCE,
		IGNORE
	};

	[SerializeField] private ReactType reactType = ReactType.BOUNCE;

	// Use this for initialization
	void Awake () {
		tCol = GetComponent<TwoCol> ();
		tCommon = GetComponent<TwoCommon> ();
	}
	
	// Update is called once per frame
	void Update () {

		Vector2 move = tCol.ColManager.CheckColMove (tCol, TwoCol.ColType.PHYSICS_DEF);
		print (move.y);

		if (move != Vector2.zero)
		{
			if (reactType == ReactType.BOUNCE)
			{
				if ((move.x < 0.0f && tCommon.XSpeed > 0.0f) ||
				    (move.x > 0.0f && tCommon.XSpeed < 0.0f))
					tCommon.XSpeed = -tCommon.XSpeed;
				if ((move.y < 0.0f && tCommon.YSpeed > 0.0f) ||
				    (move.y > 0.0f && tCommon.YSpeed < 0.0f))
					tCommon.YSpeed = -tCommon.YSpeed;
			}
			else if (reactType == ReactType.STOP)
			{
				tCommon.Pos += move;

				if (move.x != 0.0f) tCommon.XSpeed = 0.0f;
				if (move.y != 0.0f) tCommon.YSpeed = 0.0f;
			}
		}
	}
}

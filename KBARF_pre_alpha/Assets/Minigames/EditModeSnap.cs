﻿using UnityEngine;
using System.Collections;


[ExecuteInEditMode]
public class EditModeSnap : MonoBehaviour {
	public float snapValue = 0.08f;
	public float depth = 0.0f;

	void Update() {

		#if (UNITY_EDITOR)
		float snapInverse = 1/snapValue;
		
		float x, y, z;
		
		// if snapValue = .5, x = 1.45 -> snapInverse = 2 -> x*2 => 2.90 -> round 2.90 => 3 -> 3/2 => 1.5
		// so 1.45 to nearest .5 is 1.5
		x = Mathf.Round(transform.position.x * snapInverse)/snapInverse;
		y = Mathf.Round(transform.position.y * snapInverse)/snapInverse;
		z = depth; // depth from camera
		
		transform.position = new Vector3(x, y, z);
		#endif
	}
}
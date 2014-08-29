using UnityEngine;
using System.Collections;


[ExecuteInEditMode]
public class EditModeSnap : MonoBehaviour {
	public float snapValueX = 1f;
	public float snapValueY = 1f;
	public float SnapValueScale = 1f;
	public float depth = 0.0f;

	void Update() {

		if (Time.timeSinceLevelLoad > 0.0f) return;

		#if (UNITY_EDITOR)
		//

		float snapInverseX = 1/snapValueX;
		float snapInverseY = 1/snapValueY;
		
		float x, y, z;
		float xs, ys, zs;
		
		// if snapValue = .5, x = 1.45 -> snapInverse = 2 -> x*2 => 2.90 -> round 2.90 => 3 -> 3/2 => 1.5
		// so 1.45 to nearest .5 is 1.5
		x = Mathf.Round(transform.position.x * snapInverseX)/snapInverseX;
		y = Mathf.Round(transform.position.y * snapInverseY)/snapInverseY;
		z = depth; // depth from camera

		xs = (Mathf.Round(transform.localScale.x / SnapValueScale)) * SnapValueScale;
		ys = (Mathf.Round(transform.localScale.y / SnapValueScale)) * SnapValueScale;
		zs = 1.0f;

		transform.position = new Vector3(x, y, z);
		transform.localScale = new Vector3(xs, ys, zs);
		#endif
	}
}

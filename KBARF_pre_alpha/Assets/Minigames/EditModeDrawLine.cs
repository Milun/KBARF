using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class EditModeDrawLine : MonoBehaviour {
	public Vector2 P1 = Vector2.zero;
	public Vector2 P2 = Vector2.one;
	public Color color = Color.blue;

	#if (UNITY_EDITOR)
	void Update() {

		if (!StatMini.DEBUG)
						return;

		Debug.DrawRay(transform.position + new Vector3(P1.x, P1.y, 0.0f), P2 - P1, color);

	}
	#endif
}

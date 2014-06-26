using UnityEngine;
using System.Collections;


[ExecuteInEditMode]
public class EditModeDrawBounds : MonoBehaviour {
	public Vector2 bounds = Vector2.zero;
	public Color color = Color.blue;

	#if (UNITY_EDITOR)
	void Update() {

		if (!StatMini.DEBUG)
						return;

		Debug.DrawRay(transform.position, Vector3.right * bounds.x, color);
		Debug.DrawRay(transform.position + Vector3.down * bounds.y, Vector3.right * bounds.x, color);
		Debug.DrawRay(transform.position, Vector3.down * bounds.y, color);
		Debug.DrawRay(transform.position + Vector3.right * bounds.x, Vector3.down * bounds.y, color);

	}
	#endif
}

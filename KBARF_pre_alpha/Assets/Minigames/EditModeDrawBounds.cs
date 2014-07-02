using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class EditModeDrawBounds : MonoBehaviour {
	public Vector2 offset = Vector2.zero;
	public Vector2 bounds = Vector2.zero;
	public Color color = Color.blue;

	#if (UNITY_EDITOR)
	void Update() {

		if (!StatMini.DEBUG)
						return;

		Debug.DrawRay(transform.position + new Vector3(offset.x, offset.y, 0.0f), Vector3.right * bounds.x, color);
		Debug.DrawRay(transform.position + new Vector3(offset.x, offset.y, 0.0f) + Vector3.up * bounds.y, Vector3.right * bounds.x, color);
		Debug.DrawRay(transform.position + new Vector3(offset.x, offset.y, 0.0f), Vector3.up * bounds.y, color);
		Debug.DrawRay(transform.position + new Vector3(offset.x, offset.y, 0.0f) + Vector3.right * bounds.x, Vector3.up * bounds.y, color);

	}
	#endif
}

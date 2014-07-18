using UnityEditor;

[CustomEditor(typeof(PlatCollision))]
public class EditorDrawPlatCollision : Editor {
	

	public void OnSceneGUI () {

		PlatCollision myTarget = (PlatCollision) target;

		Handles.color = UnityEngine.Color.green;

		float offsetX = 0.0f;

		if (myTarget.GetComponent<PlatCommon>())
			offsetX = myTarget.GetComponent<PlatCommon> ().PosOffsetX;

		UnityEngine.Vector3 p0 = new UnityEngine.Vector3 (myTarget.Offset.x - offsetX, myTarget.Offset.y);
		UnityEngine.Vector3 p1 = new UnityEngine.Vector3 (myTarget.Offset.x - offsetX, myTarget.Offset.y + myTarget.Bounds.y);
		UnityEngine.Vector3 p2 = new UnityEngine.Vector3 (myTarget.Offset.x - offsetX + myTarget.Bounds.x, myTarget.Offset.y);
		UnityEngine.Vector3 p3 = new UnityEngine.Vector3 (myTarget.Offset.x - offsetX + myTarget.Bounds.x, myTarget.Offset.y + myTarget.Bounds.y);

		Handles.DrawLine(
			myTarget.transform.position + p0,
			myTarget.transform.position + p1);

		Handles.DrawLine(
			myTarget.transform.position + p0,
			myTarget.transform.position + p2);

		Handles.DrawLine(
			myTarget.transform.position + p3,
			myTarget.transform.position + p1);

		Handles.DrawLine(
			myTarget.transform.position + p3,
			myTarget.transform.position + p2);

	}
}
using UnityEditor;

[CustomEditor(typeof(PlatBoxPhysGive))]
public class EditorDrawPlatCollision : Editor {
	

	public void OnSceneGUI () {

		PlatBoxPhysGive myTarget = (PlatBoxPhysGive) target;

		Handles.color = UnityEngine.Color.green;

		float offsetX = 0.0f;

		if (myTarget.GetComponent<PlatCommon>())
			offsetX = myTarget.GetComponent<PlatCommon> ().PosOffsetX;

		UnityEngine.Vector3 p0 = new UnityEngine.Vector3 (myTarget.oBL.x - offsetX, myTarget.oBL.y);
		UnityEngine.Vector3 p1 = new UnityEngine.Vector3 (myTarget.oBL.x - offsetX, myTarget.oTR.y);
		UnityEngine.Vector3 p2 = new UnityEngine.Vector3 (myTarget.oTR.x - offsetX, myTarget.oBL.y);
		UnityEngine.Vector3 p3 = new UnityEngine.Vector3 (myTarget.oTR.x - offsetX, myTarget.oTR.y);

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
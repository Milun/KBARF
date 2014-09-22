using UnityEditor;

[CustomEditor(typeof(PlatBox), true)]
public class EditorDrawPlatBox : Editor {
	

	public void OnSceneGUI () {

		PlatBox myTarget = (PlatBox) target;

		if (myTarget.GetType() == typeof(PlatBoxCombGive)) Handles.color = UnityEngine.Color.red;
		if (myTarget.GetType() == typeof(PlatBoxCombTake)) Handles.color = UnityEngine.Color.green;
		if (myTarget.GetType() == typeof(PlatBoxPhysGive)) Handles.color = UnityEngine.Color.white;
		if (myTarget.GetType() == typeof(PlatBoxPhysTake)) Handles.color = UnityEngine.Color.yellow;

		float offsetX = 0.0f;

		UnityEngine.Vector3 p0 = new UnityEngine.Vector3 (myTarget.oBL.x + 0.1f, myTarget.oBL.y + 0.1f);
		UnityEngine.Vector3 p1 = new UnityEngine.Vector3 (myTarget.oBL.x + 0.1f, myTarget.oTR.y - 0.1f);
		UnityEngine.Vector3 p2 = new UnityEngine.Vector3 (myTarget.oTR.x - 0.1f, myTarget.oBL.y + 0.1f);
		UnityEngine.Vector3 p3 = new UnityEngine.Vector3 (myTarget.oTR.x - 0.1f, myTarget.oTR.y - 0.1f);

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
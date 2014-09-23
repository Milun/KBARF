using UnityEditor;

[CustomEditor(typeof(TwoFlip), true)]
public class EditorDrawFlip : Editor {
	

	public void OnSceneGUI () {

		TwoFlip myTarget = (TwoFlip) target;

		if (myTarget.FlipXManual || (myTarget.GetComponent<PlatMoveNorm>() && myTarget.GetComponent<PlatMoveNorm>().StartMove.x < 0.0f))
		{
			myTarget.transform.localScale = new UnityEngine.Vector3 (-1.0f, 1.0f, 1.0f);
		}
		else
		{
			myTarget.transform.localScale = new UnityEngine.Vector3 (1.0f, 1.0f, 1.0f);
		}
	}
}
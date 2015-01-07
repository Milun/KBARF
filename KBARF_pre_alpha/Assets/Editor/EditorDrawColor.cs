using UnityEditor;

[CustomEditor(typeof(PlatColor), true)]
public class EditorDrawColor : Editor {

	public void OnSceneGUI () {

		PlatColor myTarget = (PlatColor) target;

		if (myTarget)
		{
			myTarget.GetComponent<UnityEngine.SpriteRenderer>().color = myTarget.GetColor(myTarget.SprColor);
		}
	}
}
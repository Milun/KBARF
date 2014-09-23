using UnityEditor;

[CustomEditor(typeof(TwoCol), true)]
public class EditorDrawCol : Editor {
	

	public void OnSceneGUI () {

		TwoCol myTarget = (TwoCol) target;

		if (myTarget.GetTypes.Length == 0) return;

		if (myTarget.GetTypes.Length > 1)
		{
			Handles.color = UnityEngine.Color.magenta;
		}
		if (myTarget.HasType(TwoCol.ColType.COMBAT_DEF))
		{
			Handles.color = UnityEngine.Color.blue;
		}
		else if (myTarget.HasType(TwoCol.ColType.COMBAT_OFF))
		{
			Handles.color = UnityEngine.Color.red;
		}
		else if (myTarget.HasType(TwoCol.ColType.PHYSICS_DEF))
		{
			Handles.color = UnityEngine.Color.green;
		}
		else if (myTarget.HasType(TwoCol.ColType.PHYSICS_OFF))
		{
			Handles.color = UnityEngine.Color.yellow;
		}
		else if (myTarget.HasType(TwoCol.ColType.T3_DEF))
		{
			Handles.color = UnityEngine.Color.cyan;
		}
		else
		{
			Handles.color = UnityEngine.Color.grey;
		}

		if (myTarget.GetType() == typeof(TwoColCircle))
		{
			TwoColCircle temp = (TwoColCircle)myTarget;

			Handles.DrawWireDisc(temp.Center, UnityEngine.Vector3.forward, temp.Rad); 
		}
		else 
		if (myTarget.GetType() == typeof(TwoColSquare))
		{
			TwoColSquare temp = (TwoColSquare)myTarget;

			Handles.DrawLine(new UnityEngine.Vector3(temp.BL.x, temp.BL.y, 0.0f), new UnityEngine.Vector3(temp.BL.x, temp.TR.y, 0.0f) );
			Handles.DrawLine(new UnityEngine.Vector3(temp.BL.x, temp.BL.y, 0.0f), new UnityEngine.Vector3(temp.TR.x, temp.BL.y, 0.0f) );
			Handles.DrawLine(new UnityEngine.Vector3(temp.TR.x, temp.TR.y, 0.0f), new UnityEngine.Vector3(temp.BL.x, temp.TR.y, 0.0f) );
			Handles.DrawLine(new UnityEngine.Vector3(temp.TR.x, temp.TR.y, 0.0f), new UnityEngine.Vector3(temp.TR.x, temp.BL.y, 0.0f) );
		}
		if (myTarget.GetType() == typeof(TwoColLine))
		{
			TwoColLine temp = (TwoColLine)myTarget;

			Handles.DrawLine(new UnityEngine.Vector3(temp.P1Draw.x, temp.P1Draw.y, 0.0f), new UnityEngine.Vector3(temp.P2Draw.x, temp.P2Draw.y, 0.0f) );
		}
	}
}
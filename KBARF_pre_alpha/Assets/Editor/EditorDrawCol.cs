using UnityEditor;

[CustomEditor(typeof(TwoCol), true)]
public class EditorDrawCol : Editor {
	

	public void OnSceneGUI () {

		TwoCol myTarget = (TwoCol) target;

		if (myTarget.GetType() == typeof(TwoColCircle))
		{
			Handles.color = UnityEngine.Color.green;

			TwoColCircle temp = (TwoColCircle)myTarget;

			Handles.DrawWireDisc(temp.Center, UnityEngine.Vector3.forward, temp.Rad); 
		}
		else 
		if (myTarget.GetType() == typeof(TwoColSquare))
		{
			Handles.color = UnityEngine.Color.green;
			
			TwoColSquare temp = (TwoColSquare)myTarget;

			Handles.DrawLine(new UnityEngine.Vector3(temp.BL.x, temp.BL.y, 0.0f), new UnityEngine.Vector3(temp.BL.x, temp.TR.y, 0.0f) );
			Handles.DrawLine(new UnityEngine.Vector3(temp.BL.x, temp.BL.y, 0.0f), new UnityEngine.Vector3(temp.TR.x, temp.BL.y, 0.0f) );
			Handles.DrawLine(new UnityEngine.Vector3(temp.TR.x, temp.TR.y, 0.0f), new UnityEngine.Vector3(temp.BL.x, temp.TR.y, 0.0f) );
			Handles.DrawLine(new UnityEngine.Vector3(temp.TR.x, temp.TR.y, 0.0f), new UnityEngine.Vector3(temp.TR.x, temp.BL.y, 0.0f) );
		}
		if (myTarget.GetType() == typeof(TwoColLine))
		{
			TwoColLine temp = (TwoColLine)myTarget;

			if (temp.Type == TwoCol.ColType.DEFENCE_GIVE)
			{
				Handles.color = UnityEngine.Color.blue;
			}
			else if (temp.Type == TwoCol.ColType.OFFENSE_TAKE)
			{
				Handles.color = UnityEngine.Color.red;
			}
			else if (temp.Type == TwoCol.ColType.PHYSICS_GIVE)
			{
				Handles.color = UnityEngine.Color.green;
			}
			else if (temp.Type == TwoCol.ColType.PHYSICS_TAKE)
			{
				Handles.color = UnityEngine.Color.yellow;
			}
			else
			{
				Handles.color = UnityEngine.Color.grey;
			}

			Handles.DrawLine(new UnityEngine.Vector3(temp.P1Draw.x, temp.P1Draw.y, 0.0f), new UnityEngine.Vector3(temp.P2Draw.x, temp.P2Draw.y, 0.0f) );
		}
	}
}
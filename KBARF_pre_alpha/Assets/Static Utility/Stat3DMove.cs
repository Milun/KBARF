using UnityEngine;
using System.Collections;

public static class Stat3DMove {
	
	// Returns true if it's moved to the target.
	public static Vector3 MoveToPos(Vector3 passPos, Vector3 pos, float speed)
	{
		// Check if the camera is at the correct position.
		if ((pos - passPos).magnitude <= 0.05f)
		{
			return Vector3.zero;
		}
		
		// Move the camera.
		return (pos - passPos) * speed;
	}

	// Returns true if it's rotated to the target.
	public static Vector3 MoveToRot (Vector3 passEul, Vector3 rot, float speed)
	{
		// Get the angle between where we want to rotate and where we're currently rotated.
		float minAngleX = rot.x - passEul.x;
		float minAngleY = rot.y - passEul.y;
		
		// Check if the camera is at the correct rotation.
		if (Mathf.Abs(minAngleX) <= 0.05f &&
		    Mathf.Abs(minAngleY) <= 0.05f)
		{
			return Vector3.zero;
		}
		
		// Calculate the correct angle to use.
		while (minAngleX < -180.0f) 	minAngleX += 360.0f;
		while (minAngleX > 180)			minAngleX -= 360;
		while (minAngleY < -180.0f) 	minAngleY += 360.0f;
		while (minAngleY > 180)			minAngleY -= 360;
		
		// Rotate the camera.
		return new Vector3 (minAngleX * speed, minAngleY * speed, 0.0f);
	}

	// Returns a vector going in the direction the thing is rotated in.
	public static Vector3 GetVectorForward(Vector3 passEul)
	{
		Vector3 op = new Vector3(Mathf.Sin ( Mathf.Deg2Rad*passEul.y ),
		                         0.0f,
		                         Mathf.Cos ( Mathf.Deg2Rad*passEul.y ) );

		return op;
	}
}

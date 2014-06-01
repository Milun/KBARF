using UnityEngine;
using System.Collections;

public static class Stat3DMove {
	
	// Returns true if it's moved to the target.
	public static Vector3 MoveToPos(Vector3 passPos, Vector3 pos, float speed)
	{
		// Check if the camera is at the correct position.
		if ((pos - passPos).magnitude <= 0.005f)
		{
			return passPos;
		}
		
		// Move the camera.
		return passPos + ((pos - passPos) * speed);
	}

	// Returns true if it's rotated to the target.
	public static Vector3 MoveToRot (Vector3 passEul, Vector3 rot, float speed)
	{
		// Get the angle between where we want to rotate and where we're currently rotated.
		float minAngleX = rot.x - passEul.x;
		float minAngleY = rot.y - passEul.y;
		float minAngleZ = rot.z - passEul.z;
		
		// Check if the camera is at the correct rotation.
		if (Mathf.Abs(minAngleX) <= 0.005f &&
		    Mathf.Abs(minAngleY) <= 0.005f)
		{
			return passEul;
		}
		
		// Calculate the correct angle to use.
		while (minAngleX < -180.0f) 	minAngleX += 360.0f;
		while (minAngleX > 180)			minAngleX -= 360;
		while (minAngleY < -180.0f) 	minAngleY += 360.0f;
		while (minAngleY > 180)			minAngleY -= 360;
		while (minAngleZ < -180.0f) 	minAngleZ += 360.0f;
		while (minAngleZ > 180)			minAngleZ -= 360;
		
		// Rotate the camera.
<<<<<<< HEAD
		return passEul + new Vector3 (minAngleX * speed, minAngleY * speed, minAngleZ * speed);
=======
		return new Vector3 (minAngleX * speed, minAngleY * speed, minAngleZ * speed);
>>>>>>> 8358adb15a706e3cb58e3129ebc5344c6b24e603
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

using UnityEngine;
using System.Collections;

public static class StatMain {

	// Get the animation in the heirarchy
	public static Animation GetAnimation (Transform pass)
	{
		int tries = 5;

		Transform container = pass;
		while (container.GetComponent<Animation>() == null && tries > 0)
		{
			container = container.parent;
			tries--;
		}

		return container.animation;
	}

	public static void MoveToVal(float val, float speed, float max)
	{
	
	}

	public static int BoolToInt(bool val)
	{
		if (val) return 1;
		else 	 return 0;
	}
}

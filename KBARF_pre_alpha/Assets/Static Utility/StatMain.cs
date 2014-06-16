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
}

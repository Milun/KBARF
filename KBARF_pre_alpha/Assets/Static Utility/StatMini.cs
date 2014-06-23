using UnityEngine;
using System.Collections;

public static class StatMini {

	#region STATIC VARIABLES
	public static float PIXEL_SIZE = 0.01f;
	public static bool DEBUG = true;
	#endregion

	// Get the container of the Minigame.
	public static Transform GetMiniContainer (Transform pass)
	{
		Transform container = pass;
		while (container.GetComponent<MiniContainer>() == null)
		{
			container = container.parent;
		}

		return container;
	}
}

using UnityEngine;
using System.Collections;

public static class StatMini {
	
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

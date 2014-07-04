using UnityEngine;
using System.Collections;

public class PlatRoom : MonoBehaviour {

	// REMEMBER! The textures pivot MUST be the bottom-left corner!
	public GameObject roomUp;
	public GameObject roomRight;
	public GameObject roomDown;
	public GameObject roomLeft;

	public PlatRoom MoveRight()
	{
		if (roomRight == null)
		{
			return null;
		}

		GameObject room = (GameObject)GameObject.Instantiate (roomRight);
		room.transform.parent = this.transform.parent;

		return room.GetComponent<PlatRoom>();
	}

	public PlatRoom MoveLeft()
	{
		if (roomLeft == null)
		{
			return null;
		}
		
		GameObject room = (GameObject)GameObject.Instantiate (roomLeft);
		room.transform.parent = this.transform.parent;
		
		return room.GetComponent<PlatRoom>();
	}
}

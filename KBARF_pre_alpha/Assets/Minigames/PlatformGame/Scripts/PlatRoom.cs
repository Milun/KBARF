using UnityEngine;
using System.Collections;

public class PlatRoom : MonoBehaviour {

	// REMEMBER! The textures pivot MUST be the bottom-left corner!
	[SerializeField] private GameObject roomUp;
	[SerializeField] private GameObject roomRight;
	[SerializeField] private GameObject roomDown;
	[SerializeField] private GameObject roomLeft;

	public PlatRoom MoveRoom(GameObject newRoom)
	{
		if (newRoom == null)
		{
			return null;
		}

		GameObject room = (GameObject)GameObject.Instantiate (newRoom);
		room.transform.parent = this.transform.parent;

		return room.GetComponent<PlatRoom>();
	}

	public PlatRoom MoveLeft()
	{
		return MoveRoom(roomLeft);
	}

	public PlatRoom MoveRight()
	{
		return MoveRoom(roomRight);
	}

	public PlatRoom MoveUp()
	{
		return MoveRoom(roomUp);
	}

	public PlatRoom MoveDown()
	{
		return MoveRoom(roomDown);
	}
}

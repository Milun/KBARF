using UnityEngine;
using System.Collections;

public class ShipDoor : ClickObj {

	public bool locked = false;

	// Perform this when the object is clicked.
	public void Action()
	{
		if (!locked)
			StatMain.GetAnimation(transform).Play ("shipdoor_anim_open");
		else
			StatMain.GetAnimation(transform).Play ("shipdoor_anim_locked");
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

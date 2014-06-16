using UnityEngine;
using System.Collections;

public class ShipDoor : ClickObj {

	// Perform this when the object is clicked.
	public void Action()
	{
		StatMain.GetAnimation(transform).Play ("shipdoor_anim_open");
	}

	// Use this for initialization
	void Start () {
		StatMain.GetAnimation (transform).animation ["shipdoor_anim_open"].speed = 0.25f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

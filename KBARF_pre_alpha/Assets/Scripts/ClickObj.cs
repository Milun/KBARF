using UnityEngine;
using System.Collections;

public class ClickObj : MonoBehaviour {

	private bool pressed = false;
	private RaycastHit hit;
	private bool lastPressed = false;

	void Update()
	{
		if (lastPressed)
		{
			lastPressed = false;
			return;
		}

		pressed = false;
	}

	// Perform this when the object is clicked.
	public virtual void PressAction(RaycastHit _hit)
	{
		pressed = true;
		lastPressed = true;
		hit = _hit;
	}

	public bool Pressed
	{
		get
		{
			return pressed;
		}
	}

	public RaycastHit Hit
	{
		get
		{
			return hit;
		}
	}
}

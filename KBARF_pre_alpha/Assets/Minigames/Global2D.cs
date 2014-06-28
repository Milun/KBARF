using UnityEngine;
using System.Collections;

public class Global2D : MonoBehaviour {
	
	[SerializeField] private float 			frameDelay = 0.1f;
	[SerializeField] private float 			pixelJump = 1.0f;
	[SerializeField] private int			layer = 0;

	private float							frameTime = 0.0f;
	private bool 							frameActive = false;

	// Returns true whenever a frame triggers.
	public bool Frame()
	{
		return frameActive;
	}

	// Update is called once per frame
	void Update ()
	{
		if (frameTime < 0.0f)
		{
			frameActive = true;
			frameTime = frameDelay;
		}
		else
		{
			frameActive = false;
		}

		frameTime -= Time.deltaTime;
	}

	public float PIXEL_JUMP
	{
		get
		{
			return pixelJump;
		}
	}

	public int LAYER
	{
		get
		{
			return layer;
		}
	}
}

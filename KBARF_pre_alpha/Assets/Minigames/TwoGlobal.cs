using UnityEngine;
using System.Collections;

public class TwoGlobal : MonoBehaviour {
	
	[SerializeField] private float 			frameDelay = 0.1f;
	[SerializeField] private float 			pixelJump = 1.0f;
	[SerializeField] private int			layer = 0;
	[SerializeField] private Vector2		roomSize = Vector2.one;

	private float							frameTime = 0.0f;
	private bool 							frameActive = false;

	void Awake()
	{
		if (frameDelay == 0.0f)
		{
			frameActive = true;
		}
	}

	// Returns true whenever a frame triggers.
	public bool Frame()
	{
		return frameActive;
	}

	// Update is called once per frame
	void Update ()
	{
		if (frameDelay == 0.0f)
		{
			return;
		}

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

	public Vector2 ROOM_SIZE
	{
		get
		{
			return roomSize;
		}
	}
}

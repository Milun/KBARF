using UnityEngine;
using System.Collections;

public class MiniCommon : MonoBehaviour {

	private float			x = 0.0f;
	private float			y = 0.0f;

	private int				xPixel = 0;
	private int				yPixel = 0;

	private static float 	pixelSize = 0.1f;

	// Use this for initialization
	void Start () {
	
	}

	public void Move(Vector2 v)
	{
		x += v.x;
		y += v.y;

		int xFloor = (int)Mathf.Floor (x);
		int yFloor = (int)Mathf.Floor (y);

		// If we've moved to the next pixel, we need to synch y with x to make
		// diagonals jitter (or "jump") at the same time.
		if (xFloor != xPixel)
		{
			xPixel = xFloor;
			y = (float)yFloor;
		}

		yPixel = yFloor;
	}

	// Update is called once per frame
	void Update ()
	{
		transform.position = new Vector3 (Mathf.Floor (x)*pixelSize,
		                                  Mathf.Floor (y)*pixelSize,
		                                  transform.position.z);
	}
}

using UnityEngine;
using System.Collections;

public class PlatCommon : MonoBehaviour {
	
	// The players position on the screen.
	private LayerMask layerMask;

	private Vector2 pos;				// The objects REAL position.
	private Vector2 vel;				// The objects REAL velocity.

	private Global2D global;

	// Use this for initialization
	void Awake ()
	{
		// Establish a link to global statistics.
		global = StatMini.GetMiniContainer(transform).GetComponent<Global2D> ();

		// Set the layer mask we're using.
		layerMask = 1 << global.LAYER;

		pos = new Vector2(transform.position.x, transform.position.y);
	}

	void Start()
	{

	}

	public LayerMask Layer
	{
		get
		{
			return layerMask;
		}
	}

	public Vector2 Pos
	{
		get
		{
			return pos;
		}

		set
		{
			pos = value;
		}
	}

	public Vector2 Vel
	{
		get
		{
			return vel;
		}
		
		set
		{
			vel = value;
		}
	}

	public float X
	{
		get
		{
			return pos.x;
		}
		
		set
		{
			pos = new Vector2(value, pos.y);
		}
	}

	public float Y
	{
		get
		{
			return pos.y;
		}
		
		set
		{
			pos = new Vector2(pos.x, value);
		}
	}

	public float XSpeed
	{
		get
		{
			return vel.x;
		}

		set
		{
			vel = new Vector2(value, vel.y);
		}
	}

	public float YSpeed
	{
		get
		{
			return vel.y;
		}

		set
		{
			vel = new Vector2(vel.x, value);
		}
	}

	public void SnapToGrid()
	{
		// Snap to the fake "pixel grid".
		transform.position = new Vector3 (Mathf.Round (pos.x/global.PIXEL_JUMP)
		                                  *global.PIXEL_JUMP,
		                                  Mathf.Ceil (pos.y/global.PIXEL_JUMP)
		                                  *global.PIXEL_JUMP,
		                                  transform.position.z);
	}

	// Update is called once per frame
	void Update ()
	{
		pos += vel;

		// Make everything move at the exact same intervals.
		if (!global.Frame()) return;

		SnapToGrid ();
	}
}

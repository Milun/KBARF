using UnityEngine;
using System.Collections;

public class MiniCommon : MonoBehaviour {

	// The players position on the screen.
	public Vector2 pos = Vector2.zero;
	public Vector2 vel = Vector2.zero;

	public Global2D global;
	public MiniInput input;

	// Use this for initialization
	void Awake ()
	{
		// Establish a link to global statistics.
		global = StatMini.GetMiniContainer(transform).GetComponent<Global2D> ();
		input =	 StatMini.GetMiniContainer(transform).GetComponent<MiniInput> ();

		// Be sure our custom (not on the grid) location is recorded at the start.
		pos = new Vector2(transform.position.x/0.01f,
		                  transform.position.y/0.01f);
	}

	void Start()
	{

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

	// Update is called once per frame
	void Update ()
	{
		Move (move);

		// Make everything move at the exact same intervals.
		if (!global.Frame()) return;

		// Snap to the fake "pixel grid".
		transform.position = new Vector3 (Mathf.Floor (pos.x)*global.PIXEL_JUMP*global.PIXEL_SIZE,
		                                  Mathf.Floor (pos.y)*global.PIXEL_JUMP*global.PIXEL_SIZE,
		                                  transform.position.z);
	}
}

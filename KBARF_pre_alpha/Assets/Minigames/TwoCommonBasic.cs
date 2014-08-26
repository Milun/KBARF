using UnityEngine;
using System.Collections;

public class TwoCommonBasic : MonoBehaviour {
	
	// The players position on the screen.
	private LayerMask layerMask;

	protected Vector2 pos;				// The objects REAL position.

	protected float posOffsetX = 0.0f;	// Used in sprite flips.
	protected Vector2 scale = Vector2.one;

	protected TwoGlobal pGlobal;

	// Use this for initialization
	public virtual void Awake ()
	{
		// Establish a link to global statistics.
		pGlobal = GameObject.Find ("MiniGame2").GetComponent<TwoGlobal> ();
		//StatMini.GetMiniContainer(transform).GetComponent<PlatGlobal> ();
		
		// Set the layer mask we're using.
		layerMask = 1 << pGlobal.LAYER;
		
		pos = new Vector2(transform.position.x, transform.position.y);
	}

	public TwoGlobal PGlobal
	{
		get
		{
			return pGlobal;
		}
	}

	public LayerMask Layer
	{
		get
		{
			return layerMask;
		}
	}

	public float PosOffsetX
	{
		get
		{
			return posOffsetX;
		}
		
		set
		{
			posOffsetX = value;
		}
	}

	public Vector2 Scale
	{
		get
		{
			return scale;
		}
		
		set
		{
			scale = value;
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

	public bool Frame
	{
		get
		{
			return pGlobal.Frame();
		}
	}

	// Update is called once per frame
	void Update ()
	{
		// Make everything move at the exact same intervals.
		if (!Frame) return;

		transform.localScale = scale;
	}
}

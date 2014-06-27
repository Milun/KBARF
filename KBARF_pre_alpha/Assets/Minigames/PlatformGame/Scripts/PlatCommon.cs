using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlatCommon : MonoBehaviour {
	
	// The players position on the screen.
	public int layer   = 1;				// The layer that this object is on and should interact with.
	private LayerMask layerMask;

	public Global2D global;
	public MiniInput input;
	public Rigidbody2D rb;

	[SerializeField] private GameObject platMask;
	private GameObject pMask;

	// Use this for initialization
	void Awake ()
	{
		// Establish a link to global statistics.
		global = StatMini.GetMiniContainer(transform).GetComponent<Global2D> ();
		input =	 StatMini.GetMiniContainer(transform).GetComponent<MiniInput> ();

		rb = GetComponent<Rigidbody2D>();

		// Set the layer mask we're using.
		layerMask = 1 << layer;
	}

	void Start()
	{
		// Create your sprite mask. Pass all your sprite info to it.
		pMask = (GameObject) Instantiate(platMask, transform.position, transform.rotation);

		UnityEditorInternal.ComponentUtility.CopyComponent(this.GetComponent<SpriteRenderer>());
		UnityEditorInternal.ComponentUtility.PasteComponentAsNew(pMask);

		Destroy(this.GetComponent<SpriteRenderer>());
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
			return transform.position;
		}

		set
		{
			transform.position = value;
		}
	}

	public Vector2 Vel
	{
		get
		{
			return rb.velocity;
		}
		
		set
		{
			rb.velocity = value;
		}
	}

	public float X
	{
		get
		{
			return transform.position.x;
		}
		
		set
		{
			transform.position = new Vector2(value, transform.position.y);
		}
	}

	public float Y
	{
		get
		{
			return transform.position.y;
		}
		
		set
		{
			transform.position = new Vector2(transform.position.x, value);
		}
	}

	public float XSpeed
	{
		get
		{
			return rb.velocity.x;
		}

		set
		{
			rb.velocity = new Vector2(value, rb.velocity.y);
		}
	}

	public float YSpeed
	{
		get
		{
			return rb.velocity.y;
		}

		set
		{
			rb.velocity = new Vector2(rb.velocity.x, value);
		}
	}

	public void SnapToGrid()
	{
		// Snap to the fake "pixel grid".
		pMask.transform.position = new Vector3 (Mathf.Floor (transform.position.x/global.PIXEL_JUMP)
		                                  		*global.PIXEL_JUMP,
		                                        Mathf.Floor (transform.position.y/global.PIXEL_JUMP)
		                                  		*global.PIXEL_JUMP,
		                                  		transform.position.z);
	}

	// Update is called once per frame
	void Update ()
	{
		// Make everything move at the exact same intervals.
		if (!global.Frame()) return;

		SnapToGrid ();
	}
}

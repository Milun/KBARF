using UnityEngine;
using System.Collections;

public class TwoColLine : TwoCol {

	[SerializeField] private bool scaleable = false;
	[SerializeField] private Vector2 p1 	= new Vector2(-1.0f, -1.0f);
	[SerializeField] private Vector2 p2 	= new Vector2(1.0f, 1.0f);

	private Transform myTransform;

	// Use this for initialization
	public override void Awake () {
		myTransform = transform;

		if (scaleable)
		{
			Vector3 multiply = this.transform.localScale;
			p1 = new Vector2(p1.x * multiply.x, p1.y * multiply.y);
			p2 = new Vector2(p2.x * multiply.x, p2.y * multiply.y);
		}

		base.Awake ();
	}

	public Vector2 P1Draw
	{
		get
		{
			return p1 + (Vector2)transform.position;
		}
	}
	
	public Vector2 P2Draw
	{
		get
		{
			return p2 + (Vector2)transform.position;
		}
	}

	public Vector2 P1
	{
		get
		{
			return p1 + (Vector2)myTransform.position;
		}
	}

	public Vector2 P2
	{
		get
		{
			return p2 + (Vector2)myTransform.position;
		}
	}

	public override Vector2 BL
	{
		get
		{
			float minX = Mathf.Min(P1.x, P2.x);
			float minY = Mathf.Min(P1.y, P2.y);

			bL = new Vector2(minX, minY);
			return bL;
		}
	}

	public override Vector2 TR
	{
		get
		{
			float maxX = Mathf.Max(P1.x, P2.x);
			float maxY = Mathf.Max(P1.y, P2.y);

			tR = new Vector2(maxX, maxY);
			return tR;
		}
	}

	public override Vector2 CheckColCircle(TwoColCircle other)
	{
		if (!CheckColBounds (other)) return Vector2.zero;

		Vector2 lineVector = P2 - P1;
		lineVector.Normalize ();

		Vector2 toCenter = other.Center - P1;

		Debug.DrawRay (P1, toCenter);

		float projection = Vector2.Dot (toCenter, lineVector);

		Vector2 dist = (P1 + projection*lineVector) - other.Center;

		if (projection > 0.0f && projection < (P2 - P1).magnitude && dist.magnitude < other.Rad) return dist - dist.normalized*other.Rad;

		if ((P1 - other.Center).magnitude < other.Rad) return (P1-other.Center) - (P1-other.Center).normalized*other.Rad;
		if ((P2 - other.Center).magnitude < other.Rad) return (P2-other.Center) - (P2-other.Center).normalized*other.Rad;

		return Vector2.zero;
	}

	public override Vector2 CheckColSquare(TwoColSquare other)
	{
		return Vector2.zero;
	}

	public override Vector2 CheckColLine(TwoColLine other)
	{
		return Vector2.zero;
	}


	// Update is called once per frame
	void Update () {
	
	}
}

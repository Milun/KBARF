using UnityEngine;
using System.Collections;

public class TwoColLine : TwoCol {

	[SerializeField] private Vector2 p1 	= new Vector2(-1.0f, -1.0f);
	[SerializeField] private Vector2 p2 	= new Vector2(1.0f, 1.0f);

	private Transform myTransform;

	// Use this for initialization
	public override void Awake () {
		myTransform = transform;

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

	public override bool CheckColCircle(TwoColCircle other)
	{
		if (!CheckColBounds (other)) return false;

		if ((P1 - other.Center).magnitude < other.Rad) return true;
		if ((P2 - other.Center).magnitude < other.Rad) return true;

		Vector2 lineVector = P2 - P1;
		lineVector.Normalize ();

		Vector2 toCenter = other.Center - P1;

		Debug.DrawRay (P1, toCenter);

		float projection = Vector2.Dot (toCenter, lineVector);

		Vector2 dist = (P1 + projection*lineVector) - other.Center;

		if (dist.magnitude < other.Rad) return true;

		return false;
	}

	public override bool CheckColSquare(TwoColSquare other)
	{
		return false;
	}

	public override bool CheckColLine(TwoColLine other)
	{
		return false;
	}


	// Update is called once per frame
	void Update () {
	
	}
}

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

	public Vector2 localP1
	{
		get
		{
			return p1;
		}
	}

	public Vector2 localP2
	{
		get
		{
			return p2;
		}
	}

	public Vector2 P1
	{
		get
		{
			return p1 + (Vector2)myTransform.position;
		}

		set
		{
			p1 = value;
		}
	}

	public Vector2 P2
	{
		get
		{
			return p2 + (Vector2)myTransform.position;
		}

		set
		{
			p2 = value;
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
		if (!CheckColBounds (other)) return Vector2.zero;
		
		Vector2 lineVector = P2 - P1;
		lineVector.Normalize ();
		
		Vector2 toCenter = other.Center - P1;
		
		float Rad = 6.0f;
		
		float projection = Vector2.Dot (toCenter, lineVector);
		
		Vector2 dist = (P1 + projection*lineVector) - other.Center;
		
		if (projection > 0.0f && projection < (P2 - P1).magnitude && dist.magnitude < Rad) return dist - dist.normalized*Rad;
		
		if ((P1 - other.Center).magnitude < Rad) return (P1-other.Center) - (P1-other.Center).normalized*Rad;
		if ((P2 - other.Center).magnitude < Rad) return (P2-other.Center) - (P2-other.Center).normalized*Rad;

		return Vector2.zero;
	}

	public Vector2 CheckColLineParam(Vector2 oP1, Vector2 oP2)
	{	
		float A1 = P2.y - P1.y;
		float B1 = P1.x - P2.x;
		float C1 = A1*P1.x + B1*P1.y;
		
		float A2 = oP2.y - oP1.y;
		float B2 = oP1.x - oP2.x;
		float C2 = A2*oP1.x + B2*oP1.y;
		
		float det = A1 * B2 - A2 * B1;
		
		if (det == 0)
		{
			return Vector2.zero;
		}
		else
		{
			float minX1 = Mathf.Min(P1.x, P2.x);
			float maxX1 = Mathf.Max(P1.x, P2.x);
			
			float minY1 = Mathf.Min(P1.y, P2.y);
			float maxY1 = Mathf.Max(P1.y, P2.y);
			
			Vector2 output = new Vector2((B2*C1 - B1*C2)/det, (A1*C2 - A2*C1)/det);
			
			// This check doesn't work... or does it?!
			if (minX1 <= output.x+0.001f && maxX1 >= output.x-0.001f &&
			    minY1 <= output.y+0.001f && maxY1 >= output.y-0.001f)
			{
				return output - P2;
			}
		}

		return Vector2.zero;
	}

	public override Vector2 CheckColLine(TwoColLine other)
	{
		if (!CheckColBounds (other)) return Vector2.zero;

		return CheckColLineParam (other.P1, other.P2);
	}


	// Update is called once per frame
	void Update () {
	
	}
}

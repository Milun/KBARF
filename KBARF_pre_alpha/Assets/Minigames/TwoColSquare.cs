using UnityEngine;
using System.Collections;

[System.Serializable]
public class TwoColSquare : TwoCol {

	[SerializeField] private Vector2 bottomLeft 	= Vector2.zero;
	[SerializeField] private Vector2 topRight 		= Vector2.zero;
	private Vector2 center			= Vector2.zero;

	public Vector2 Center
	{
		get
		{
			return center + Position;
		}
	}

	public override void Flip()
	{
		float topRightTempX = topRight.x;
		topRight.x = -bottomLeft.x;
		bottomLeft.x = -topRightTempX;

		center = new Vector2 (topRight.x - bottomLeft.x, topRight.y - bottomLeft.y);
	}

	public override Vector2 BL
	{
		get
		{
			bL = new Vector2(Position.x + bottomLeft.x, Position.y + bottomLeft.y);
			return bL;
		}
	}

	public Vector2 localBL
	{
		get
		{
			return bottomLeft;
		}

		set
		{
			bottomLeft = value;
		}
	}

	public Vector2 localTR
	{
		get
		{
			return topRight;
		}
		
		set
		{
			topRight = value;
		}
	}

	public override Vector2 TR
	{
		get
		{
			tR = new Vector2(Position.x + topRight.x, Position.y + topRight.y);
			return tR;
		}
	}

	public override Vector2 CheckColCircle(TwoColCircle other)
	{
		if (CheckColBounds(other))
		{
			print("homestar");

			if (other.Center.x > this.BL.x && other.Center.x < this.TR.x)
			{
				if (other.Center.y > this.Center.y) return new Vector2(0.0f, other.Center.y - other.Rad - this.TR.y);
				if (other.Center.y < this.Center.y) return new Vector2(0.0f, other.Center.y + other.Rad - this.BL.y);
			}
			if (other.Center.y > this.BL.y && other.Center.y < this.TR.y)
			{
				if (other.Center.x > this.Center.x) return new Vector2(other.Center.x - other.Rad - this.TR.x, 0.0f);
				if (other.Center.x < this.Center.x) return new Vector2(other.Center.x + other.Rad - this.BL.x, 0.0f);
			}



			Vector2 dist;
			dist = new Vector2 (BL.x, TR.y) - other.Center;
			if (dist.magnitude < other.Rad) return dist.normalized*(other.Rad-dist.magnitude);

			dist = BL - other.Center;
			if (dist.magnitude < other.Rad) return dist.normalized*(other.Rad-dist.magnitude);

			dist = TR - other.Center;
			if (dist.magnitude < other.Rad) return dist.normalized*(other.Rad-dist.magnitude);

			dist = new Vector2 (TR.x, BL.y) - other.Center;
			if (dist.magnitude < other.Rad) return dist.normalized*(other.Rad-dist.magnitude);
		}

		return Vector2.zero;
	}

	public override Vector2 CheckColSquare(TwoColSquare other)
	{
		if (!CheckColBounds(other)) return Vector2.zero;
		
		float left 		= Mathf.Abs(other.BL.x - TR.x);
		float right 	= Mathf.Abs(other.TR.x - BL.x);
		float down 		= Mathf.Abs(other.BL.y - TR.y);
		float up 		= Mathf.Abs(other.TR.y - BL.y);

		if (left <= right && left <= up && left <= down) 	return new Vector2(-left, 0.0f);
		if (right <= left && right <= up && right <= down) 	return new Vector2(right, 0.0f);
		if (up <= right && up <= left && up <= down) 		return new Vector2(0.0f, up);
		if (down <= right && down <= left && down <= up) 	return new Vector2(0.0f, -down);

		return Vector2.zero;
	}

	public override Vector2 CheckColLine(TwoColLine other)
	{	
		if (!CheckColBounds (other)) return Vector2.zero;
		
		Vector2 output = Vector2.zero;
		
		//output = CheckColLineParam (other.Center, vTR);
		//if (output != Vector2.zero) return new Vector2(0.0f, output.y);
		
		//output = CheckColLineParam (other.Center, vTL);
		//if (output != Vector2.zero) return new Vector2(0.0f, output.y);
		
		//output = CheckColLineParam (other.Center, vBL);
		//if (output != Vector2.zero) return new Vector2(0.0f, output.y);
		
		output = other.CheckColLineParam (TR + Vector2.up*(-4.0f), TR);
		if (output != Vector2.zero) return new Vector2(0.0f, output.y);
		
		return Vector2.zero;
	}

	// Use this for initialization
	public override void Awake () {
		base.Awake ();

		center = new Vector2 (topRight.x - bottomLeft.x, topRight.y - bottomLeft.y);
	}

	// Update is called once per frame
	void Update () {
	
	}
}

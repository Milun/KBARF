using UnityEngine;
using System.Collections;

public class TwoColCircle : TwoCol {

	[SerializeField] private float rad 			= 1.0f;
	[SerializeField] private Vector2 center 	= Vector2.zero;

	public float Rad
	{
		get
		{
			return rad;
		}
	}

	public Vector2 Center
	{
		get
		{
			return center + (Vector2)transform.position;
		}
	}

	public override void Flip()
	{
		center.x = -center.x;
	}

	public override Vector2 BL
	{
		get
		{
			bL = new Vector2(center.x + this.transform.position.x - rad, center.y + this.transform.position.y - rad);
			return bL;
		}
	}

	public override Vector2 TR
	{
		get
		{
			tR = new Vector2(center.x + this.transform.position.x + rad, center.y + this.transform.position.y + rad);
			return tR;
		}
	}

	public override Vector2 CheckColCircle(TwoColCircle other)
	{
		if (!CheckColBounds (other)) return Vector2.zero;

		Vector2 dist = this.Center - other.Center;

		float distMax = this.rad + other.Rad;

		if (dist.magnitude < distMax)
		{
			return dist - dist.normalized*distMax;
		}

		return Vector2.zero;
	}

	public override Vector2 CheckColSquare(TwoColSquare other)
	{
		return -other.CheckColCircle(this);
	}

	public override Vector2 CheckColLine(TwoColLine other)
	{
		return other.CheckColCircle(this);
	}

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
	
	}
}

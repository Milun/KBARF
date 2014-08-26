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

	public override Vector2 BL
	{
		get
		{
			bL = new Vector2(this.transform.position.x - rad, this.transform.position.y - rad);
			return bL;
		}
	}

	public override Vector2 TR
	{
		get
		{
			tR = new Vector2(this.transform.position.x + rad, this.transform.position.y + rad);
			return tR;
		}
	}

	public override bool CheckColCircle(TwoColCircle other)
	{
		if (!CheckColBounds (other)) return false;

		Vector2 dist = this.Center - other.Center;

		if (dist.magnitude < this.rad + other.Rad)
		{
			return true;
		}

		return false;
	}

	public override bool CheckColSquare(TwoColSquare other)
	{
		return other.CheckColCircle(this);
	}

	public override bool CheckColLine(TwoColLine other)
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

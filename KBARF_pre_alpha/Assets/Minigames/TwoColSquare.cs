using UnityEngine;
using System.Collections;

public class TwoColSquare : TwoCol {

	[SerializeField] private Vector2 bottomLeft 	= Vector2.zero;
	[SerializeField] private Vector2 topRight 		= Vector2.zero;

	public override Vector2 BL
	{
		get
		{
			bL = new Vector2(this.transform.position.x + bottomLeft.x, this.transform.position.y + bottomLeft.y);
			return bL;
		}
	}
	
	public override Vector2 TR
	{
		get
		{
			tR = new Vector2(this.transform.position.x + topRight.x, this.transform.position.y + topRight.y);
			return tR;
		}
	}

	public override Vector2 CheckColCircle(TwoColCircle other)
	{
		if (CheckColBounds(other))
		{
			if (other.BL.x > this.BL.x && other.TR.x < this.TR.x)
			{
				if (other.Center.y > this.TR.y) return new Vector2(0.0f, other.Center.y - other.TR.y);
				if (other.Center.y < this.BL.y) return new Vector2(0.0f, other.Center.y - other.BL.y);
			}
			if (other.BL.y > this.BL.y && other.TR.y < this.TR.y)
			{
				if (other.Center.x > this.TR.x) return new Vector2(other.Center.x - other.TR.x, 0.0f);
				if (other.Center.x < this.BL.x) return new Vector2(other.Center.x - other.BL.x, 0.0f);
			}

			if ((new Vector2 (BL.x, TR.y) - other.Center).magnitude < other.Rad) return Vector2.zero;//
			if ((BL - other.Center).magnitude 						< other.Rad) return Vector2.zero;//
			if ((TR - other.Center).magnitude 						< other.Rad) return Vector2.zero;//
			if ((new Vector2 (TR.x, BL.y) - other.Center).magnitude < other.Rad) return Vector2.zero;//
		}

		return Vector2.zero;
	}

	public override Vector2 CheckColSquare(TwoColSquare other)
	{
		return Vector2.zero;//other.CheckColBounds(other);
	}

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
	
	}
}

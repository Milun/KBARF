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

	public override bool CheckColCircle(TwoColCircle other)
	{
		if (CheckColBounds(other))
		{
			if (other.BL.x > this.BL.x && other.TR.x < this.TR.x) return true;
			if (other.BL.y > this.BL.y && other.TR.y < this.TR.y) return true;

			if ((new Vector2 (BL.x, TR.y) - other.Center).magnitude < other.Rad) return true;
			if ((BL - other.Center).magnitude 						< other.Rad) return true;
			if ((TR - other.Center).magnitude 						< other.Rad) return true;
			if ((new Vector2 (TR.x, BL.y) - other.Center).magnitude < other.Rad) return true;
		}

		return false;
	}

	public override bool CheckColSquare(TwoColSquare other)
	{
		return other.CheckColBounds(other);
	}

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
	
	}
}

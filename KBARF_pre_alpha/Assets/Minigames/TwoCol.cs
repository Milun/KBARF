using UnityEngine;
using System.Collections;

public class TwoCol : MonoBehaviour {

	TwoColManager colManager;
	protected Vector2 bL = new Vector2(-1.0f, -1.0f);
	protected Vector2 tR = new Vector2(1.0f, 1.0f);

	// Use this for initialization
	public virtual void Awake ()
	{
		// Ryan says not to put "find" in awake. He may have a point. Fine fine Ill put it in start. fukin noob mate.
		colManager = GameObject.Find ("CollisionManager").GetComponent<TwoColManager> ();

		colManager.AddCol (this);
	}

	public virtual Vector2 BL
	{
		get
		{
			return bL;
		}
	}

	public virtual Vector2 TR
	{
		get
		{
			return tR;
		}
	}

	public TwoColManager ColManager
	{
		get
		{
			return colManager;
		}
	}

	protected bool CheckColBounds(TwoCol other)
	{
		if (BL.x > other.TR.x) return false;
		if (TR.x < other.BL.x) return false;
		if (BL.y > other.TR.y) return false;
		if (TR.y < other.BL.y) return false;

		return true;
	}

	public virtual Vector2 CheckColCircle(TwoColCircle other)
	{
		return Vector2.zero;
	}

	public virtual Vector2 CheckColSquare(TwoColSquare other)
	{
		return Vector2.zero;
	}

	public virtual Vector2 CheckColLine(TwoColLine other)
	{
		return Vector2.zero;
	}

	public Vector2 CheckCol(TwoCol other)
	{
		if (other.GetType() == typeof(TwoColCircle))
		{
			return this.CheckColCircle((TwoColCircle)other);
		}
		else
		if (other.GetType() == typeof(TwoColSquare))
		{
			return this.CheckColSquare((TwoColSquare)other);
		}
		else
		if (other.GetType() == typeof(TwoColLine))
		{
			return this.CheckColLine((TwoColLine)other);
		}

		return Vector2.zero;
	}

	// Update is called once per frame
	void OnDestroy () {
		colManager.RemoveCol (this);
	}
}

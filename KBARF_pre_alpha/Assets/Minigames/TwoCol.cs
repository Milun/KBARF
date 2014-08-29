using UnityEngine;
using System.Collections;

public class TwoCol : MonoBehaviour {

	public enum ColType {
		PHYSICS_GIVE,
		PHYSICS_TAKE,
		OFFENSE_TAKE,
		DEFENCE_GIVE,
		OTHER
	};

	TwoColManager colManager;
	protected Vector2 bL = new Vector2(-1.0f, -1.0f);
	protected Vector2 tR = new Vector2(1.0f, 1.0f);

	[SerializeField] private ColType type;

	// Use this for initialization
	public virtual void Awake ()
	{
		// Ryan says not to put "find" in awake. He may have a point. Fine fine Ill put it in start. fukin noob mate.
		colManager = GameObject.Find ("CollisionManager").GetComponent<TwoColManager> ();

		if (type != ColType.PHYSICS_GIVE && type != ColType.DEFENCE_GIVE) return;

		colManager.AddCol (this);
	}

	public ColType Type 
	{
		get
		{
			return type;
		}
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
		if (!colManager) return;
		colManager.RemoveCol (this);
	}
}

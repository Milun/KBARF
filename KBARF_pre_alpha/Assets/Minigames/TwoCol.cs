using UnityEngine;
using System.Collections;

public class TwoCol : MonoBehaviour {

	public enum ColType {
		PHYSICS_DEF,
		PHYSICS_OFF,
		COMBAT_DEF,
		COMBAT_OFF,
		T1_DEF,
		T1_OFF,
		T2_DEF,
		T2_OFF,
		T3_DEF,
		T3_OFF
	};

	TwoColManager colManager;
	protected Vector2 bL = new Vector2(-1.0f, -1.0f);
	protected Vector2 tR = new Vector2(1.0f, 1.0f);

	[SerializeField] private ColType[] types;

	// Use this for initialization
	public virtual void Awake ()
	{
		// Ryan says not to put "find" in awake. He may have a point. Fine fine Ill put it in start. fukin noob mate.
		colManager = GameObject.Find ("CollisionManager").GetComponent<TwoColManager> ();

		foreach (ColType e in types)
		{
			if (e == ColType.PHYSICS_DEF ||
			    e == ColType.COMBAT_DEF ||
			    e == ColType.T1_DEF ||
			    e == ColType.T2_DEF ||
			    e == ColType.T3_DEF)
			{
				colManager.AddCol (this);
				return;
			}
		}
	}

	public ColType[] GetTypes
	{
		get
		{
			return types;
		}
	}

	public bool HasType(ColType ct)
	{
		foreach (ColType e in types)
		{
			if (e == ct)
			{
				return true;
			}
		}
		return false;
	}

	// Note: There is a more efficient way of doing this.
	public bool CanCollide(TwoCol other)
	{
		if (this.HasType(ColType.COMBAT_DEF) && other.HasType(ColType.COMBAT_OFF)) return true;
		if (this.HasType(ColType.PHYSICS_DEF) && other.HasType(ColType.PHYSICS_OFF)) return true;
		if (this.HasType(ColType.T1_DEF) && other.HasType(ColType.T1_OFF)) return true;
		if (this.HasType(ColType.T2_DEF) && other.HasType(ColType.T2_OFF)) return true;
		if (this.HasType(ColType.T3_DEF) && other.HasType(ColType.T3_OFF)) return true;

		return false;
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

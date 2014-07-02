using UnityEngine;
using System.Collections;

public class PlatCollision : MonoBehaviour {

	private PlatCommon 			pCommon;

	[SerializeField] private Vector2 offset = Vector2.zero;	// Example: [2, 1]. Is the Bottom Left point.
	[SerializeField] private Vector2 bounds = Vector2.zero; // Example: [8, 8]
	[SerializeField] private bool solid = true;

	public enum CollisionBehavior
	{
		STOP,
		BOUNCE,
		IGNORE
	};
	private PlatBound 			pBound;

	[SerializeField] private CollisionBehavior 	colBehavior = CollisionBehavior.STOP;

	private PlatCollisionManager pColManager;	// Reference to the object which handles collisions.
	private Vector2 cols = Vector2.zero;


	// Use this for initialization
	void Awake ()
	{
		pCommon 	= GetComponent<PlatCommon> ();
		pColManager = GameObject.Find("CollisionManager").GetComponent<PlatCollisionManager> ();

		// Set up your initial bounds and send them to the manager for storage.
		pBound = new PlatBound();
		pBound.pBL = offset + new Vector2(transform.position.x, transform.position.y);
		pBound.pTR = offset + bounds + new Vector2(transform.position.x, transform.position.y);
		pBound.solid = solid;

		// If the pBound is never updated again, the manager will think there's always a collision here.
		pColManager.AddCol (pBound);
	}

	public void Update()
	{
		// Only check collisions if the object is moving.
		// If the object is still, such as a wall, it won't have PlatformCommon at all, so Update() won't execute.
		if (!pCommon) return;

		// Move the bound to be relative to your (real) position.
		pBound.pBL = offset + pCommon.Pos;
		pBound.pTR = offset + bounds + pCommon.Pos;

		cols = pColManager.CheckCol (pBound, this);

		// Don't do anything if this object is a "ghost".
		if (colBehavior == CollisionBehavior.IGNORE)
		{
			return;
		}
		else if (colBehavior == CollisionBehavior.STOP)
		{
			if (IsColLeft() || IsColRight())
			{
				pCommon.XSpeed = 0.0f;
			}

			if (IsColDown() || IsColUp())
			{
				pCommon.YSpeed = 0.0f;
			}
		}
		else if (colBehavior == CollisionBehavior.BOUNCE)
		{
			if (IsColLeft() || IsColRight())
			{
				pCommon.XSpeed = -pCommon.XSpeed;
			}
			
			if (IsColDown() || IsColUp())
			{
				pCommon.YSpeed = -pCommon.YSpeed;
			}
		}
	}

	public bool IsColLeft()
	{
		return (cols.x > 0.0f);
	}

	public bool IsColRight()
	{
		return (cols.x < 0.0f);
	}

	public bool IsColDown()
	{
		return (cols.y > 0.0f);
	}

	public bool IsColUp()
	{
		return (cols.y < 0.0f);
	}

	public PlatCommon PCommon
	{
		get
		{
			return pCommon;
		}
	}

	public Vector2 Offset
	{
		get
		{
			return offset;
		}
	}

	public Vector2 Bounds
	{
		get
		{
			return bounds;
		}
	}
}

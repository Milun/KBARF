﻿using UnityEngine;
using System.Collections;

public class PlatCollision : MonoBehaviour {

	public PlatCommon pCommon;

	[SerializeField] private Vector2 offset = Vector2.zero;	// Example: [2, 1]. Is the Bottom Left point.
	[SerializeField] private Vector2 bounds = Vector2.zero; // Example: [8, 8]
	[SerializeField] private bool solid = true;

	public enum CollisionBehavior
	{
		STOP,
		BOUNCE,
		IGNORE
	};

	public CollisionBehavior colBehavior = CollisionBehavior.STOP;

	private PlatBound pBound;
	public Vector2 pBL;		// The original bounds (relative to the object)
	public Vector2 pTR;		// The original bounds (relative to the object)
	private Vector2 cols = Vector2.zero;

	private PlatCollisionManager pColManager;	// Reference to the object which handles collisions.

	// Use this for initialization
	void Awake ()
	{
		pCommon 	= GetComponent<PlatCommon> ();
		pColManager = GameObject.Find("CollisionManager").GetComponent<PlatCollisionManager> ();

		// If this object ever moves, we'll need these.
		pBL = offset;
		pTR = offset + bounds;

		// Set up your initial bounds and send them to the manager for storage.
		pBound = this.gameObject.AddComponent<PlatBound>();
		pBound.pBL = pBL + new Vector2(transform.position.x, transform.position.y);
		pBound.pTR = pTR + new Vector2(transform.position.x, transform.position.y);
		pBound.solid = solid;

		// If the pBound is never updated again, the manager will think there's always a collision here.
		pColManager.AddCol (pBound);
	}

	public void CheckCol()
	{
		// Move the bound to be relative to your (real) position.
		pBound.pBL = pBL + pCommon.Pos;
		pBound.pTR = pTR + pCommon.Pos;

		cols = pColManager.CheckCol (pBound, this);

		if (colBehavior == CollisionBehavior.STOP)
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


}
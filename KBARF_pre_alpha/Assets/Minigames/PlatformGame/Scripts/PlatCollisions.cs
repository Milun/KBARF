﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlatCommon))]
public class PlatCollisions : MonoBehaviour {

	private PlatCommon pCommon;

	[SerializeField] private Vector2 offset = Vector2.zero;	// Example: [2, -1]
	[SerializeField] private Vector2 bounds = Vector2.zero; // Example: [8, 8]

	// Use this for initialization
	void Awake ()
	{
		pCommon = GetComponent<PlatCommon> ();
	}

	public bool ColBot(float dist)
	{
		if (pCommon.YSpeed >= 0.0f)
			return false;
		
		// Give +0.1f to make them not intefere with the side collisions.
		RaycastHit2D hitLeft = Physics2D.Raycast(new Vector3(pCommon.Pos.x + offset.x + 0.5f,
		                                                     pCommon.Pos.y + offset.y - bounds.y + 0.1f,
		                                                     0.0f),
		                                         Vector2.up,
		                                         dist,
		                                         pCommon.Layer);
		if (hitLeft.collider != null)
		{
			pCommon.Y = hitLeft.point.y + bounds.y + offset.y;
			return true;
		}

		RaycastHit2D hitRight = Physics2D.Raycast(new Vector3(pCommon.Pos.x + offset.x + bounds.x - 0.5f,
		                                                      pCommon.Pos.y + offset.y - bounds.y + 0.1f,
		                                                      0.0f),
		                                          Vector2.up,
		                                          dist,
		                                          pCommon.Layer);
		if (hitRight.collider != null)
		{
			pCommon.Y = hitRight.point.y + bounds.y + offset.y;
			return true;
		}

		return false;
	}

	public bool ColTop(float dist)
	{
		if (pCommon.YSpeed <= 0.0f)
			return false;
		
		// Give +0.1f to make them not intefere with the side collisions.
		RaycastHit2D hitLeft = Physics2D.Raycast(new Vector3(pCommon.Pos.x + offset.x + 0.5f,
		                                                     pCommon.Pos.y + offset.y - 0.1f,
		                                                     0.0f),
		                                         Vector2.up,
		                                         dist,
		                                         pCommon.Layer);
		if (hitLeft.collider != null && !hitLeft.collider.tag.Contains("PlatWallPass"))
		{
			pCommon.Y = hitLeft.point.y + offset.y;
			return true;
		}
		
		RaycastHit2D hitRight = Physics2D.Raycast(new Vector3(pCommon.Pos.x + offset.x + bounds.x - 0.5f,
		                                                      pCommon.Pos.y + offset.y - 0.1f,
		                                                      0.0f),
		                                          Vector2.up,
		                                          dist,
		                                          pCommon.Layer);
		if (hitRight.collider != null && !hitRight.collider.tag.Contains("PlatWallPass"))
		{
			pCommon.Y = hitRight.point.y + offset.y;
			return true;
		}
		
		return false;
	}

	// Will only collide left if negative, will only collide right if positive.
	public bool ColSides(float dist)
	{
		if (dist == 0.0f)
			return false;

		float x = 0.0f;

		if (dist < 0.0f)
		{
			x = offset.x;
		}
		else
		{
			x = offset.x + bounds.x;
		}

		RaycastHit2D hitTop = Physics2D.Raycast(new Vector3(pCommon.Pos.x + x,
		                                                    pCommon.Pos.y - offset.y - 0.5f,
		                                                    0.0f),
		                                         Vector2.right * dist,
		                                         dist,
		                                         pCommon.Layer);
		if (hitTop.collider != null && !hitTop.collider.tag.Contains("PlatWallPass"))
		{
			pCommon.X = hitTop.point.x - x;
			print ("VEL-L");
			return true;
		}
		
		RaycastHit2D hitBottom = Physics2D.Raycast(new Vector3(pCommon.Pos.x + x,
		                                                       pCommon.Pos.y + offset.y - bounds.y + 0.5f,
		                                                       0.0f),
		                                          Vector2.right * dist,
		                                          dist,
		                                          pCommon.Layer);
		if (hitBottom.collider != null && !hitBottom.collider.tag.Contains("PlatWallPass"))
		{
			pCommon.X = hitBottom.point.x - x;
			print ("VEL-R");
			return true;
		}
		
		return false;
	}

	// Update is called once per frame
	void Update () {
	
	}
}

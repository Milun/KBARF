using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MiniCommon))]
public class PlatCollisions : MonoBehaviour {

	MiniCommon mc;

	[SerializeField] private Vector2 offset = Vector2.zero;	// Example: [2, -1]
	[SerializeField] private Vector2 bounds = Vector2.zero; // Example: [8, 8]

	// Use this for initialization
	void Awake ()
	{
		mc = GetComponent<MiniCommon> ();
	}

	public bool ColBot(float dist)
	{
		if (mc.YSpeed >= 0.0f)
			return false;
		
		// Give +0.1f to make them not intefere with the side collisions.
		RaycastHit2D hitLeft = Physics2D.Raycast(new Vector3(mc.Pos.x + offset.x + 0.1f,
		                                                     mc.Pos.y + offset.y - bounds.y + 0.1f,
		                                                     0.0f) * StatMini.PIXEL_SIZE,
		                                         Vector2.up,
		                                         dist * StatMini.PIXEL_SIZE,
		                                         mc.Layer);
		if (hitLeft.collider != null)
		{
			mc.Y = hitLeft.point.y/StatMini.PIXEL_SIZE + bounds.y + offset.y;
			return true;
		}

		RaycastHit2D hitRight = Physics2D.Raycast(new Vector3(mc.Pos.x + offset.x + bounds.x - 0.1f,
		                                                      mc.Pos.y + offset.y - bounds.y + 0.1f,
		                                                      0.0f) * StatMini.PIXEL_SIZE,
		                                          Vector2.up,
		                                          dist * StatMini.PIXEL_SIZE,
		                                          mc.Layer);
		if (hitRight.collider != null)
		{
			mc.Y = hitRight.point.y/StatMini.PIXEL_SIZE + bounds.y + offset.y;
			return true;
		}

		return false;
	}

	public bool ColTop(float dist)
	{
		if (mc.YSpeed <= 0.0f)
			return false;
		
		// Give +0.1f to make them not intefere with the side collisions.
		RaycastHit2D hitLeft = Physics2D.Raycast(new Vector3(mc.Pos.x + offset.x + 0.1f,
		                                                     mc.Pos.y + offset.y - 0.1f,
		                                                     0.0f) * StatMini.PIXEL_SIZE,
		                                         Vector2.up,
		                                         dist * StatMini.PIXEL_SIZE,
		                                         mc.Layer);
		if (hitLeft.collider != null && !hitLeft.collider.tag.Contains("PlatWallPass"))
		{
			mc.Y = hitLeft.point.y/StatMini.PIXEL_SIZE + offset.y;
			return true;
		}
		
		RaycastHit2D hitRight = Physics2D.Raycast(new Vector3(mc.Pos.x + offset.x + bounds.x - 0.1f,
		                                                      mc.Pos.y + offset.y - 0.1f,
		                                                      0.0f) * StatMini.PIXEL_SIZE,
		                                          Vector2.up,
		                                          dist * StatMini.PIXEL_SIZE,
		                                          mc.Layer);
		if (hitRight.collider != null && !hitRight.collider.tag.Contains("PlatWallPass"))
		{
			mc.Y = hitRight.point.y/StatMini.PIXEL_SIZE + offset.y;
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

		RaycastHit2D hitTop = Physics2D.Raycast(new Vector3(mc.Pos.x + x,
		                                                    mc.Pos.y - offset.y - 0.1f,
		                                                    0.0f) * StatMini.PIXEL_SIZE,
		                                         Vector2.right * dist,
		                                         dist * StatMini.PIXEL_SIZE,
		                                         mc.Layer);
		if (hitTop.collider != null && !hitTop.collider.tag.Contains("PlatWallPass"))
		{
			mc.X = hitTop.point.x/StatMini.PIXEL_SIZE - x;
			return true;
		}
		
		RaycastHit2D hitBottom = Physics2D.Raycast(new Vector3(mc.Pos.x + x,
		                                                       mc.Pos.y + offset.y - bounds.y + 0.1f,
		                                                       0.0f) * StatMini.PIXEL_SIZE,
		                                          Vector2.right * dist,
		                                          dist * StatMini.PIXEL_SIZE,
		                                          mc.Layer);
		if (hitBottom.collider != null && !hitBottom.collider.tag.Contains("PlatWallPass"))
		{
			mc.X = hitBottom.point.x/StatMini.PIXEL_SIZE - x;
			return true;
		}
		
		return false;
	}

	// Update is called once per frame
	void Update () {
	
	}
}

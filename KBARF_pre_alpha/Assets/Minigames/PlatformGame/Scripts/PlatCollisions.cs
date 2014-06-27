using UnityEngine;
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
		if (pCommon.YSpeed > 0.0f)
			return false;

		// Give +0.1f to make them not intefere with the side collisions.
		RaycastHit2D hitLeft = Physics2D.Raycast(new Vector3(pCommon.Pos.x + offset.x + 0.5f,
		                                                     pCommon.Pos.y + offset.y - bounds.y + 0.5f,
		                                                     0.0f),
		                                         Vector2.up,
		                                         dist,
		                                         pCommon.Layer);
		if (hitLeft.collider != null &&
		    (
				hitLeft.collider.tag == "PlatWallPass" ||
		 	  	hitLeft.collider.tag == "PlatWallSolid"
			)
		   )
		{
			pCommon.Y = hitLeft.point.y + bounds.y + offset.y + 0.1f;
			return true;
		}

		RaycastHit2D hitRight = Physics2D.Raycast(new Vector3(pCommon.Pos.x + offset.x + bounds.x - 0.5f,
		                                                      pCommon.Pos.y + offset.y - bounds.y + 0.5f,
		                                                      0.0f),
		                                          Vector2.up,
		                                          dist,
		                                          pCommon.Layer);
		if (hitRight.collider != null &&
		     (
				hitRight.collider.tag == "PlatWallPass" ||
				hitRight.collider.tag == "PlatWallSolid"
			 )
		    )
		{
			pCommon.Y = hitRight.point.y + bounds.y + offset.y + 0.1f;
			return true;
		}

		return false;
	}
}

using UnityEngine;
using System.Collections;

public class PlatEnemy : MonoBehaviour {

	private PlatCommon 		pCommon;
	private PlatCollision 	pCollision;

	[SerializeField] private float 		animSpeed = 0.1f;

	void Awake ()
	{
		pCommon 	= GetComponent<PlatCommon> ();	
		pCollision 	= GetComponent<PlatCollision> ();	
	}

	// Use this for initialization
	void Start () {

		// Set the speed based on the movement speed.
		if (pCommon)
		{
			// Invert speed vs animation.
			float speed = pCommon.Vel.magnitude;
			if (speed != 0.0f)
				speed = 0.05f/speed;

			GetComponent<Animator>().speed = speed;
		}
		else
		{
			GetComponent<Animator>().speed = animSpeed = 0.1f;
		}
	}
}

[ExecuteInEditMode]
Debug.DrawRay(transform.position + new Vector3(offset.x, offset.y, 0.0f), Vector3.right * bounds.x, color);
Debug.DrawRay(transform.position + new Vector3(offset.x, offset.y, 0.0f) + Vector3.up * bounds.y, Vector3.right * bounds.x, color);
Debug.DrawRay(transform.position + new Vector3(offset.x, offset.y, 0.0f), Vector3.up * bounds.y, color);
Debug.DrawRay(transform.position + new Vector3(offset.x, offset.y, 0.0f) + Vector3.right * bounds.x, Vector3.up * bounds.y, color);

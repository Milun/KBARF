using UnityEngine;
using System.Collections;

public class PlatEnemy : MonoBehaviour {

	private TwoCommon 		pCommon;

	[SerializeField] private float 		animSpeed = 0.1f;

	void Awake ()
	{
		pCommon 	= GetComponent<TwoCommon> ();	
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
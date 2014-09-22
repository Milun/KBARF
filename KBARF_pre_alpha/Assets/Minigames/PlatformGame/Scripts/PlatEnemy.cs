using UnityEngine;
using System.Collections;

public class PlatEnemy : MonoBehaviour {

	private TwoCommon 		pCommon;
	private TwoCol			tCol;

	[SerializeField] private float 		animSpeed = 0.1f;

	void Awake ()
	{
		pCommon 	= GetComponent<TwoCommon> ();
		tCol 		= GetComponent<TwoCol> ();
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

	void Update()
	{
		if (tCol)
		{
			TwoCol col = tCol.ColManager.IsCol(tCol, TwoCol.ColType.COMBAT_DEF);
			
			if (col)
			{
				
				col.GetComponent<PlatHero>().Die();
			}
		}
	}
}
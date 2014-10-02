using UnityEngine;
using System.Collections;

public class PlatEnemy : MonoBehaviour {

	private TwoCommon 		pCommon;
	private TwoCol[]		tCols;

	[SerializeField] private float 		animSpeed = 0.1f;

	void Awake ()
	{
		pCommon 	= GetComponent<TwoCommon> ();
		tCols 		= GetComponents<TwoCol> ();
	}

	// Use this for initialization
	void Start () {

		// Set the speed based on the movement speed.
		Animator myAnimator = GetComponent<Animator> ();

		if (myAnimator)
		{
			if (pCommon)
			{
				// Invert speed vs animation.
				float speed = Mathf.Sqrt(pCommon.Vel.magnitude) * 1.1f;
				/*if (speed != 0.0f)
					speed = 0.05f/speed;*/

				myAnimator.speed = speed;
			}
			else
			{
				myAnimator.speed = animSpeed;
			}
		}
	}

	void Update()
	{
		if (tCols.Length > 0)
		{
			foreach (TwoCol e in tCols)
			{
				TwoCol col = e.ColManager.IsCol(e, TwoCol.ColType.COMBAT_DEF);
				
				if (col)
				{
					
					col.GetComponent<PlatHero>().Die();
				}
			}
		}
	}
}
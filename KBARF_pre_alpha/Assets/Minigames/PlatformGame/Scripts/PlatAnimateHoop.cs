using UnityEngine;
using System.Collections;

public class PlatAnimateHoop : MonoBehaviour {

	private TwoCommon 		pCommon;
	[SerializeField] private Material	mat;

	[SerializeField] private float 		animSpeed = 0.1f;
	private float						finalSpeed = 0.0f;

	private float						countDown = 0.0f;
	private float						offset = 0.0f;

	void Awake ()
	{
		pCommon 	= GetComponent<TwoCommon> ();
	}

	// Use this for initialization
	void Start () {

		// Set the speed based on the movement speed.
		if (mat)
		{
			if (pCommon)
			{
				// Invert speed vs animation.
				finalSpeed = pCommon.Vel.magnitude;
				if (finalSpeed != 0.0f)
					finalSpeed = 0.05f/finalSpeed;

				countDown = finalSpeed;
			}
			else
			{
				finalSpeed = animSpeed;
				countDown = finalSpeed;
			}
		}
	}

	void Update()
	{
		if (countDown > 0.0f)
		{
			countDown -= Time.deltaTime;
		}
		else
		{
			countDown = finalSpeed;

			offset += 0.25f;
			if (offset >= 1.0f) offset -= 1.0f;

			mat.mainTextureOffset = new Vector2(offset, 0.0f);
		}
	}
}
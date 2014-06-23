using UnityEngine;
using System.Collections;

public class PlatHero : MonoBehaviour {

	MiniCommon mc;
	PlatCollisions pc;

	[SerializeField] private float moveSpeed = 0.5f;
	[SerializeField] private float gravity = 0.1f;
	[SerializeField] private float ySpeedMax = 1.0f;
	[SerializeField] private float jumpHeight = 2.0f;

	// Use this for initialization
	void Awake ()
	{
		mc = GetComponent<MiniCommon> ();	
		pc = GetComponent<PlatCollisions> ();	
	}

	private void Gravity()
	{
		if (pc.ColBot (mc.Vel.y))
		{
			mc.YSpeed = 0.0f;

			if (mc.input.HoldUp())
			{
				if (mc.YSpeed == 0.0f)
				{
					mc.YSpeed = jumpHeight;
				}
			}

			return;
		}

		if (pc.ColTop (mc.Vel.y))
		{
			mc.YSpeed = 0.0f;
			return;
		}

		mc.YSpeed -= gravity;

		if (mc.YSpeed < -ySpeedMax)
		{
			mc.YSpeed = -ySpeedMax;
		}
	}

	// Update is called once per frame
	void Update ()
	{
		Gravity ();



		if (mc.input.HoldRight() && !pc.ColSides(moveSpeed))
		{
			mc.XSpeed = moveSpeed;
		}
		else if (mc.input.HoldLeft() && !pc.ColSides(-moveSpeed))
		{
			mc.XSpeed = -moveSpeed;
		}
		else
		{
			mc.XSpeed = 0.0f;
		}
	}
}

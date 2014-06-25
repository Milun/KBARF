using UnityEngine;
using System.Collections;

public class PlatHero : MonoBehaviour {

	private MiniCommon mc;
	private PlatCollisions pc;
	private PlatGravity pg;

	[SerializeField] private float moveSpeed = 0.5f;
	[SerializeField] private float jumpHeight = 2.0f;

	// Use this for initialization
	void Awake ()
	{
		mc = GetComponent<MiniCommon> ();	
		pc = GetComponent<PlatCollisions> ();
		pg = GetComponent<PlatGravity> ();	
	}

	private void Gravity()
	{
		if (pc.ColTop (mc.Vel.y))
		{
			mc.YSpeed = 0.0f;
			return;
		}
	}

	// Update is called once per frame
	void Update ()
	{
		Gravity ();

		if (mc.input.HoldUp() && pg.OnGround)
		{
			mc.YSpeed = jumpHeight;
		}

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

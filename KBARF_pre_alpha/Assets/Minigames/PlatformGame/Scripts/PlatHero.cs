using UnityEngine;
using System.Collections;

public class PlatHero : MonoBehaviour {

	private PlatCommon pCommon;
	private PlatCollisions pc;
	private PlatGravity pg;

	[SerializeField] private float moveSpeed = 0.5f;
	[SerializeField] private float jumpHeight = 2.0f;

	// Use this for initialization
	void Awake ()
	{
		pCommon = GetComponent<PlatCommon> ();	
		pc = GetComponent<PlatCollisions> ();
		pg = GetComponent<PlatGravity> ();	
	}

	// Update is called once per frame
	void Update ()
	{
		/*if (pc.ColTop (mc.Vel.y))
		{
			mc.YSpeed = 0.0f;
			return;
		}*/

		if (pCommon.input.HoldUp() && pg.OnGround)
		{
			//mc.YSpeed = jumpHeight;
		}

		if (pCommon.input.HoldRight() && !pc.ColSides(moveSpeed))
		{
			print ("HEY!");
			pCommon.XSpeed = moveSpeed;
		}
		else if (pCommon.input.HoldLeft() && !pc.ColSides(-moveSpeed))
		{
			pCommon.XSpeed = -moveSpeed;
		}
		else
		{
			pCommon.XSpeed = 0.0f;
		}
	}
}

using UnityEngine;
using System.Collections;

public class PlatHero : MonoBehaviour {

	private PlatCommon pCommon;
	private PlatCollision pc;
	private PlatGravity pg;

	[SerializeField] private float moveSpeed = 0.5f;
	[SerializeField] private float jumpHeight = 2.0f;

	private MiniInput input;

	// Use this for initialization
	void Awake ()
	{
		pCommon = GetComponent<PlatCommon> ();	
		pc = GetComponent<PlatCollision> ();
		pg = GetComponent<PlatGravity> ();	

		input =	 StatMini.GetMiniContainer(transform).GetComponent<MiniInput> ();
	}

	// Update is called once per frame
	void Update ()
	{
		/*if (pc.ColTop (mc.Vel.y))
		{
			mc.YSpeed = 0.0f;
			return;
		}*/

		pc.CheckCol ();

		if (input.HoldUp() && pc.IsColDown())
		{
			pCommon.YSpeed = jumpHeight;
		}

		if (input.HoldRight())
		{
			pCommon.XSpeed = moveSpeed;
		}
		else if (input.HoldLeft())
		{
			pCommon.XSpeed = -moveSpeed;
		}
		else
		{
			pCommon.XSpeed = 0.0f;
		}
	}
}

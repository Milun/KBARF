using UnityEngine;
using System.Collections;

public class PlatBounce : MonoBehaviour {

	private MiniCommon mc;
	private PlatCollisions pc;

	void Awake ()
	{
		mc = GetComponent<MiniCommon> ();
		pc = GetComponent<PlatCollisions> ();
	}

	void Update()
	{
		if (pc.ColSides(mc.XSpeed))
		{
			print ("zibbidy");
			mc.XSpeed *= -1.0f;
		}

		if (pc.ColTop(mc.YSpeed) || pc.ColBot(mc.YSpeed))
		{
			print ("zobbity");
			mc.YSpeed *= -1.0f;
		}
	}
}

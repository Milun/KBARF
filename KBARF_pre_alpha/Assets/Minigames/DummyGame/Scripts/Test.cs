using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

	MiniCommon mc;

	// Use this for initialization
	void Awake ()
	{
		mc = GetComponent<MiniCommon> ();	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (mc.input.HoldUp())
		{
			mc.Move(Vector2.up*0.4f);
		}

		if (mc.input.HoldDown())
		{
			mc.Move(-Vector2.up*0.4f);
		}

		if (mc.input.HoldRight())
		{
			mc.Move(Vector2.right*0.4f);
		}
		
		if (mc.input.HoldLeft())
		{
			mc.Move(-Vector2.right*0.4f);
		}
	}
}

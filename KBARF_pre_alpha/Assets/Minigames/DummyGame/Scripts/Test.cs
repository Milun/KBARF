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
		if (Input.GetKey("up"))
		{
			mc.Move(Vector2.up*0.4f);
		}

		if (Input.GetKey("down"))
		{
			mc.Move(-Vector2.up*0.4f);
		}

		if (Input.GetKey("right"))
		{
			mc.Move(Vector2.right*0.4f);
		}
		
		if (Input.GetKey("left"))
		{
			mc.Move(-Vector2.right*0.4f);
		}
	}
}

using UnityEngine;
using System.Collections;

public class PlatHero : MonoBehaviour {

	MiniCommon mc;

	[SerializeField] private float moveSpeed = 0.5f;
	[SerializeField] private float gravity = 0.1f;
	[SerializeField] private float ySpeedMax = 1.0f;

	// Use this for initialization
	void Awake ()
	{
		mc = GetComponent<MiniCommon> ();	
	}

	void OnTriggerEnter2D(Collider2D collision) 
	{
		mc.YSpeed = 1.0f;
		print ("DSF");
	}

	private void Gravity()
	{
		mc.YSpeed -= gravity;

		if (mc.YSpeed < -ySpeedMax)
		{
			mc.YSpeed = -ySpeedMax;
		}

		var layerMask = 1 << 9;
		RaycastHit2D hitLeft = Physics2D.Raycast(new Vector3(mc.pos.x+0.01f, mc.pos.y-8.0f, 0.0f) * mc.global.PIXEL_SIZE,
		                                     	mc.move,
		                                     	mc.move.magnitude*mc.global.PIXEL_SIZE,
		                                     	layerMask);
		RaycastHit2D hitRight = Physics2D.Raycast(new Vector3(mc.pos.x+7.99f, mc.pos.y-8.0f, 0.0f) * mc.global.PIXEL_SIZE,
		                                         mc.move,
		                                         mc.move.magnitude*mc.global.PIXEL_SIZE,
		                                         layerMask);
		if (hitLeft.collider != null || hitRight.collider != null)
		{
			mc.YSpeed = 0.0f;
		}
	}

	// Update is called once per frame
	void Update ()
	{
		Gravity ();

		if (mc.input.HoldUp())
		{
			if (mc.YSpeed == 0.0f)
			{
				mc.YSpeed = 2.0f;
			}
		}

		if (mc.input.HoldRight())
		{
			mc.Move(Vector2.right*moveSpeed);
		}
		
		if (mc.input.HoldLeft())
		{
			mc.Move(-Vector2.right*moveSpeed);
		}
	}
}

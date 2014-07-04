using UnityEngine;
using System.Collections;

public class PlatHero : MonoBehaviour {

	private PlatCommon pCommon;
	private PlatCollision pc;
	private PlatGravity pg;
	private PlatGlobal pGlobal;

	[SerializeField] private float moveSpeed = 0.5f;
	[SerializeField] private float jumpHeight = 2.0f;

	private float width;
	private float height;

	private MiniInput input;

	[SerializeField] private GameObject passRoom;
	private PlatRoom pRoom;

	// Use this for initialization
	void Awake ()
	{
		pCommon = GetComponent<PlatCommon> ();	
		pc = GetComponent<PlatCollision> ();
		pg = GetComponent<PlatGravity> ();
		pGlobal = StatMini.GetMiniContainer(transform).GetComponent<PlatGlobal> ();

		input =	 StatMini.GetMiniContainer(transform).GetComponent<MiniInput> ();
	}

	void Start()
	{
		width 	= pc.Offset.x + pc.Bounds.x;
		height 	= pc.Offset.y + pc.Bounds.y;

		pRoom = passRoom.GetComponent<PlatRoom> ();
	}

	private void MoveRoom()
	{
		if (pCommon.XSpeed > 0.0f && pCommon.X + width > pGlobal.ROOM_SIZE.x)
		{
			PlatRoom room = pRoom.MoveRight();
			
			if (room != null)
			{
				GameObject.Destroy(pRoom.gameObject);
				pRoom = room;
				
				pCommon.X = 0.0f;
			}
			else
			{
				pCommon.XSpeed = 0.0f;
				pCommon.X = pGlobal.ROOM_SIZE.x - width;
			}
		}
		else if (pCommon.XSpeed < 0.0f && pCommon.X < 0.0f)
		{
			PlatRoom room = pRoom.MoveLeft();

			if (room != null)
			{
				GameObject.Destroy(pRoom.gameObject);
				pRoom = room;
				
				pCommon.X = pGlobal.ROOM_SIZE.x - width;
			}
			else
			{
				pCommon.XSpeed = 0.0f;
				pCommon.X = 0.0f;
			}
		}



		if (pCommon.Y + height > pGlobal.ROOM_SIZE.y)
		{
			pCommon.Y = 0.0f;
		}
		else if (pCommon.Y < 0.0f)
		{
			pCommon.Y = pGlobal.ROOM_SIZE.y - height;
		}
	}

	// Update is called once per frame
	void Update ()
	{
		// Consider null rooms making you wrap around instead of stop?
		MoveRoom ();


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

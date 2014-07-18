using UnityEngine;
using System.Collections;

public class PlatHero : MonoBehaviour {

	private PlatCommon pCommon;
	private PlatCollision pc;
	private PlatGravity pg;
	private PlatGlobal pGlobal;

	[SerializeField] private float moveSpeed = 0.5f;
	[SerializeField] private float jumpHeight = 2.0f;

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
		pRoom = passRoom.GetComponent<PlatRoom> ();
	}

	private void Die()
	{

	}

	private void MoveRoom()
	{
		if (pCommon.XSpeed > 0.0f && pCommon.X + pc.Offset.x + pc.Bounds.x > pGlobal.ROOM_SIZE.x)
		{
			PlatRoom room = pRoom.MoveRight();
			
			if (room != null)
			{
				GameObject.Destroy(pRoom.gameObject);
				pRoom = room;
				
				pCommon.X = pc.Offset.x;
			}
			else
			{
				pCommon.XSpeed = 0.0f;
				pCommon.X = pGlobal.ROOM_SIZE.x - pc.Offset.x - pc.Bounds.x;
			}
		}
		else if (pCommon.XSpeed < 0.0f && pCommon.X + pc.Offset.x < 0.0f)
		{
			PlatRoom room = pRoom.MoveLeft();

			if (room != null)
			{
				GameObject.Destroy(pRoom.gameObject);
				pRoom = room;
				
				pCommon.X = pGlobal.ROOM_SIZE.x - pc.Offset.x - pc.Bounds.x;
			}
			else
			{
				pCommon.XSpeed = 0.0f;
				pCommon.X = pc.Offset.x;
			}
		}

		
		if (pCommon.YSpeed > 0.0f && pCommon.Y + pc.Offset.y + pc.Bounds.y > pGlobal.ROOM_SIZE.y)
		{
			PlatRoom room = pRoom.MoveUp();
			
			if (room != null)
			{
				GameObject.Destroy(pRoom.gameObject);
				pRoom = room;
				
				pCommon.Y = pc.Offset.y;
			}
			else
			{
				pCommon.YSpeed = 0.0f;
				pCommon.Y = pGlobal.ROOM_SIZE.y - pc.Offset.y - pc.Bounds.y;
			}

		}
		else if (pCommon.YSpeed < 0.0f && pCommon.Y + pc.Offset.y < 0.0f)
		{
			PlatRoom room = pRoom.MoveDown();
			
			if (room != null)
			{
				GameObject.Destroy(pRoom.gameObject);
				pRoom = room;
				
				pCommon.Y = pGlobal.ROOM_SIZE.y - pc.Offset.y - pc.Bounds.y;
			}
			else
			{
				pCommon.YSpeed = 0.0f;
				pCommon.Y = pc.Offset.y;

				Die ();
			}
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

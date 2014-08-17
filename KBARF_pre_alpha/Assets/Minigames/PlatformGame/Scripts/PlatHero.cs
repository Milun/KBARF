using UnityEngine;
using System.Collections;

public class PlatHero : MonoBehaviour {

	private TwoCommon pCommon;
	private PlatBoxPhysTake pTake;
	private PlatBoxCombTake pCombTake;
	private PlatGravity pg;
	private TwoGlobal pGlobal;

	[SerializeField] private float moveSpeed = 0.5f;
	[SerializeField] private float jumpHeight = 2.0f;

	private MiniInput input;

	[SerializeField] private GameObject passRoom;
	private PlatRoom pRoom;

	// Use this for initialization
	void Awake ()
	{
		pCommon = GetComponent<TwoCommon> ();	
		pTake = GetComponent<PlatBoxPhysTake> ();
		pCombTake = GetComponent<PlatBoxCombTake> ();

		pg = GetComponent<PlatGravity> ();
		pGlobal = StatMini.GetMiniContainer(transform).GetComponent<TwoGlobal> ();

		input =	 StatMini.GetMiniContainer(transform).GetComponent<MiniInput> ();
	}

	void Start()
	{
		pRoom = passRoom.GetComponent<PlatRoom> ();
	}

	private void Die()
	{
		pCommon.Pos = new Vector2 (128.0f, 128.0f);
		pCommon.YSpeed = 0.0f;
	}

	private void MoveRoom()
	{
		if (pCommon.XSpeed > 0.0f && pCommon.X + pTake.oTR.x > pGlobal.ROOM_SIZE.x)
		{
			PlatRoom room = pRoom.MoveRight();
			
			if (room != null)
			{
				GameObject.Destroy(pRoom.gameObject);
				pRoom = room;
				
				pCommon.X = pTake.oBL.x;
			}
			else
			{
				pCommon.XSpeed = 0.0f;
				pCommon.X = pGlobal.ROOM_SIZE.x - pTake.oTR.x;
			}
		}
		else if (pCommon.XSpeed < 0.0f && pCommon.X + pTake.oBL.x < 0.0f)
		{
			PlatRoom room = pRoom.MoveLeft();

			if (room != null)
			{
				GameObject.Destroy(pRoom.gameObject);
				pRoom = room;
				
				pCommon.X = pGlobal.ROOM_SIZE.x - pTake.oTR.x;
			}
			else
			{
				pCommon.XSpeed = 0.0f;
				pCommon.X = pTake.oBL.x;
			}
		}

		
		if (pCommon.YSpeed > 0.0f && pCommon.Y + pTake.oTR.y > pGlobal.ROOM_SIZE.y)
		{
			PlatRoom room = pRoom.MoveUp();
			
			if (room != null)
			{
				GameObject.Destroy(pRoom.gameObject);
				pRoom = room;
				
				pCommon.Y = pTake.oBL.y;
			}
			else
			{
				pCommon.YSpeed = 0.0f;
				pCommon.Y = pGlobal.ROOM_SIZE.y - pTake.oTR.y;
			}

		}
		else if (pCommon.YSpeed < 0.0f && pCommon.Y + pTake.oBL.y < 0.0f)
		{
			PlatRoom room = pRoom.MoveDown();
			
			if (room != null)
			{
				GameObject.Destroy(pRoom.gameObject);
				pRoom = room;
				
				pCommon.Y = pGlobal.ROOM_SIZE.y - pTake.oTR.y;
			}
			else
			{
				pCommon.YSpeed = 0.0f;
				pCommon.Y = pTake.oBL.y;

				Die ();
			}
		}
	}

	// Update is called once per frame
	void Update ()
	{
		// Consider null rooms making you wrap around instead of stop?
		MoveRoom ();

		if (pCombTake.CheckCol())
		{
			Die ();
		}

		if (input.HoldUp() && pTake.IsColDown())
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

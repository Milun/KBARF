using UnityEngine;
using System.Collections;

public class PlatHero : MonoBehaviour {

	private TwoCommon pCommon;
	private TwoColSquare tCol;
	private PlatGravity 	pGravity;
	private PlatPhysRamp 	pRamp;
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
		tCol = GetComponent<TwoColSquare> ();

		pGravity = GetComponent<PlatGravity> ();
		pRamp = GetComponent<PlatPhysRamp> ();

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
		if (pCommon.XSpeed > 0.0f && pCommon.X + tCol.localTR.x > pGlobal.ROOM_SIZE.x)
		{
			PlatRoom room = pRoom.MoveRight();
			
			if (room != null)
			{
				GameObject.Destroy(pRoom.gameObject);
				pRoom = room;
				
				pCommon.X = tCol.localBL.x;
			}
			else
			{
				pCommon.XSpeed = 0.0f;
				pCommon.X = pGlobal.ROOM_SIZE.x - tCol.localTR.x;
			}
		}
		else if (pCommon.XSpeed < 0.0f && pCommon.X + tCol.localBL.x < 0.0f)
		{
			PlatRoom room = pRoom.MoveLeft();

			if (room != null)
			{
				GameObject.Destroy(pRoom.gameObject);
				pRoom = room;
				
				pCommon.X = pGlobal.ROOM_SIZE.x - tCol.localTR.x;
			}
			else
			{
				pCommon.XSpeed = 0.0f;
				pCommon.X = tCol.localBL.x;
			}
		}

		
		if (pCommon.YSpeed > 0.0f && pCommon.Y + tCol.localTR.y > pGlobal.ROOM_SIZE.y)
		{

			PlatRoom room = pRoom.MoveUp();
			
			if (room != null)
			{
				GameObject.Destroy(pRoom.gameObject);
				pRoom = room;
				
				pCommon.Y = tCol.localBL.y;
			}
			else
			{
				pCommon.YSpeed = 0.0f;
				pCommon.Y = pGlobal.ROOM_SIZE.y - tCol.localTR.y;
			}

		}
		else if (pCommon.YSpeed < 0.0f && pCommon.Y + tCol.localBL.y < 0.0f)
		{
			PlatRoom room = pRoom.MoveDown();
			
			if (room != null)
			{
				GameObject.Destroy(pRoom.gameObject);
				pRoom = room;
				
				pCommon.Y = pGlobal.ROOM_SIZE.y - tCol.localTR.y;
			}
			else
			{
				pCommon.YSpeed = 0.0f;
				pCommon.Y = tCol.localBL.y;

				Die ();
			}
		}
	}

	// Update is called once per frame
	void Update ()
	{
		MoveRoom ();

		if (tCol.ColManager.IsCol(tCol, TwoCol.ColType.COMBAT_OFF))
		{
			Die ();
		}

		if (Input.GetKey("up") && (pGravity.OnGround || pRamp.OnRamp))
		{
			print ("not working");
			pCommon.YSpeed = jumpHeight;
		}

		if (Input.GetKey("down") && (pRamp))
		{
			pRamp.Ignore();
		}

		if (Input.GetKey("right"))
		{
			pCommon.XSpeed = moveSpeed;
		}
		else if (Input.GetKey("left"))
		{
			pCommon.XSpeed = -moveSpeed;
		}
		else
		{
			pCommon.XSpeed = 0.0f;
		}
	}
}

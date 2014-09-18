using UnityEngine;
using System.Collections;

public class PlatHero : MonoBehaviour {

	private TwoCommon pCommon;
	private TwoColSquare tCol;
	private PlatGravity 	pGravity;
	private PlatPhysRamp 	pRamp;
	private TwoGlobal pGlobal;

	[SerializeField] private float moveSpeed = 0.5f;
	[SerializeField] private float attackSpeedMulti = 1.2f;
	[SerializeField] private float attackDuration = 1.0f;
	[SerializeField] private float jumpForce = 2.0f;

	private bool jump = false;
	private float attacking = 0.0f;
	private float lastXSpeed = 0.0f;

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

	private bool OnGround()
	{
		return (pGravity.OnGround || pRamp.OnRamp);
	}

	// Update is called once per frame
	void Update ()
	{
		MoveRoom ();

		if (tCol.ColManager.IsCol(tCol, TwoCol.ColType.COMBAT_OFF))
		{
			Die ();
		}

		// Perform the attack
		if (attacking > 0.0f)
		{
			// Your attack time only counts down when you're grounded.
			if (OnGround()) attacking -= Time.deltaTime;

			transform.localScale = new Vector3(1.5f, 0.75f, 1.0f);
		}
		else
		{
			transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
		

			if (Input.GetKey("down") && (pRamp))
			{
				pRamp.IgnoreCol();
			}
			
			// Make all horizontal speed stop if certain conditions are met.
			if ( pGravity.MaxSpeed || (!OnGround() && !jump) )
			{
				lastXSpeed = 0.0f;
			}
			
			// Grounded behavior + Horizontal movement.
			if (OnGround())
			{
				if (pCommon.YSpeed <= 0.0f) jump = false;

				if (!jump)
				{

					if (Input.GetKey("right"))		lastXSpeed = moveSpeed;
					else if (Input.GetKey("left"))	lastXSpeed = -moveSpeed;
					else 							lastXSpeed = 0.0f;
					
					if (Input.GetKeyDown("space") && OnGround())
					{
						attacking = attackDuration;
						lastXSpeed *= attackSpeedMulti;
					}

				}
			}

			pCommon.XSpeed = lastXSpeed;

		}

		if (Input.GetKey("up") && (OnGround() || attacking > 0.0f) )
		{
			jump = true;
			attacking = 0.0f;
			pCommon.YSpeed = jumpForce;
		}


	}
}

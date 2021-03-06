﻿using UnityEngine;
using System.Collections;

public class PlatHero : MonoBehaviour {

	private TwoCommon pCommon;
	private TwoColSquare tCol;
	private PlatGravity 	pGravity;
	private PlatPhysRamp 	pRamp;
	private TwoGlobal pGlobal;
	private TwoColPhysics tColPhys;

	[SerializeField] private float moveSpeed = 0.5f;
	[SerializeField] private float attackSpeedMulti = 1.2f;
	[SerializeField] private float attackDuration = 1.0f;
	[SerializeField] private float jumpForce = 2.0f;

	[SerializeField] private GameObject deathFlash;
	[SerializeField] private float deathSleep = 2.0f;
	private float deathTimer = 0.0f;

	private bool jump = false;
	private float attacking = 0.0f;
	private float lastXSpeed = 0.0f;

	private Vector2 spawnPoint = Vector2.zero;
	private Vector2 spawnSpeed = Vector2.zero;

	private MiniInput input;

	[SerializeField] private GameObject passRoom;
	private PlatRoom pRoom;

	// Use this for initialization
	void Awake ()
	{
		pCommon = GetComponent<TwoCommon> ();	
		tCol = GetComponent<TwoColSquare> ();
		tColPhys = GetComponent<TwoColPhysics> ();

		pGravity = GetComponent<PlatGravity> ();
		pRamp = GetComponent<PlatPhysRamp> ();

		pGlobal = StatMini.GetMiniContainer(transform).GetComponent<TwoGlobal> ();

		input =	 StatMini.GetMiniContainer(transform).GetComponent<MiniInput> ();


	}

	void Start()
	{
		pRoom = passRoom.GetComponent<PlatRoom> ();

		deathFlash.renderer.enabled = false;

		spawnPoint = this.pCommon.Pos;
	}

	public void Die()
	{
		if (deathTimer > 0.0f)
		{
			return;
		}

		pCommon.YSpeed = 0.0f;
		pCommon.XSpeed = 0.0f;

		pGravity.On = false;

		deathTimer = deathSleep;
		deathFlash.renderer.enabled = true;
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

				pCommon.X = tCol.localTR.x;

				spawnPoint = pCommon.Pos;
				spawnSpeed = pCommon.Vel;
			}
			else
			{
				pCommon.XSpeed = 0.0f;
				pCommon.X = pGlobal.ROOM_SIZE.x - tCol.localTR.x;

				//spawnPoint = pCommon.Pos;
				//spawnSpeed = pCommon.Vel;
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

				spawnPoint = pCommon.Pos;
				spawnSpeed = pCommon.Vel;
			}
			else
			{
				pCommon.XSpeed = 0.0f;
				pCommon.X = tCol.localBL.x;

				//spawnPoint = pCommon.Pos;
				//spawnSpeed = pCommon.Vel;
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

				spawnPoint = pCommon.Pos;
				spawnSpeed = pCommon.Vel;
			}
			else
			{
				pCommon.YSpeed = 0.0f;
				pCommon.Y = pGlobal.ROOM_SIZE.y - tCol.localTR.y;

				//spawnPoint = pCommon.Pos;
				//spawnSpeed = pCommon.Vel;
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

				spawnPoint = pCommon.Pos;
				spawnSpeed = pCommon.Vel;
			}
			else
			{
				pCommon.YSpeed = 0.0f;
				pCommon.Y = tCol.localBL.y;

				//spawnPoint = pCommon.Pos;
				//spawnSpeed = pCommon.Vel;

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
		if (deathTimer > 0.0f)
		{
			deathTimer -= Time.deltaTime;
			if (deathTimer <= 0.0f)
			{
				pCommon.Pos = spawnPoint;
				pGravity.On = true;
				deathFlash.renderer.enabled = false;
			}

			return;
		}

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

			// Go up ramps automatically while attacking.
			pRamp.ForceRamp();
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

			// Make it so you don't move after bumping into a wall.
			if (pCommon.YSpeed < 0.0f && pCommon.XSpeed == 0.0f) lastXSpeed = 0.0f;

			if (tColPhys.Move.x != 0.0f)
			{
				lastXSpeed = 0.0f;
			}

			pCommon.XSpeed = lastXSpeed;

		}

		if (Input.GetKey("up") && (OnGround() || attacking > 0.0f) )
		{
			jump = true;
			attacking = 0.0f;
			pCommon.Y++;
			pCommon.YSpeed = jumpForce;
		}
	}
}

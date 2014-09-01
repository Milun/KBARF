﻿using UnityEngine;
using System.Collections.Generic;

public class BatHero : MonoBehaviour {

	private TwoColCircle[] col;

	[SerializeField] private float xSpeedMax = 1.0f;
	[SerializeField] private float xSpeedAccel = 0.1f;
	[SerializeField] private float xFriction = 0.98f;
	[SerializeField] private float gravity = 0.1f;
	[SerializeField] private float ySpeedMax = 1.5f;
	[SerializeField] private float flapHeight = 1.0f;

	[SerializeField] private BatHUDBar energyBar;

	private float energy = 100.0f;

	private float xSpeed, ySpeed = 0.0f;

	// Use this for initialization
	void Awake ()
	{
		col = GetComponents<TwoColCircle> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (col.Length > 0)
		{
			TwoColCircle colPhys = col[0];
			TwoColCircle colOff = col[1];

			List<TwoColManager.Col> other = colPhys.ColManager.CheckCol (colPhys);

			if (other.Count > 0)
			{
				foreach (TwoColManager.Col e in other)
				{
					transform.position += (Vector3)e.move;
				}
			}

			List<TwoColManager.Col> other2 = colOff.ColManager.CheckCol (colOff);

			if (other2.Count > 0)
			{
				foreach (TwoColManager.Col e in other2)
				{
					
					BatMoth colMoth = e.col.GetComponent<BatMoth>();
					BatEnemy colEnemy = e.col.GetComponent<BatEnemy>();
					
					if (colMoth)
					{
						colMoth.Die();
						energy += 5.0f;
						if (energy > 100.0f) energy = 100.0f;
						energyBar.SetValue(energy/100.0f);
					}
					else
						if (colEnemy)
					{
						transform.position = new Vector3(100.0f, 150.0f, 0.0f);
					}
				}
			}
		}
	
		if (Input.GetKey("left"))
		{
			xSpeed -= xSpeedAccel;
		}
		else if (Input.GetKey("right"))
		{
			xSpeed += xSpeedAccel;
		}
		else
		{
			if (Mathf.Abs(xSpeed) > 0.05f)
			{
				xSpeed *= xFriction;
			}
			else
			{
				xSpeed = 0.0f;
			}
		}

		xSpeed = Mathf.Clamp(xSpeed, -xSpeedMax, xSpeedMax);

		if (ySpeed > -ySpeedMax)
		{
			ySpeed -= gravity;
		}

		if (energy > 0.0f && Input.GetKeyDown("up"))
		{
			ySpeed += flapHeight;

			energy -= 5.0f;
			if (energy < 0.0f) energy = 0.0f;

			energyBar.SetValue(energy/100.0f);
		}

		ySpeed = Mathf.Clamp(ySpeed, -ySpeedMax, ySpeedMax);

		transform.position += new Vector3 (xSpeed, ySpeed, 0.0f);
	}
}

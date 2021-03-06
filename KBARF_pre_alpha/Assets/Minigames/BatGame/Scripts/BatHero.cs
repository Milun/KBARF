﻿using UnityEngine;
using System.Collections.Generic;

public class BatHero : MonoBehaviour {

	private TwoColCircle col;
	private BatController controller;

	[SerializeField] private float xSpeedMax = 1.0f;
	[SerializeField] private float xSpeedAccel = 0.1f;
	[SerializeField] private float xSpeedAccelChange = 0.5f;

	[SerializeField] private float xFriction = 0.98f;
	[SerializeField] private float xFrictionChange = 0.3f;

	[SerializeField] private float gravity = 0.1f;
	[SerializeField] private float ySpeedMax = 1.5f;
	[SerializeField] private float ySpeedGlide = 1.0f;
	[SerializeField] private float flapHeight = 1.0f;

	[SerializeField] private BatHUDBar energyBar;
	[SerializeField] private BatHUDBar weightBar;

	[SerializeField] private BatVectorAnim anim;

	private float energy = 1.0f;
	private float weight = 1.0f;

	private float xSpeed, ySpeed = 0.0f;

	// Use this for initialization
	void Awake ()
	{
		col = GetComponent<TwoColCircle> ();
	}

	void Start()
	{
		anim.InitSprites ();
		controller = GameObject.Find("cam_main").GetComponent<BatController>();
	}

	public void Die()
	{
		controller.Mute();
		Destroy (this.gameObject);
	}

	public void EatMoth()
	{
		energy += 0.05f;
		if (energy > 1.0f) energy = 1.0f;
		
		weight += 0.05f;
		if (weight > 1.0f) weight = 1.0f;
		
		energyBar.SetValue(energy);
		weightBar.SetValue(weight);
	}

	// Update is called once per frame
	void Update () {

		anim.Draw (this.transform.position);

		if (col)
		{
			TwoColCircle colPhys = col;
			TwoColCircle colOff = col;

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
			xSpeed -= xSpeedAccel - xSpeedAccel*xSpeedAccelChange*weight;
		}
		else if (Input.GetKey("right"))
		{
			xSpeed += xSpeedAccel - xSpeedAccel*xSpeedAccelChange*weight;
		}
		else
		{
			if (Mathf.Abs(xSpeed) > 0.05f)
			{
				xSpeed *= xFriction + (1.0f-xFriction)*xFrictionChange*weight;
			}
			else
			{
				xSpeed = 0.0f;
			}
		}

		if (xSpeed > xSpeedMax || xSpeed < -xSpeedMax)
		{
			xSpeed *= 0.95f;
		}

		if (ySpeed > -ySpeedMax)
		{
			ySpeed -= gravity;
		}

		if (energy > 0.0f && Input.GetKeyDown("up"))
		{
			ySpeed += flapHeight;

			if (Input.GetKey("right"))
			{
				xSpeed -= xSpeedAccel - xSpeedAccel*xSpeedAccelChange*weight*50.0f;
			}
			else if (Input.GetKey("left"))
			{
				xSpeed += xSpeedAccel - xSpeedAccel*xSpeedAccelChange*weight*50.0f;
			}

			energy -= 0.05f;
			if (energy < 0.0f) energy = 0.0f;

			weight -= 0.025f;
			if (weight < 0.0f) weight = 0.0f;

			energyBar.SetValue(energy);
			weightBar.SetValue(weight);

			anim.PlayOnce(0, 4, 0.04f);
		}

		if (Input.GetKey("up"))
		{
			if (ySpeed < -ySpeedGlide)
			{
				ySpeed = -ySpeedGlide;//(ySpeed + ySpeedGlide)*0.5f;
			}
		}

		if (Time.frameCount % 50 == 0)
		{
			weight -= 0.01f;
			weightBar.SetValue(weight);
		}

		ySpeed = Mathf.Clamp(ySpeed, -ySpeedMax, ySpeedMax);

		transform.position += new Vector3 (xSpeed, ySpeed, 0.0f);
	}
}

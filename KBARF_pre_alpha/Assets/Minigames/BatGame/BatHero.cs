using UnityEngine;
using System.Collections;

public class BatHero : MonoBehaviour {

	private TwoColCircle col;

	[SerializeField] private float xSpeedMax = 1.0f;
	[SerializeField] private float xSpeedAccel = 0.1f;
	[SerializeField] private float xFriction = 0.98f;
	[SerializeField] private float gravity = 0.1f;
	[SerializeField] private float ySpeedMax = 1.5f;
	[SerializeField] private float flapHeight = 1.0f;

	private float xSpeed, ySpeed = 0.0f;

	// Use this for initialization
	void Awake ()
	{
		col = GetComponent<TwoColCircle> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (col)
		{
			TwoCol other = col.ColManager.CheckCol (col);

			if (other)
			{
				BatMoth colMoth = other.GetComponent<BatMoth>();
				BatEnemy colEnemy = other.GetComponent<BatEnemy>();

				if (colMoth)
				{
					colMoth.Die();
				}
				else
				if (colEnemy)
				{
					transform.position = new Vector3(47.0f, 24.0f, 0.0f);
				}
				else
				{
					transform.position = new Vector3(47.0f, 24.0f, 0.0f);
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

		if (Input.GetKeyDown("up"))
		{
			ySpeed += flapHeight;
		}

		ySpeed = Mathf.Clamp(ySpeed, -ySpeedMax, ySpeedMax);

		transform.position += new Vector3 (xSpeed, ySpeed, 0.0f);
	}
}

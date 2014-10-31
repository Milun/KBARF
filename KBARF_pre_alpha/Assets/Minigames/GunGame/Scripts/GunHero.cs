using UnityEngine;
using System.Collections;

public class GunHero : MonoBehaviour {

	CharacterController charController;

	private Vector3 groundNormal = Vector3.zero;

	private float viewAngle = 0;

	// Use this for initialization
	void Awake () {
		charController = GetComponent<CharacterController> ();
	}

	void OnControllerColliderHit(ControllerColliderHit col)
	{
		groundNormal = col.normal;
		print (groundNormal.x + " " + groundNormal.y + " " + groundNormal.z);
		//this.transform.up = col.normal;

		//Vector3 fwd = transform.forward;
		this.transform.up = groundNormal;
		//this.transform.forward = fwd;

		//transform.rotation = Quaternion.FromToRotation (Vector3.up, col.normal);/* * Quaternion.AngleAxis( viewAngle, col.normal)*/;

		//Quaternion quatHit = Quaternion.FromToRotation(Vector3.up , col.normal);
		//Quaternion quatForward = Quaternion.FromToRotation(this.transform.up, this.transform.forward);
		//Quaternion quatC = quatHit * quatForward;
		//transform.rotation = quatC;

		//this.transform.RotateAround(this.transform.position, col.normal, viewAngle);

		//this.transform.eulerAngles = 
	}

	private void Move()
	{
		if (Input.GetKey(KeyCode.W))
		{
			charController.Move(transform.forward);
		}
		else if (Input.GetKey(KeyCode.S))
		{
			charController.Move(-transform.forward);
		}

		if (Input.GetKey(KeyCode.A))
		{
			charController.Move(-transform.right);
		}
		if (Input.GetKey(KeyCode.D))
		{
			charController.Move( transform.right);
		}

		if (Input.GetKey(KeyCode.LeftArrow))
		{
			viewAngle -= 1.0f;
		}
		if (Input.GetKey(KeyCode.RightArrow))
		{
			viewAngle += 1.0f;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		charController.Move(-groundNormal * Time.deltaTime * 30.0f);

		Move ();
	}
}

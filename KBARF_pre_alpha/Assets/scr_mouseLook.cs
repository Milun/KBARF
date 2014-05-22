using UnityEngine;
using System.Collections;
[AddComponentMenu("Camera-Control/Mouse Look")]

public class scr_mouseLook : MonoBehaviour {

	[SerializeField] private float lookSpeed = 1.0f;

	private float x = 0.0f;
	private float y = 0.0f;

	private float moveSpeed = 0.1f;

	Quaternion rotStart;

	void Start() {

		rotStart = transform.localRotation;

		Screen.showCursor = false;
		Screen.lockCursor = true;
	}

	// Update is called once per frame
	void Update () {
	
		x += lookSpeed * Input.GetAxis ("Mouse X");
		y += lookSpeed * Input.GetAxis ("Mouse Y");

		if (x < -360.0f)
			x += 360.0f;
		else if (x > 360.0f)
			x -= 360.0f;
	
		if (y < -90.0f)
			y = -90.0f;
		else if (y > 90.0f)
			y = 90.0f;

		

		
		Quaternion xQuaternion = Quaternion.AngleAxis (x, Vector3.up);
		Quaternion yQuaternion = Quaternion.AngleAxis (y, -Vector3.right);
		
		transform.localRotation = rotStart * xQuaternion * yQuaternion;

		if (Input.GetKey("w"))
		{
			transform.position += new Vector3( Mathf.Sin ( Mathf.Deg2Rad*x )*moveSpeed, 0.0f, Mathf.Cos ( Mathf.Deg2Rad*x )*moveSpeed);
		}
		else if (Input.GetKey("s"))
		{
			transform.position -= new Vector3( Mathf.Sin ( Mathf.Deg2Rad*x )*moveSpeed, 0.0f, Mathf.Cos ( Mathf.Deg2Rad*x )*moveSpeed);
		}

		if (Input.GetKey("a"))
		{
			transform.position += new Vector3( Mathf.Sin ( Mathf.Deg2Rad*(x-90.0f) )*moveSpeed, 0.0f, Mathf.Cos ( Mathf.Deg2Rad*(x-90.0f) )*moveSpeed);
		}
		else if (Input.GetKey("d"))
		{
			transform.position += new Vector3( Mathf.Sin ( Mathf.Deg2Rad*(x+90.0f) )*moveSpeed, 0.0f, Mathf.Cos ( Mathf.Deg2Rad*(x+90.0f) )*moveSpeed);
		}

	}
}

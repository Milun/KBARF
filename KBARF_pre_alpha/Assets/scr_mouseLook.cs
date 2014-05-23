using UnityEngine;
using System.Collections;
[AddComponentMenu("Camera-Control/Mouse Look")]

public class scr_mouseLook : MonoBehaviour {

	[SerializeField] private float lookSpeed = 1.0f;
	[SerializeField] private float moveSpeed = 0.1f;

	private float xAim 		= 0.0f;
	private float yAim 		= 0.0f;

	private Quaternion rotStart;

	void Start() {

		rotStart = transform.localRotation;

		Screen.showCursor = false;
		Screen.lockCursor = true;
	}

	private void MouseAim()
	{
		// Change the angle of the camera.
		xAim += lookSpeed * Input.GetAxis ("Mouse X");
		yAim += lookSpeed * Input.GetAxis ("Mouse Y");

		// Wrap the camera angles around.
		if (xAim < -360.0f)
		{
			xAim += 360.0f;
		}
		else if (xAim > 360.0f)
		{
			xAim -= 360.0f;
		}
		
		if (yAim < -90.0f)
		{
			yAim = -90.0f;
		}
		else if (yAim > 90.0f)
		{
			yAim = 90.0f;
		}

		// Actually change the angle of the camera.
		Quaternion xQ = Quaternion.AngleAxis (xAim, Vector3.up);
		Quaternion yQ = Quaternion.AngleAxis (yAim, -Vector3.right);

		transform.localRotation = rotStart * xQ * yQ;
	}

	// Update is called once per frame
	void Update () {
	
		MouseAim ();

		Vector3 xVec = new Vector3(Mathf.Sin ( Mathf.Deg2Rad*xAim )*moveSpeed,
		                           0.0f,
		                           Mathf.Cos ( Mathf.Deg2Rad*xAim )*moveSpeed);

		Vector3 yVec = new Vector3(Mathf.Sin ( Mathf.Deg2Rad*xAim+90.0f )*moveSpeed,
		                           0.0f,
		                           Mathf.Cos ( Mathf.Deg2Rad*xAim+90.0f )*moveSpeed);


		if (Input.GetKey("w")) 		transform.position += xVec;
		else if (Input.GetKey("s")) transform.position -= xVec;

		if (Input.GetKey("a"))		transform.position -= yVec;
		else if (Input.GetKey("d")) transform.position += yVec;

	}
}

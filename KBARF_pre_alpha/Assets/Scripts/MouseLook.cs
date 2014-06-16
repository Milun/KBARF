using UnityEngine;
using System.Collections;
[AddComponentMenu("Camera-Control/Mouse Look")]

public class MouseLook : MonoBehaviour {

	[SerializeField] private float lookSpeed = 1.0f;	// Speed at which the mouse aim moves.

	private float xAim 		= 0.0f;						// The aim of the mouselook.
	private float yAim 		= 0.0f;

	void Start() {

		Screen.showCursor = false;
	}

	public void MouseAim()
	{
		// Change the angle of the camera.
		xAim = lookSpeed * Input.GetAxis ("Mouse X");
		yAim = lookSpeed * Input.GetAxis ("Mouse Y");

		// Now rotate the camera.
		transform.Rotate (-Vector3.right * yAim);
		transform.Rotate (Vector3.up * xAim);
	}

	public void SetMouseAim(float _xAim, float _yAim)
	{
		xAim = _xAim;
		yAim = _yAim;
	}
}

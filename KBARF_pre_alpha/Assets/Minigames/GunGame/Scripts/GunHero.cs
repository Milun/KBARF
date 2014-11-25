using UnityEngine;
using System.Collections;

public class GunHero : MonoBehaviour {

	CharacterController charController;
	Transform myTransform;

	// Use this for initialization
	void Awake () {
		charController = GetComponent<CharacterController> ();
		myTransform = this.transform;
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
			myTransform.eulerAngles += Vector3.down;
		}
		if (Input.GetKey(KeyCode.D))
		{
			myTransform.eulerAngles += Vector3.up;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{

		Move ();
	}
}

using UnityEngine;
using System.Collections;

public class GunHero : MonoBehaviour {

	CharacterController charController;

	// Use this for initialization
	void Awake () {
		charController = GetComponent<CharacterController> ();
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
			this.transform.eulerAngles = this.transform.eulerAngles - new Vector3(0.0f, 1.5f, 0.0f);
		}
		if (Input.GetKey(KeyCode.RightArrow))
		{
			this.transform.eulerAngles = this.transform.eulerAngles + new Vector3(0.0f, 1.5f, 0.0f);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		charController.Move( Vector3.down * Time.deltaTime * 30.0f);

		Move ();
	}
}

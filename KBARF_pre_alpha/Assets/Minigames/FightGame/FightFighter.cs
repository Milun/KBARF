using UnityEngine;
using System.Collections;

public class FightFighter : MonoBehaviour {

	[SerializeField] float walkSpeed = 10.0f;

	private Vector3 vel = Vector3.zero;
	private float realEuls = 0.0f;
	private float targetEuls = 0.0f;

	private Anim 				anim;
	private CharacterController controller;
	private Transform			myTransform;

	// Use this for initialization
	void Awake () {
		anim 		= GetComponent<Anim> ();
		controller 	= GetComponent<CharacterController> ();

		myTransform = this.transform;
	}

	public float XSpeed
	{
		set
		{
			vel.x = value;
		}

		get
		{
			return vel.x;
		}
	}

	public float YSpeed
	{
		set
		{
			vel.z = value;
		}
		
		get
		{
			return vel.z;
		}
	}

	public float ZSpeed
	{
		set
		{
			vel.y = value;
		}
		
		get
		{
			return vel.y;
		}
	}

	private void Walk (float xPercent, float yPercent)
	{
		XSpeed = xPercent*walkSpeed*Time.deltaTime;
		YSpeed = yPercent*walkSpeed*Time.deltaTime;
	}

	private void RotateModel()
	{
		if (XSpeed == 0.0f && YSpeed == 0.0f)
		{
			anim.PlayAnimationLooped ("anim_idle", 2.0f, 0.3f);
			return;
		}

		anim.PlayAnimationLooped ("anim_run", vel.magnitude, 0.3f);

		transform.rotation = Quaternion.LookRotation(new Vector3(vel.x, 0.0f, vel.z));
	}

	private void UpdateController ()
	{
		// Move the character
		controller.Move (vel);

		// Rotate Model
		RotateModel ();
	}

	// Update is called once per frame
	void Update () {
		Walk (-StatMain.BoolToInt(Input.GetKey(KeyCode.LeftArrow)) + StatMain.BoolToInt(Input.GetKey(KeyCode.RightArrow)),
		       StatMain.BoolToInt(Input.GetKey(KeyCode.UpArrow))   - StatMain.BoolToInt(Input.GetKey(KeyCode.DownArrow)));

		if (Input.GetKey(KeyCode.LeftControl)) ZSpeed = 1.0f;

		UpdateController ();
	}
}

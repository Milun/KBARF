using UnityEngine;
using System.Collections;

public enum States 	{
	IDLE,
	WALKING
};

public class PetHero : MonoBehaviour {

	private TwoCommon tCommon;
	private Animator anim;

	[SerializeField] private float animSpeed = 0.1f;

	private float hunger 	= 100.0f;
	private float health	= 100.0f;
	private float joy 		= 50.0f;
	private float stamina	= 0.0f;

	private float moveSpeed = 0.0f;

	// Use this for initialization
	void Awake () {
		tCommon = GetComponent<TwoCommon> ();
		anim = GetComponent<Animator> ();
	}

	public float MoveSpeed
	{
		get
		{
			return moveSpeed;
		}
	}

	private void Anim () {
		//GetComponent<Animator>().speed = animSpeed * moveSpeed * 2.0f;
	}

	private void Age() {
		hunger -= Time.deltaTime;

		if (hunger <= 20.0f)
		{
			health -= Time.deltaTime;
		}
	}

	private void Walk() {

		if (stamina < 10.0f)
		{
			anim.SetBool ("Walk", false);
			stamina += Time.deltaTime;

			if ( Mathf.Floor(stamina % 5.0f) >= 3.0f)
			{
				anim.SetBool ("UniqueIdle", true);
			}
			else
			{
				anim.SetBool ("UniqueIdle", false);
			}
		}
		else
		{
			anim.SetBool ("UniqueIdle", false);
			anim.SetBool ("Walk", true);
		}
	}

	// Update is called once per frame
	void Update () {


		Walk ();

		if (anim.GetCurrentAnimatorStateInfo(0).IsName("anim_petWalk"))
		{
			moveSpeed = (hunger + health) / 200.0f;
		}
		else
		{
			moveSpeed = 0.0f;
		}

		Anim ();
		Age ();

	}
}

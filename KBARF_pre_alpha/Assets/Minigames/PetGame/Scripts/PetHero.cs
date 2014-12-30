using UnityEngine;
using System.Collections;

public enum States 	{
	IDLE,
	WALKING
};

public class PetHero : MonoBehaviour {

	private TwoCommon tCommon;
	private Animator anim;

	public UnityEngine.Texture test;
	public UnityEngine.Texture test2;
	public TextAsset imageTextAsset;

	[SerializeField] private float animSpeed = 0.1f;

	private float hunger 	= 100.0f;
	private float health	= 100.0f;
	private float joy 		= 50.0f;
	private float stamina	= 0.0f;

	private float moveSpeed = 0.0f;

	public bool stop = false;

	/*var frames : Texture2D[];
	
	var framesPerSecond = 10.0;
	
	function Update () {
		var index : int = Time.time * framesPerSecond;
		index = index % frames.Length;
		renderer.material.mainTexture = frames[index];
	}*/


	// Use this for initialization
	void Awake () {
		tCommon = GetComponent<TwoCommon> ();
		anim = GetComponent<Animator> ();

		//gameObject.GetComponent<SpriteRenderer> ().sprite = test;

		//imageTextAsset = Resources.Load("tex_petDinoTEST.png") as TextAsset;

		//Texture2D tex = new Texture2D(64, 32);
		//tex.LoadImage(imageTextAsset.bytes);
		//renderer.material.mainTexture = (Texture2D)Resources.Load("tex_petDinoTEST.png");

		//anim.get

		//test2.name = test.name;
		//this.GetComponent<SpriteRenderer>().material.SetTexture("_MainTex", test);

		/*string skin;
		SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();

		print ("LOOK: " + renderers.Length);

		for (int i = 0; i < renderers.Length; i++) {

			MaterialPropertyBlock block = new MaterialPropertyBlock();
			block.AddTexture("_MainTex", test);
			renderers[i].SetPropertyBlock(block);
		}*/

	}

	public float MoveSpeed
	{
		get
		{
			return moveSpeed;
		}
	}

	private void Anim () {
		GetComponent<Animator>().speed = animSpeed * moveSpeed * 2.0f;
	}

	private void Age() {
		hunger -= Time.deltaTime;

		if (hunger <= 20.0f)
		{
			health -= Time.deltaTime;
		}
	}

	private void Walk() {

		if (stamina < 1.0f)
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

		if (!stop)
		{
			Walk ();
		}
		else
		{
			if (joy < 100.0f)
			{
				joy += Time.deltaTime * 10.0f;
			}
			anim.SetBool ("Walk", false);
			anim.SetBool ("Special", true);
		}

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

	void LateUpdate()
	{
		//make this ReadOnlyCollectionBase happen when the Sprite (name) changes

		/*string skin;
		SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();

		for (int i = 0; i < renderers.Length; i++) {

			MaterialPropertyBlock block = new MaterialPropertyBlock();
			block.AddTexture("_MainTex", test);
			renderers[i].SetPropertyBlock(block);
		}*/
	}

	public bool Joy
	{
		get
		{
			return (joy >= 100.0f);
		}
	}
}

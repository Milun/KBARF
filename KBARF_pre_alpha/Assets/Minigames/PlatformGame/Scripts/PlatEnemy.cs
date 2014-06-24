using UnityEngine;
using System.Collections;

public class PlatEnemy : MonoBehaviour {

	MiniCommon mc;
	PlatCollisions pc;

	[SerializeField] private float animSpeed = 0.1f;

	void Awake ()
	{
		mc = GetComponent<MiniCommon> ();	
		pc = GetComponent<PlatCollisions> ();	
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "PlatHero")
			print ("Whoops");
		
	}

	// Use this for initialization
	void Start () {
	
		GetComponent<Animator>().speed = animSpeed;

		mc.XSpeed = mc.YSpeed = 0.2f;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

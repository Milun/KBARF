using UnityEngine;
using System.Collections;

public class PlatEnemy : MonoBehaviour {

	private PlatCommon pCommon;
	private PlatCollisions pc;

	[SerializeField] private float animSpeed = 0.1f;
	[SerializeField] private Vector2 startMove = Vector2.zero;

	void Awake ()
	{
		pCommon = GetComponent<PlatCommon> ();	
		pc = GetComponent<PlatCollisions> ();	
	}

	// Use this for initialization
	void Start () {
		
		GetComponent<Animator>().speed = animSpeed;
		
		pCommon.Vel = startMove;
		
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "PlatHero")
			print ("Whoops");
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

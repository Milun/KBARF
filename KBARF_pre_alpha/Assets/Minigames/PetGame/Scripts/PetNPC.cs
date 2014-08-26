using UnityEngine;
using System.Collections;

public class PetNPC : MonoBehaviour {

	private TwoCommon tCommon;
	
	// Use this for initialization
	void Awake () {
		tCommon = GetComponent<TwoCommon> ();
		
	}
	
	// Update is called once per frame
	void Update () {

		tCommon.XSpeed = GameObject.Find("pre_petHero").GetComponent<PetHero>().MoveSpeed * -0.1f;
		
		if (tCommon.X < -8.0f)
		{
			tCommon.X = 64.0f;
			tCommon.Y = Random.Range(10, 25);
		}

		if (transform.position.x <= 53.0f)
		{
			GameObject.Find("pre_petHero").GetComponent<PetHero>().stop = true;
		}
	}
}

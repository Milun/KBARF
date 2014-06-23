using UnityEngine;
using System.Collections;

public class PlatCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GameObject hero = GameObject.Find ("pre_hero");

		float x = Mathf.Floor((hero.transform.position.x+1.28f) / 2.56f);
		float y = Mathf.Floor((hero.transform.position.y+0.96f) / 1.92f);

		// Move the camera based on hero position.
		transform.position = new Vector3 (x*2.56f, y*1.92f, -5.0f);
	}
}

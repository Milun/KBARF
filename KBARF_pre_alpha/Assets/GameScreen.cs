using UnityEngine;
using System.Collections;

public class GameScreen : MonoBehaviour {

	[SerializeField] private Vector3 targetTransform = Vector3.zero;
	[SerializeField] private Vector3 targetRotation = Vector3.zero;

	private bool lookLock; 
	GameObject hero;

	// Use this for initialization
	void Awake ()
	{
		hero = GameObject.Find ("hero");
	}
	
	// Update is called once per frame
	void Update () {
	
		if (!lookLock && Input.GetKeyDown("space"))
		{
			if (Vector3.Distance(hero.transform.position, this.transform.position) < 3.0f)
			{
				hero.GetComponent<MouseLook>().lookLock = true;
				lookLock = true;
			}
		}

		if (lookLock)
		{
			hero.transform.position += (targetTransform - hero.transform.position) / 20.0f;
			hero.transform.LookAt(this.transform.position, Vector3.up);
		}
	}
}
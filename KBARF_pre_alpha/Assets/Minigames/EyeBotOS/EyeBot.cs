using UnityEngine;
using System.Collections;

[RequireComponent( typeof(ClickObj))]
public class EyeBot : MonoBehaviour {

	public GameObject particle;

	private ClickObj clickObj;

	void Awake ()
	{
		clickObj = GetComponent<ClickObj>();
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
	
		if (clickObj.Pressed)
		{
			// Get a vector from the center to the ray hit.
			Vector3 getHit = clickObj.Hit.point - transform.position;

			Debug.DrawRay(transform.position, getHit);

			getHit.Normalize();
			getHit *= 3.0f;

			// Rotate it based on the eyes rotation.

			// Get
			/*
			Quaternion x = Quaternion.AngleAxis(transform.eulerAngles.x, Vector3.right);
			Quaternion y = Quaternion.AngleAxis(transform.eulerAngles.y, -Vector3.up);
			Quaternion z = Quaternion.AngleAxis(transform.eulerAngles.z, Vector3.forward);

			getHit = x * y * z * getHit;

			*/
			Instantiate(particle, new Vector3(-getHit.x, getHit.y, -0.1f), Quaternion.identity);
		}

	}
}

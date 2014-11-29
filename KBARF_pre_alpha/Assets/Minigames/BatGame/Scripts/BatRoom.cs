using UnityEngine;
using System.Collections;

public class BatRoom : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		transform.position += Vector3.left * Time.deltaTime*20.0f;

		if (transform.position.x < -256.0f)
		{
			transform.position += Vector3.right*640.0f;
		}
	
	}
}

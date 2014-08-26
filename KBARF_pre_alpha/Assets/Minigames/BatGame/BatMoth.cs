using UnityEngine;
using System.Collections;

public class BatMoth : MonoBehaviour {

	// Use this for initialization
	void Awake ()
	{
	
	}

	public void Die()
	{
		Destroy (this.gameObject);
	}

	// Update is called once per frame
	void Update ()
	{
		this.transform.position += Vector3.right * 0.05f;
	}
}

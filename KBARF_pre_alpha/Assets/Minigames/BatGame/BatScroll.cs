using UnityEngine;
using System.Collections;

public class BatScroll : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position += Vector3.left * 0.15f;
	}
}

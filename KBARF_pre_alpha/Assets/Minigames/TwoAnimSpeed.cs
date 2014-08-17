using UnityEngine;
using System.Collections;

public class TwoAnimSpeed : MonoBehaviour {

	[SerializeField] private float animSpeed = 0.1f;

	// Use this for initialization
	void Start () {
		GetComponent<Animator>().speed = animSpeed;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

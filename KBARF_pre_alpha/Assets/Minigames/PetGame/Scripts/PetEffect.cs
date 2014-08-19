﻿using UnityEngine;
using System.Collections;

public class PetEffect : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.renderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.Find("pre_petHero").GetComponent<PetHero>().Joy)
		{
			this.renderer.enabled = true;
		}
	}
}

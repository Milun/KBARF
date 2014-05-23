﻿using UnityEngine;
using System.Collections;

public class Global : MonoBehaviour {
	
	[SerializeField] private float 			frameDelay = 0.1f;
	[SerializeField] private float 			pixelSize = 0.05f;

	private float							frameTime = 0.0f;
	private bool 							frameActive = false;

	// Use this for initialization
	void Start () {
	
	}

	public bool Frame()
	{
		return frameActive;
	}

	// Update is called once per frame
	void Update ()
	{
		if (frameTime < 0.0f)
		{
			frameActive = true;
			frameTime = frameDelay;
		}
		else
		{
			frameActive = false;
		}

		frameTime -= Time.deltaTime;
	}

	public float PIXEL_SIZE
	{
		get
		{
			return pixelSize;
		}
	}
}

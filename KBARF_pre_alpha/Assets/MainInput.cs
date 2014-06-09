using UnityEngine;
using System.Collections;

public class MainInput : MonoBehaviour {

	public bool HoldMiniUp()
	{
		return Input.GetKey("up");
	}

	public bool HoldMiniDown()
	{
		return Input.GetKey("down");	
	}

	public bool HoldMiniLeft()
	{
		return Input.GetKey("left");
	}

	public bool HoldMiniRight()
	{
		return Input.GetKey("right");
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

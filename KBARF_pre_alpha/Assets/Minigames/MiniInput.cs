using UnityEngine;
using System.Collections;

public class MiniInput : MonoBehaviour {

	MainInput input = null;

	public void SetInput(MainInput pass)
	{
		input = pass;
	}

	public void FreeInput()
	{
		input = null;
	}

	public bool HoldUp()
	{
		if (input == null) return false;
		return input.HoldMiniUp();
	}
	
	public bool HoldDown()
	{
		if (input == null) return false;
		return input.HoldMiniDown();
	}
	
	public bool HoldLeft()
	{
		if (input == null) return false;
		return input.HoldMiniLeft();
	}
	
	public bool HoldRight()
	{
		if (input == null) return false;
		return input.HoldMiniRight();
	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

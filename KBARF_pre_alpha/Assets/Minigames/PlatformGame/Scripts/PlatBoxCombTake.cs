using UnityEngine;
using System.Collections;

public class PlatBoxCombTake : PlatBox {

	// Use this for initialization
	protected override void Awake ()
	{
		base.Awake ();
	}

	public void Update()
	{
		// Only update collision position if you've moved.
		if (pCommon && pCommon.Vel != Vector2.zero)
		{
			UpdateBox ();
		}
	}

	public bool CheckCol()
	{
		return pColManager.CheckCombCol(this);
	}
}

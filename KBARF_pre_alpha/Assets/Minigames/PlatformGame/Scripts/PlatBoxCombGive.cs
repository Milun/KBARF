using UnityEngine;
using System.Collections;

public class PlatBoxCombGive : PlatBox {

	public enum Type
	{
		NORMAL,
		KNOCKBACK,
	};

	[SerializeField] private Type 	type = Type.NORMAL;


	// Use this for initialization
	protected override void Awake ()
	{
		base.Awake ();

		// If the pBound is never updated again, the manager will think there's always a collision here.
		pColManager.AddCombCol (this);
	}

	public void Update()
	{
		// Only check collisions if the object is moving.
		// If the object is still, such as a wall, it won't have PlatformCommon at all, so Update() won't execute.
		if (GetComponent<TwoCommon>() == null ||
		    GetComponent<TwoCommon>().Vel == Vector2.zero) return;

		// Move the bound to be relative to your (real) position.
		UpdateBox ();
	}

	void OnDestroy()
	{
		pColManager.DestroyCombCol (this);
	}
}

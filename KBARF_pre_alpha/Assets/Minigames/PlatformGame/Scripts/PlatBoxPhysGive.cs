using UnityEngine;
using System.Collections;

public class PlatBoxPhysGive : PlatBox {

	[SerializeField] private bool solid = true;

	public enum Type
	{
		NORMAL,
		ICE,
		BOUNCE
	};

	[SerializeField] private Type 	type = Type.NORMAL;


	// Use this for initialization
	protected override void Awake ()
	{
		base.Awake ();

		// If the pBound is never updated again, the manager will think there's always a collision here.
		pColManager.AddPhysCol (this);
	}

	public void Update()
	{
		// Only check collisions if the object is moving.
		// If the object is still, such as a wall, it won't have PlatformCommon at all, so Update() won't execute.
		if (GetComponent<PlatCommon>() == null) return;

		// Move the bound to be relative to your (real) position.
		UpdateBox ();
	}

	void OnDestroy()
	{
		pColManager.DestroyPhysCol (this);
	}

	public bool Solid
	{
		get
		{
			return solid;
		}
	}
}

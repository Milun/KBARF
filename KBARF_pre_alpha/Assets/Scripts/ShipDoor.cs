using UnityEngine;
using System.Collections;

[RequireComponent( typeof(ClickObj))]
public class ShipDoor : MonoBehaviour {

	public bool locked = false;

	private ClickObj clickObj;

	void Awake ()
	{
		clickObj = GetComponent<ClickObj>();
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
	
		if (clickObj.Pressed)
		{
			if (!locked)
				StatMain.GetAnimation(transform).Play ("shipdoor_anim_open");
			else
				StatMain.GetAnimation(transform).Play ("shipdoor_anim_locked");
		}

	}
}

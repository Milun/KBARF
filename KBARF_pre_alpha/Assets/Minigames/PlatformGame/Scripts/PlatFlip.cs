using UnityEngine;
using System.Collections;

[RequireComponent( typeof(PlatCommon) )]
public class PlatFlip : MonoBehaviour {

	private PlatCommon 		pCommon;

	[SerializeField] private Vector2 	spriteSize = Vector2.zero;
	[SerializeField] private bool		flipX	   = false;

	void Awake ()
	{
		pCommon 	= GetComponent<PlatCommon> ();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (flipX && pCommon.XSpeed != 0.0f)
		{
			if (pCommon.XSpeed < 0.0f)
			{
				pCommon.Scale = new Vector3(-1.0f, 1.0f, 0.0f);
				pCommon.PosOffsetX = spriteSize.x;
			}
			else
			{
				pCommon.Scale = new Vector3(1.0f, 1.0f, 0.0f);
				pCommon.PosOffsetX = 0.0f;
			}
		}
	}
}

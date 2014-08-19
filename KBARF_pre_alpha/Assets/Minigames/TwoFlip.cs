using UnityEngine;
using System.Collections;

[RequireComponent( typeof(TwoCommon) )]
public class TwoFlip : MonoBehaviour {

	private TwoCommon 		pCommon;

	private int flip = 1;

	[SerializeField] private Vector2 	spriteSize = Vector2.zero;
	[SerializeField] private bool		flipX	   = false;

	void Awake ()
	{
		pCommon 	= GetComponent<TwoCommon> ();
		//pShadow 	= GetComponent<PetShadow> ();
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

				flip = -1;
			}
			else
			{
				pCommon.Scale = new Vector3(1.0f, 1.0f, 0.0f);
				pCommon.PosOffsetX = 0.0f;

				flip = 1;
			}
		}
	}

	public int Flip
	{
		get
		{
			return flip;
		}
	}
}

using UnityEngine;
using System.Collections;

[RequireComponent( typeof(TwoCommonBasic) )]
public class TwoFlip : MonoBehaviour {

	private TwoCommonBasic 		pCommonBasic;
	private TwoCommon 			pCommon;

	private int flip = 1;

	[SerializeField] private Vector2 	spriteSize = Vector2.zero;
	[SerializeField] private bool		flipX	   = false;
	[SerializeField] private bool		flipXStart = false;

	void Awake ()
	{
		pCommonBasic 	= GetComponent<TwoCommonBasic> ();
		pCommon 		= GetComponent<TwoCommon> ();

		if (flipXStart)
		{
			flip = -1;
		}
		else
		{
			flip = 1;
		}

		UpdateScale ();
	}
	
	// Update is called once per frame
	void Update () {

		if (flipX)
		{
			if (pCommon && pCommon.GetType() == typeof(TwoCommon) && pCommon.XSpeed != 0.0f)
			{
				if (pCommon.XSpeed < 0.0f)
				{
					flip = -1;
				}
				else if (pCommon.XSpeed > 0.0f)
				{
					flip = 1;
				}
			}

			UpdateScale();
		}
	}

	private void UpdateScale()
	{
		if (flip < 0)
		{
			pCommonBasic.Scale = new Vector3(-1.0f, 1.0f, 0.0f);
			pCommonBasic.PosOffsetX = spriteSize.x;
		}
		else
		{
			pCommonBasic.Scale = new Vector3(1.0f, 1.0f, 0.0f);
			pCommonBasic.PosOffsetX = 0.0f;
		}
	}

	public int Flip
	{
		get
		{
			return flip;
		}

		set
		{
			flip = value;
		}
	}
}

using UnityEngine;
using System.Collections;

[RequireComponent( typeof(TwoCommonBasic) )]
public class TwoFlip : MonoBehaviour {

	private TwoCommonBasic 		pCommonBasic;
	private TwoCommon 			pCommon;

	private TwoCol[]			tCols;

	private int flip = 1;

	[SerializeField] private bool		flipXManual = false;

	void Awake ()
	{
		pCommonBasic 	= GetComponent<TwoCommonBasic> ();
		pCommon 		= GetComponent<TwoCommon> ();
		tCols			= GetComponents<TwoCol>();

		if (flipXManual)
		{
			flip = -1;
			UpdateScale ();
		}
	}

	public bool FlipXManual
	{
		get
		{
			return flipXManual;
		}
	}

	// Update is called once per frame
	void Update () {

		if (!flipXManual && pCommon && pCommon.GetType() == typeof(TwoCommon) && pCommon.XSpeed != 0.0f)
		{
			if (pCommon.XSpeed < 0.0f && flip != -1)
			{
				flip = -1;
				UpdateScale();
			}
			else if (pCommon.XSpeed > 0.0f && flip != 1)
			{
				flip = 1;
				UpdateScale();
			}
		}
	}

	private void UpdateScale()
	{
		if (flip < 0)
		{
			pCommonBasic.Scale = new Vector3(-1.0f, 1.0f, 0.0f);
		}
		else
		{
			pCommonBasic.Scale = new Vector3(1.0f, 1.0f, 0.0f);
		}

		if (tCols.Length > 0)
		{
			foreach (TwoCol e in tCols)
			{
				e.Flip();
			}
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

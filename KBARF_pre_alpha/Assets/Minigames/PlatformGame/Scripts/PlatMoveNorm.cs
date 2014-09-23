using UnityEngine;
using System.Collections;

[RequireComponent( typeof(TwoCommon))]
public class PlatMoveNorm : MonoBehaviour {

	private TwoCommon pCommon;

	[SerializeField] private Vector2 startMove = Vector2.zero;

	void Awake ()
	{
		pCommon = GetComponent<TwoCommon> ();
		pCommon.Vel = startMove;
	}

	// Use this for initialization
	public Vector2 StartMove
	{
		get
		{
			return startMove;
		}
	}
}

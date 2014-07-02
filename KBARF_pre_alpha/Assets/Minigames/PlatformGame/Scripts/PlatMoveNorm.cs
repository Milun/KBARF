using UnityEngine;
using System.Collections;

[RequireComponent( typeof(PlatCommon))]
public class PlatMoveNorm : MonoBehaviour {

	private PlatCommon pCommon;

	[SerializeField] private Vector2 startMove = Vector2.zero;

	void Awake ()
	{
		pCommon = GetComponent<PlatCommon> ();
		pCommon.Vel = startMove;
	}

	// Use this for initialization
	void Start () {

	}
}

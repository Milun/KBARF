using UnityEngine;
using System.Collections;

public class PlatBounce : MonoBehaviour {

	private PlatCommon 		pCommon;
	private PlatCollisions 	pCollisions;

	void Awake ()
	{
		pCommon = GetComponent<PlatCommon> ();
		pCollisions = GetComponent<PlatCollisions> ();
	}


	void Update()
	{

	}
}

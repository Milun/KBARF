using UnityEngine;
using System.Collections;

[RequireComponent( typeof(PlatGravity) )]
[RequireComponent( typeof(TwoColPhysics) )]
public class PlatEnemyHop : MonoBehaviour {

	private TwoCommon 		pCommon;
	private TwoColPhysics 	tColPhys;
	private PlatGravity		pGrav;

	[SerializeField] private float 		jumpHeight = 2.0f;

	void Awake ()
	{
		pCommon 	= GetComponent<TwoCommon> ();
		tColPhys 	= GetComponent<TwoColPhysics> ();
		pGrav		= GetComponent<PlatGravity> ();
	}

	void Update()
	{
		if (pGrav.OnGround) 
		{
			pCommon.YSpeed = jumpHeight;
		}

		if ((tColPhys.Move.x < 0.0f && pCommon.XSpeed > 0.0f) ||
		    (tColPhys.Move.x > 0.0f && pCommon.XSpeed < 0.0f))
			pCommon.XSpeed = -pCommon.XSpeed;
	}
}
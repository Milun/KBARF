using UnityEngine;
using System.Collections;

public class PlatBox : MonoBehaviour {

	public Vector2 oBL = Vector2.zero;
	public Vector2 oTR = Vector2.zero;

	private Vector2 pBL = Vector2.zero;	// Example: [2, 1]. Is the Bottom Left point.
	private Vector2 pTR = Vector2.zero; 	// Example: [8, 8]

	protected PlatCollisionManager pColManager;	// Reference to the object which handles collisions.
	protected PlatCommon 		   pCommon;

	// Use this for initialization
	protected virtual void Awake ()
	{
		pColManager = GameObject.Find("CollisionManager").GetComponent<PlatCollisionManager> ();
		UpdateBox ();

		pCommon = GetComponent<PlatCommon> ();

		if (pBL.x > pTR.x ||
		    pBL.y > pTR.y)
		{
			print ("ERROR! HITBOX POINTS INVERTED!");
		}
	}

	public bool IsEqual(PlatBox other)
	{
		return (other.pBL == pBL && other.pTR == pTR);
	}

	protected void UpdateBox()
	{
		if (!pCommon)
		{
			pBL = (Vector2)this.transform.position + oBL;
			pTR = (Vector2)this.transform.position + oTR;
		}
		else
		{
			pBL = pCommon.Pos + oBL;
			pTR = pCommon.Pos + oTR;
		}
	}

	public Vector2 PBL
	{
		get
		{
			return pBL;
		}
	}

	public Vector2 PTR
	{
		get
		{
			return pTR;
		}
	}

	public PlatCommon PCommon
	{
		get
		{
			return pCommon;
		}
	}
}

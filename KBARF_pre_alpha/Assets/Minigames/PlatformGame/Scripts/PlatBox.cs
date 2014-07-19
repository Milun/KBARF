using UnityEngine;
using System.Collections;

public class PlatBox : MonoBehaviour {

	public Vector2 oBL = Vector2.zero;
	public Vector2 oTR = Vector2.zero;

	private Vector2 pBL = Vector2.zero;	// Example: [2, 1]. Is the Bottom Left point.
	private Vector2 pTR = Vector2.zero; 	// Example: [8, 8]

	protected PlatCollisionManager pColManager;	// Reference to the object which handles collisions.

	// Use this for initialization
	protected virtual void Awake ()
	{
		pColManager = GameObject.Find("CollisionManager").GetComponent<PlatCollisionManager> ();
		UpdateBox ();
	}

	public bool IsEqual(PlatBox other)
	{
		return (other.pBL == pBL && other.pTR == pTR);
	}

	protected void UpdateBox()
	{
		pBL = (Vector2)this.transform.position + oBL;
		pTR = (Vector2)this.transform.position + oTR;
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
}

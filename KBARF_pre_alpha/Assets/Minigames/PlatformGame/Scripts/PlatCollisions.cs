using UnityEngine;
using System.Collections;

public class PlatCollisions : MonoBehaviour {

	private PlatCommon pCommon;

	[SerializeField] private Vector2 offset = Vector2.zero;	// Example: [2, -1]
	[SerializeField] private Vector2 bounds = Vector2.zero; // Example: [8, 8]
	private PlatBound bb;
	private Vector2 p0;
	private Vector2 p1;

	private GameObject platCollisionManager;
	private PlatCollisionManager pcm;

	// Use this for initialization
	void Awake ()
	{
		pCommon = GetComponent<PlatCommon> ();
		pcm = GameObject.Find("CollisionManager").GetComponent<PlatCollisionManager> ();

		bb = this.gameObject.AddComponent<PlatBound>();
		bb.p0 = offset;
		bb.p1 = new Vector2(offset.x + bounds.x, offset.y - bounds.x);

		p0 = offset;
		p1 = new Vector2(offset.x + bounds.x, offset.y - bounds.x);

		bb.p0 += new Vector2(transform.position.x, transform.position.y);
		bb.p1 += new Vector2(transform.position.x, transform.position.y);

		pcm.AddCol (bb);
	}

	void Create()
	{

	}	

	void Update()
	{

	}

	public void CheckCollisions()
	{
		bb.p0 = p0 + pCommon.Pos;
		bb.p1 = p1 + pCommon.Pos;

		if (pCommon.XSpeed > 0.0f) bb.p1 += Vector2.right * pCommon.XSpeed;
		if (pCommon.XSpeed < 0.0f) bb.p0 += Vector2.right * pCommon.XSpeed;
		if (pCommon.YSpeed > 0.0f) bb.p0 += Vector2.up * pCommon.XSpeed;
		if (pCommon.YSpeed < 0.0f) bb.p1 += Vector2.up * pCommon.XSpeed;

		bb.draw ();

		Vector2 test = pcm.CheckCol (bb, pCommon.Vel);

		if (test.x != 0.0f)
						pCommon.X = test.x;
		if (test.y != 0.0f)
						pCommon.Y = test.y;
	
	}
}

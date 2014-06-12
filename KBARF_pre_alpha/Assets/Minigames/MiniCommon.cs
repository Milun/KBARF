using UnityEngine;
using System.Collections;

public class MiniCommon : MonoBehaviour {

	// The players position on the screen.
	protected Vector2 pos = Vector2.zero;

	public Global2D global;
	public MiniInput input;

	// Use this for initialization
	void Awake ()
	{
		// Establish a link to global statistics.
		global = StatMini.GetMiniContainer(transform).GetComponent<Global2D> ();
		input =	 StatMini.GetMiniContainer(transform).GetComponent<MiniInput> ();

		// Be sure our custom (not on the grid) location is recorded at the start.
		pos = new Vector2(transform.position.x, transform.position.y);
	}

	public void Move(Vector2 v)
	{
		pos += v;
	}

	// Update is called once per frame
	void Update ()
	{
		// Make everything move at the exact same intervals.
		if (!global.Frame()) return;

		// Snap to the fake "pixel grid".
		transform.position = new Vector3 (Mathf.Floor (pos.x)*global.PIXEL_SIZE,
		                                  Mathf.Floor (pos.y)*global.PIXEL_SIZE,
		                                  transform.position.z);
	}
}

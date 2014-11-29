using UnityEngine;
using System.Collections;

public class BatMoth : MonoBehaviour {

	private TwoColLine[] col;
	private TwoColCircle colCirc;
	private float rot = 90.0f;
	private float rotWander = 0.0f;

	private int deadEndTurn = 0;

	// Use this for initialization
	void Awake ()
	{
		col = GetComponents<TwoColLine> ();
		colCirc = GetComponent<TwoColCircle> ();
	}

	public void Die()
	{
		Destroy (this.gameObject);
	}

	// Update is called once per frame
	void Update ()
	{
		TwoCol colC = colCirc.ColManager.IsCol (colCirc, TwoCol.ColType.COMBAT_DEF);
		if (colC)
		{
			BatHero hero = colC.GetComponent<BatHero>();
			if (hero)
			{
				hero.EatMoth();
				Die ();
				return;
			}
		}

		//boop.
		// Note to self: Rethink the way the hitboxes were handles by the bat (maybe make the MOTHS the ones that collide with it.
		// Seems more efficient.
		//
		// Also, instead of the random the moths currently have, maybe limit the rotation between two angles (so that the moths always go from one end of the screen to the other).

		//rot += 1.0f;

		/*if (rot >= 256.0f)
		{
			rot -= 256.0f;
		}
		if (rot < 0.0f)
		{
			rot += 256.0f;
		}*/

		if (col.Length > 0)
		{
			Vector2 wiskR = new Vector2 (Mathf.Cos (Mathf.Deg2Rad*(rot-40.0f))*20.0f,
			                             Mathf.Sin (Mathf.Deg2Rad*(rot-40.0f))*20.0f);
			Vector2 wiskL = new Vector2 (Mathf.Cos (Mathf.Deg2Rad*(rot+40.0f))*20.0f,
			                             Mathf.Sin (Mathf.Deg2Rad*(rot+40.0f))*20.0f);
			Vector2 wiskM = new Vector2 (Mathf.Cos (Mathf.Deg2Rad*(rot))*15.0f,
			                             Mathf.Sin (Mathf.Deg2Rad*(rot))*15.0f);
			col[0].P2 = wiskR;
			col[1].P2 = wiskL;
			col[2].P2 = wiskM;

			Vector2 leftMove = Vector2.zero, rightMove = Vector2.zero, midMove = Vector2.zero;

			foreach (TwoColManager.Col e in col[0].ColManager.CheckCol (col[0]))
			{
				rightMove += e.move;
			}
			foreach (TwoColManager.Col e in col[1].ColManager.CheckCol (col[1]))
			{
				leftMove += e.move;
			}
			foreach (TwoColManager.Col e in col[2].ColManager.CheckCol (col[2]))
			{
				midMove += e.move;
			}

			if (midMove == Vector2.zero)
			{
				deadEndTurn = 0;
			}

			if (deadEndTurn == 0)
			{
				deadEndTurn = Random.Range(0, 2);
				deadEndTurn = -1 + deadEndTurn*2;
			}

			rot += midMove.magnitude * 2.0f * (float)deadEndTurn;
			rot -= leftMove.magnitude * 0.2f;
			rot += rightMove.magnitude * 0.2f;

			rotWander += Random.Range(-0.1f, 0.1f);
			Mathf.Clamp(rotWander, -0.25f, 0.25f);

			rot += rotWander;
		}

		this.transform.position += new Vector3( Mathf.Cos (Mathf.Deg2Rad*(rot)) * Time.deltaTime * 16.0f,
		                                     	Mathf.Sin (Mathf.Deg2Rad*(rot)) * Time.deltaTime * 16.0f,
		                                        0.0f);
	}
}

using UnityEngine;
using System.Collections;

public class BatMoth : MonoBehaviour {

	private TwoColLine[] col;
	private float rot = 90.0f;

	// Use this for initialization
	void Awake ()
	{
		col = GetComponents<TwoColLine> ();
	}

	public void Die()
	{
		Destroy (this.gameObject);
	}

	// Update is called once per frame
	void Update ()
	{
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
				print ("RIGHT!");
			}
			foreach (TwoColManager.Col e in col[1].ColManager.CheckCol (col[1]))
			{
				leftMove += e.move;
				print ("LEFT!");
			}
			foreach (TwoColManager.Col e in col[2].ColManager.CheckCol (col[2]))
			{
				midMove += e.move;
			}

			if (leftMove != Vector2.zero || rightMove != Vector2.zero /*|| midMove != Vector2.zero*/)
			{
				if (midMove != Vector2.zero)
				{
					rot += midMove.magnitude * 2.5f;
				}
				else
				{
					//if (leftMove.magnitude > rightMove.magnitude)
					{
						rot -= leftMove.magnitude * 0.5f;
					}
					//else
					{
						rot += rightMove.magnitude * 0.5f;
					}
				}
			}
		}

		this.transform.position += new Vector3( Mathf.Cos (Mathf.Deg2Rad*(rot)) * Time.deltaTime * 16.0f,
		                                     	Mathf.Sin (Mathf.Deg2Rad*(rot)) * Time.deltaTime * 16.0f,
		                                        0.0f);
	}
}

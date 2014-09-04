using UnityEngine;
using System.Collections.Generic;
using Vectrosity;

public class BatSpike : MonoBehaviour {

	private Vector2 p1 = Vector2.zero;
	private Vector2 p2 = Vector2.zero;
	private Vector2 p = Vector2.zero;

	private BatController controller;
	private Transform myTransform;

	private TwoColLine[] col;
	private TwoColSquare colSquare;
	VectorLine[] vl;

	private float speed = 0.0f;

	// Use this for initialization
	void Awake () {
		vl = new VectorLine[2];
		col = GetComponents<TwoColLine> ();
		colSquare = GetComponent<TwoColSquare> ();
	}

	void Start () {
	
		controller = GameObject.Find("cam_main").GetComponent<BatController>();

		Destroy (this.GetComponent<SpriteRenderer> ());

		Vector3 multiply = this.transform.localScale;
		myTransform = this.transform;

		p = new Vector2 (0.0f, 0.0f);
		p1 = new Vector2 ( multiply.x * -0.5f, multiply.y);
		p2 = new Vector2 ( multiply.x *  0.5f, multiply.y);
		
		vl[0] = controller.CreateLine(myTransform.position + (Vector3)(p), myTransform.position + (Vector3)(p1));
		vl[1] = controller.CreateLine(myTransform.position + (Vector3)(p), myTransform.position + (Vector3)(p2));
		
		this.transform.localScale = Vector3.one;

	}
	
	// Update is called once per frame
	void Update () {

		List<TwoColManager.Col> other = col [0].ColManager.CheckCol (col [0]);
		Vector2 mask = Vector2.zero;
		foreach (TwoColManager.Col e in other)
		{
			if (e.move.magnitude > mask.magnitude)
			{
				mask = e.move;
			}
		}

		float magnitude = p1.magnitude - mask.magnitude;
		if (magnitude < 0.0f)
		{
			mask = p1;
		}

		if ((p1+mask).magnitude > p1.magnitude)
		{
			mask = Vector2.zero;
		}


		Vector3[] v1 = new Vector3[2];
		v1 [0] = myTransform.position + (Vector3)p;
		v1 [1] = myTransform.position + (Vector3)(p1+mask);

		vl [0].Resize (v1);




		other = col [1].ColManager.CheckCol (col [1]);
		mask = Vector2.zero;
		foreach (TwoColManager.Col e in other)
		{
			if (e.move.magnitude > mask.magnitude)
			{
				mask = e.move;
			}
		}
		
		magnitude = p2.magnitude - mask.magnitude;
		if (magnitude < 0.0f)
		{
			mask = p2;
		}
		
		if ((p2+mask).magnitude > p2.magnitude)
		{
			mask = Vector2.zero;
		}

		Vector3[] v2 = new Vector3[2];
		v2 [0] = myTransform.position + (Vector3)p;
		v2 [1] = myTransform.position + (Vector3)(p2+mask);

		vl [1].Resize (v2);


		if (speed == 0.0f)
		{
			List<TwoColManager.Col> otherSquare = colSquare.ColManager.CheckCol (colSquare);
			print (otherSquare.Count);
			foreach (TwoColManager.Col e in otherSquare)
			{
				if (e.move != Vector2.zero && e.col.GetComponent<BatHero>())
				{
					speed += Time.deltaTime;
					break;
				}
			}
		}
		else
		{
			myTransform.position += Vector3.down*speed;
			speed += Time.deltaTime;
		}
	}
}

using UnityEngine;
using System.Collections.Generic;
using Vectrosity;

public class BatSpike : MonoBehaviour {

	private Vector2 p1 = Vector2.zero;
	private Vector2 p2 = Vector2.zero;
	private Vector2 p = Vector2.zero;

	private Vector2 p1Mask = Vector2.zero;
	private Vector2 p2Mask = Vector2.zero;

	private BatController controller;
	private Transform myTransform;
	
	private TwoColLine[] col;
	private TwoColSquare colSquare;
	VectorLine[] vl;

	private bool die = false;

	[SerializeField] private float speedAdd = 3.0f;
	[SerializeField] private AudioClip ac;
	private float speed = 0.0f;
	private bool  freefall = false;


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

		p = new Vector2 (0.0f, -multiply.y);
		p1 = new Vector2 ( multiply.x * -0.5f, 0.0f);
		p2 = new Vector2 ( multiply.x *  0.5f, 0.0f);
		
		vl[0] = controller.CreateLine(Vector2.zero, Vector2.zero);
		vl[1] = controller.CreateLine(Vector2.zero, Vector2.zero);

		this.transform.localScale = Vector3.one;

	}

	private void UpdateMask(TwoColLine colLine, Vector2 point, ref Vector2 mask)
	{
		List<TwoColManager.Col> other = colLine.ColManager.CheckCol (colLine);
		mask = Vector2.zero;

		foreach (TwoColManager.Col e in other)
		{
			if (e.col.HasType(TwoCol.ColType.COMBAT_DEF))
			{
			BatHero hero = e.col.GetComponent<BatHero>();
				if (hero)
				{
					hero.Die();
					die = true;
					if (ac) AudioSource.PlayClipAtPoint(ac, transform.position);
					return;
				}
			}

			if (e.col.HasType(TwoCol.ColType.PHYSICS_DEF) && e.move.magnitude > mask.magnitude)
			{
				mask = e.move;
			}
		}
	}

	// Update is called once per frame
	void LateUpdate () {

		if (die)
		{
			VectorLine.Destroy(ref vl[0]);
			VectorLine.Destroy(ref vl[1]);
			Destroy (this.gameObject);

			return;
		}

		UpdateMask(col[0], p1, ref p1Mask);
		UpdateMask(col[1], p2, ref p2Mask);

		// When it's idle
		if (speed == 0.0f)
		{
			if (p1Mask == Vector2.zero && p2Mask == Vector2.zero)
			{
				print ("ERROR! A spike is not properly embedded into a wall!"); 
			}

			// Start falling.
			if (speedAdd != 0.0f)
			{
				if (colSquare.ColManager.IsCol (colSquare, TwoCol.ColType.T1_DEF))
				{
					speed += Time.deltaTime*speedAdd;
					return;
				}
			}
		}
		else
		// When it's falling
		{
			UpdateMask(col[0], p1, ref p1Mask);
			UpdateMask(col[1], p2, ref p2Mask);

			// Check if the spike is completely unmasked.
			if (!freefall &&
			    p1Mask == Vector2.zero &&
			    p2Mask == Vector2.zero)
			{
				freefall = true;
			}
			// If so, then the next time it hits the ground, destroy it!
			else if (freefall &&
			         (
						p1Mask != Vector2.zero ||
						p2Mask != Vector2.zero
					 )
			        )
			{
				die = true;
				return;
			}


			myTransform.position += Vector3.down*speed;
			speed += Time.deltaTime*speedAdd;
		}

		// REDRAW!
		controller.ResizeLine (vl [0],
		                       myTransform.position + (Vector3)p,
		                       myTransform.position + (Vector3)(p1 + p1Mask));
		
		controller.ResizeLine (vl [1],
		                       myTransform.position + (Vector3)p,
		                       myTransform.position + (Vector3)(p2 + p2Mask));
	}
}

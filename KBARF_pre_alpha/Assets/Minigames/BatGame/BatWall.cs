using UnityEngine;
using System.Collections;
using Vectrosity;

public class BatWall : MonoBehaviour {

	[SerializeField] private Vector2 p1 = Vector2.zero;
	[SerializeField] private Vector2 p2 = Vector2.zero;
	[SerializeField] private Material mat;
	[SerializeField] private Texture2D[] tex;

	private float width = 2.0f;

	VectorLine vl;

	// Use this for initialization
	void Awake () {

		Destroy (this.GetComponent<SpriteRenderer> ());

		Vector3 multiply = this.transform.localScale;
		p1 = new Vector2(p1.x * multiply.x, p1.y * multiply.y);
		p2 = new Vector2(p2.x * multiply.x, p2.y * multiply.y);

		vl = VectorLine.SetLine3D (	Color.white,
		                      		this.transform.position + (Vector3)p1,
		                      		this.transform.position + (Vector3)p2);

		vl.name = "BatWall";
		vl.lineWidth = width;
		VectorLine.SetEndCap ("BatWall", EndCap.Mirror, mat, tex);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

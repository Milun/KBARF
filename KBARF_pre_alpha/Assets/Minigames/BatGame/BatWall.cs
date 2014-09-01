using UnityEngine;
using System.Collections;
using Vectrosity;

public class BatWall : MonoBehaviour {

	[SerializeField] private Vector2 p1 = Vector2.zero;
	[SerializeField] private Vector2 p2 = Vector2.zero;

	private BatController controller;

	VectorLine vl;

	// Use this for initialization
	void Start () {

		controller = GameObject.Find("cam_main").GetComponent<BatController>();

		Destroy (this.GetComponent<SpriteRenderer> ());

		Vector3 multiply = this.transform.localScale;
		p1 = new Vector2(p1.x * multiply.x, p1.y * multiply.y);
		p2 = new Vector2(p2.x * multiply.x, p2.y * multiply.y);

		vl = controller.CreateLine(this.transform.position + (Vector3)p1,
		                      	   this.transform.position + (Vector3)p2);

		this.transform.localScale = Vector3.one;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

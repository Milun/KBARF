using UnityEngine;
using System.Collections;
using Vectrosity;

[System.Serializable]
public class BatVector {

	private Vector2 point1 = Vector2.zero;
	private Vector2 point2 = Vector2.zero;

	private Vector2 point1Original = Vector2.zero;
	private Vector2 point2Original = Vector2.zero;

	/*
	private float offsetMulti = 0.0f;
	private Vector2 offset = Vector2.zero;
	*/

	private VectorLine vl;

	private Vector2 pos = Vector2.zero;

	BatController controller;

	// Use this for initialization
	void Start () {
	
	}

	public void InitLine (BatController c, Vector2 p1, Vector2 p2) {

		controller = c;

		point1 = p1;
		point2 = p2;

		point1Original = p1;
		point2Original = p2;

		vl = controller.CreateLine(p1, p2);
	}

	public void Scale(float s) {
		point1 = point1Original*s;
		point2 = point2Original*s;
	}

	public void Show() {
		vl.active = true;
	}

	public void Hide() {
		vl.active = false;
	}

	public void Delete() {
		VectorLine.Destroy(ref vl);
	}

	public void Explode(Vector2 _pos) {

		/*float randAngle = Random.Range (0.0f, 3.1415f);

		offsetMulti = 0.1f;
		offset = new Vector2 (Mathf.Cos (randAngle), Mathf.Sin (randAngle));*/
	}

	public void Draw(Vector2 _pos) {
		if (vl == null) return;

		pos = _pos;
		controller.ResizeLine(vl, point1 + pos, point2 + pos);
	}
}

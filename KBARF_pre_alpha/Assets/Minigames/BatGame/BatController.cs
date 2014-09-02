using UnityEngine;
using System.Collections;
using Vectrosity;

public class BatController : MonoBehaviour {

	[SerializeField] private Material mat;
	[SerializeField] private Texture2D[] tex;

	[SerializeField] private float lineWidth = 30.0f;
	[SerializeField] private Material lineMat;

	// Use this for initialization
	void Awake () {
		VectorLine.SetEndCap ("Cap", EndCap.Mirror, mat, tex);
	}

	void Start() {
		VectorLine vl = new VectorLine("help", new Vector3[60], lineMat, lineWidth, LineType.Discrete, Joins.Weld);


		//CreateLine (new Vector3 (0.0f, 0.0f, 0.0f), new Vector3 (400.0f, 400.0f, 0.0f));

		vl.MakeText ("123456", new Vector3(10.0f, 310.0f, 0.0f), 15.0f);

		vl.endCap = "Cap";

		//vl.joins = Joins.Weld;

		vl.Draw ();

		//
	}

	public VectorLine CreateLine(Vector3 p1, Vector3 p2)
	{
		VectorLine vl = VectorLine.SetLine3D (Color.white, p1, p2);
			
		vl.lineWidth = lineWidth;
		vl.material = lineMat;
		vl.endCap = "Cap";

		return vl;
	}

	// Update is called once per frame
	void Update () {
	
	}
}

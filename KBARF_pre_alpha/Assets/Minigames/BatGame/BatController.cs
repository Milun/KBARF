using UnityEngine;
using System.Collections;
using Vectrosity;

public class BatController : MonoBehaviour {

	[SerializeField] private Material mat;
	[SerializeField] private Texture2D[] tex;

	// Use this for initialization
	void Awake () {
		VectorLine.SetEndCap ("Cap", EndCap.Mirror, mat, tex);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

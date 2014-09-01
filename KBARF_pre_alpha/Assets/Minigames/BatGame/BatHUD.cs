using UnityEngine;
using System.Collections;
using Vectrosity;

public class BatHUD : MonoBehaviour {

	[SerializeField] private Vector3 	barPos;
	[SerializeField] private float	 	width = 100.0f;
	[SerializeField] private Material	mat;

	private VectorLine[] barEnergy;

	// Use this for initialization
	void Start () {

		barEnergy = new VectorLine[9];

		for (int i = 0; i < barEnergy.Length; i++)
		{
			barEnergy[i] = VectorLine.SetLine3D (Color.white,
			                          barPos + Vector3.down*(float)i*2.4f,
			                          barPos + Vector3.right * width + Vector3.down*(float)i*2.4f);

			barEnergy[i].lineWidth = 30.0f;
			barEnergy[i].material = mat;
			barEnergy[i].endCap = "Cap";
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

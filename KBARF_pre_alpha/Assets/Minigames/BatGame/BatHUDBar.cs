using UnityEngine;
using System.Collections;
using Vectrosity;

public class BatHUDBar : MonoBehaviour {

	private Vector3 	 barPos;
	private VectorLine[] barEnergy;

	[SerializeField] private float spread = 2.3f;
	[SerializeField] private float width; 
	private float percent = 1.0f;

	private BatController controller; 

	// Use this for initialization
	void Start () {

		controller = GameObject.Find("cam_main").GetComponent<BatController>();

		barPos = transform.position;
		barEnergy = new VectorLine[2];

		for (int i = 0; i < barEnergy.Length; i++)
		{
			barEnergy[i] = controller.CreateLine(barPos + Vector3.down*(float)i*spread,
			                                     barPos + Vector3.right * width + Vector3.down*(float)i*spread);
		}
	}

	public void SetValue(float val)
	{
		if (val >= 0.0f)
		{
			if (val <= 0.01f)
			{
				percent = 0.001f;
			}
			else
			{
				percent = val;
			}

			for (int i = 0; i < barEnergy.Length; i++)
			{
				Vector3[] pass = new Vector3[2];
				pass[0] = barPos + Vector3.down*(float)i*spread;
				pass[1] = barPos + Vector3.right * width * percent + Vector3.down*(float)i*spread;

				barEnergy[i].Resize(pass);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

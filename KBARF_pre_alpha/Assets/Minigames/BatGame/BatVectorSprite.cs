using UnityEngine;
using System.Collections;
using Vectrosity;

[System.Serializable]
public class BatVectorSprite {

	private BatController controller;

	public Vector2[] line;
	private VectorLine[] vl;

	/*= {	new Vector2(50f, 15f),
								new Vector2(0f, 0f),
								new Vector2(51.5f, 10f),
							new Vector2(50f, 15f),
							new Vector2(40f, 12f),
							new Vector2(41.5f, 7f),
							new Vector2(30f, 9f),
							new Vector2(31.5f, 4f),
							new Vector2(20f, 6f),
							new Vector2(21.5f, 1f),
							new Vector2(10f, 10f),
							new Vector2(15f, 4.5f),
							new Vector2(10f, 10f),
							new Vector2(0f, 0f);
	};*/

	// Use this for initialization
	public void InitSprite () {
		controller = GameObject.Find("cam_main").GetComponent<BatController>();

		int vlLength = (int)(line.Length/2);

		Debug.Log(vlLength);

		vl = new VectorLine[vlLength];

		for (int i = 0; i < vlLength; i++)
		{
			vl[i] = controller.CreateLine(line[i*2], line[i*2+1]);
			Debug.Log("ASAS");
			if (vl[i] == null) Debug.Log("AASSASA");
		}
	}

	public void Draw(Vector2 pos) {
		if (vl == null || vl.Length == 0) return;

		for (int i = 0; i < vl.Length; i++)
		{
			controller.ResizeLine(vl[i], line[i*2] + pos, line[i*2+1] + pos);
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}

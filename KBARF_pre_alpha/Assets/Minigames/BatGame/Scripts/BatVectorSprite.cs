using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Vectrosity;
using System.Text;
using System.IO;

[System.Serializable]
public class BatVectorSprite {

	private BatController controller;

	[SerializeField] private string fileName;

	private List<Vector2> lines;
	private VectorLine[] vl;

	// Use this for initialization
	public void InitSprite () {
		controller = GameObject.Find("cam_main").GetComponent<BatController>();
		Load ();

		int vlLength = (int)(lines.Count/2);

		vl = new VectorLine[vlLength];

		for (int i = 0; i < vlLength; i++)
		{
			vl[i] = controller.CreateLine(lines[i*2], lines[i*2+1]);
		}


	}

	public void Draw(Vector2 pos) {
		if (vl == null || vl.Length == 0) return;

		for (int i = 0; i < vl.Length; i++)
		{
			controller.ResizeLine(vl[i], lines[i*2] + pos, lines[i*2+1] + pos);
		}
	}

	private bool Load()
	{
		lines = new List<Vector2> ();

		try
		{
			string 			line;
			TextAsset 		reader = Resources.Load(fileName) as TextAsset;

			string[] 		linesFile = reader.text.Split("\n"[0]);

			//using (reader)
			{
				foreach (string e in linesFile)
				{
					if (e != null)
					{
						string[] entries = e.Split(',');
						if (entries.Length == 2)
						{
							lines.Add(new Vector2(float.Parse(entries[0]), float.Parse(entries[1])));
						}
						else
						{
							continue;
						}
					}
				}
				//while (line != null);

				Resources.UnloadUnusedAssets();
				return true;
			}
		}
		
		// If anything broke in the try block, we throw an exception with information
		// on what didn't work
		catch (IOException e)
		{
			Debug.Log(e.Message);
			return false;
		}
	}


	// Update is called once per frame
	void Update () {
	
	}
}

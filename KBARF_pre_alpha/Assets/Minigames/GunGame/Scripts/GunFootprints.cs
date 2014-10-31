using UnityEngine;
using System.Collections;

public class GunFootprints : MonoBehaviour {

	[SerializeField] private int footMax = 50;
	[SerializeField] private float footSize = 10.0f;
	[SerializeField] private Material mat;
	private int footCount = 0;

	private Vector3[] vertices;
	private Vector2[] UVs;
	private int[]	  tris;

	private Mesh mesh;

	void Awake()
	{
		mesh = new Mesh ();
		this.gameObject.AddComponent<MeshFilter> ().mesh = mesh;
		this.gameObject.AddComponent<MeshRenderer> ().material = mat;

		vertices 	= new Vector3[4*footMax];
		UVs 		= new Vector2[4*footMax];
		tris 		= new int[6*footMax];

		mesh.vertices 	= vertices;
		mesh.uv 		= UVs;
		mesh.triangles	= tris;

		mesh.RecalculateNormals();
	}

	public void CreateFootprint(Vector3 pos)
	{	
		if (footCount >= footMax)
		{
			footCount = 0;
		}

		int off4 = footCount * 4;
		int off6 = footCount * 6;

		vertices [off4  ] = new Vector3 (pos.x - footSize, 0.1f, pos.y - footSize);
		vertices [off4+1] = new Vector3 (pos.x - footSize, 0.1f, pos.y + footSize);
		vertices [off4+2] = new Vector3 (pos.x + footSize, 0.1f, pos.y + footSize);
		vertices [off4+3] = new Vector3 (pos.x + footSize, 0.1f, pos.y - footSize);
		
		UVs [off4  ] = new Vector2 (1.0f, 1.0f);
		UVs [off4+1] = new Vector2 (1.0f, 0.0f);
		UVs [off4+2] = new Vector2 (0.0f, 0.0f);
		UVs [off4+3] = new Vector2 (0.0f, 1.0f);
		
		tris [off6  ] = off4;
		tris [off6+1] = off4+1;
		tris [off6+2] = off4+2;
		tris [off6+3] = off4;
		tris [off6+4] = off4+2;
		tris [off6+5] = off4+3;

		footCount++;

		mesh.vertices 	= vertices;
		mesh.uv 		= UVs;
		mesh.triangles	= tris;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		CreateFootprint (Vector3.zero + Vector3.right*footCount);
	}
}

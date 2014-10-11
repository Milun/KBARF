using UnityEngine;
using System.Collections;

public class GunEnemy : MonoBehaviour {

	private Vector3[] vertices;
	private Vector2[] UVs;
	private int[]	  tris;

	[SerializeField] private Vector2 dimensions;
	[SerializeField] private Material mat;

	private Transform myPos; 
	private Transform heroPos;
	private Mesh mesh;

	// Use this for initialization
	void Start () {
	
		// Find the hero (so you can always face it).
		heroPos = GameObject.Find ("gun_hero").transform;
		myPos = transform;

		// Initialising the mesh for the enemy.
		mesh = new Mesh ();
		this.gameObject.AddComponent<MeshFilter> ().mesh = mesh;
		this.gameObject.AddComponent<MeshRenderer> ().material = mat;

		vertices 	= new Vector3[4];
		UVs 		= new Vector2[4];
		tris 		= new int[6];

		vertices [0] = new Vector3 ( dimensions.x, 0.0f,  dimensions.y);
		vertices [1] = new Vector3 ( dimensions.x, 0.0f, -dimensions.y);
		vertices [2] = new Vector3 (-dimensions.x, 0.0f, -dimensions.y);
		vertices [3] = new Vector3 (-dimensions.x, 0.0f,  dimensions.y);

		UVs [0] = new Vector2 (1.0f, 0.0f);
		UVs [1] = new Vector2 (1.0f, 1.0f);
		UVs [2] = new Vector2 (0.0f, 1.0f);
		UVs [3] = new Vector2 (0.0f, 0.0f);

		tris [0] = 0;
		tris [1] = 1;
		tris [2] = 2;
		tris [3] = 0;
		tris [4] = 2;
		tris [5] = 3;

		mesh.vertices 	= vertices;
		mesh.uv 		= UVs;
		mesh.triangles	= tris;

		mesh.RecalculateNormals();
	}

	private void FacePlayer()
	{
		if (!myPos || !heroPos) return;

		Vector2 toPlayer = new Vector2 (Mathf.Sin(Mathf.Deg2Rad*heroPos.eulerAngles.y),
		                                -Mathf.Cos(Mathf.Deg2Rad*heroPos.eulerAngles.y));

		toPlayer.Normalize ();
		toPlayer = new Vector2 (-toPlayer.y, toPlayer.x);

		vertices [0] = new Vector3 ( dimensions.x * toPlayer.x,  dimensions.y*2.0f, -dimensions.x * toPlayer.y);
		vertices [1] = new Vector3 ( dimensions.x * toPlayer.x,  0.0f, -dimensions.x * toPlayer.y);
		vertices [2] = new Vector3 (-dimensions.x * toPlayer.x,  0.0f,  dimensions.x * toPlayer.y);
		vertices [3] = new Vector3 (-dimensions.x * toPlayer.x,  dimensions.y*2.0f,  dimensions.x * toPlayer.y);

		mesh.vertices = vertices;
	}
	
	// Update is called once per frame
	void Update () {
		FacePlayer ();
	}
}

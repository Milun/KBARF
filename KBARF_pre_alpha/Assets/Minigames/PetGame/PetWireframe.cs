using UnityEngine;
using System.Collections;

public class PetWireframe : MonoBehaviour {

	void Create() {
		OnPreRender ();
	}

	void OnPreRender() {
		GL.Color(Color.yellow);
		GL.wireframe = true;
	}

	void OnPostRender() {
		GL.wireframe = false;
	}
}
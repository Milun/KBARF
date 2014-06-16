using UnityEngine;
using System.Collections;

public class MainGUI : MonoBehaviour {

	void OnGUI ()
	{
		// Make a background box
		GUI.Box(new Rect(Screen.width/2.0f - 5.0f, Screen.height/2.0f - 5.0f, 10.0f, 10.0f), "Crosshair");
	}
}

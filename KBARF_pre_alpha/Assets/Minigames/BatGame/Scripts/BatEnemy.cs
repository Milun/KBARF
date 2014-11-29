using UnityEngine;
using System.Collections;

public class BatEnemy : MonoBehaviour {

	private GameObject batHero;

	// Use this for initialization
	void Start ()
	{
		batHero = GameObject.Find ("pre_batHero");
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (batHero)
		{
			float dist = batHero.transform.position.x - this.transform.position.x;

			if (Mathf.Abs(dist) > 0.1f)
			{
				transform.position += new Vector3( (dist)/100.0f, 0.0f, 0.0f );
			}
		}
	}
}

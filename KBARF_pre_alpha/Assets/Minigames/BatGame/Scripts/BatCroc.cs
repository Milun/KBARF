using UnityEngine;
using System.Collections;

public class BatCroc : MonoBehaviour {

	private GameObject batHero;

	[SerializeField] private BatVectorSprite sprMouthOpen;

	// Use this for initialization
	void Start ()
	{
		batHero = GameObject.Find ("pre_batHero");

		Destroy (this.GetComponent<MeshRenderer> ());

		sprMouthOpen.InitSprite ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		sprMouthOpen.Draw (this.transform.position);

		if (batHero)
		{
			float dist = batHero.transform.position.x - this.transform.position.x;

			if (Mathf.Abs(dist) > 0.1f)
			{
				float multi = (batHero.transform.position.y - this.transform.position.y)*2.0f;
				if (multi <= 2.0f) multi = 2.0f;

				transform.position += new Vector3( dist/multi, 0.0f, 0.0f );
			}
		}
	}
}

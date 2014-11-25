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
				transform.position += new Vector3( (dist)/100.0f, 0.0f, 0.0f );
			}
		}
	}
}

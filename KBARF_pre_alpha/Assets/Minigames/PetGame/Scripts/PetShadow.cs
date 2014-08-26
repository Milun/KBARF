using UnityEngine;
using System.Collections;

public class PetShadow : MonoBehaviour {

	public GameObject preShadow;

	private TwoCommon tCommon;
	private TwoFlip	  tFlip;
	private TwoFlip	  tFlipOther;
	private SpriteRenderer sRenderer;

	void Awake () {
		GameObject 	temp = 	(GameObject)HierarchicalPrefabUtility.Instantiate (preShadow);
		sRenderer = temp.GetComponent<SpriteRenderer> ();

		tCommon = GetComponent<TwoCommon> ();

		tFlip 		= GetComponent<TwoFlip> ();
		tFlipOther 	= preShadow.GetComponent<TwoFlip> ();
	}
	
	// Update is called once per frame
	void Update () {
	
		preShadow.GetComponent<SpriteRenderer> ().sprite = this.GetComponent<SpriteRenderer> ().sprite;

		preShadow.transform.position = transform.position + new Vector3 (0.4f, 0.6f, -3.0f);

		if (tFlip)
		{
			tFlipOther.Flip = tFlip.Flip;
		}

		preShadow.renderer.enabled = this.renderer.enabled;
	}
}

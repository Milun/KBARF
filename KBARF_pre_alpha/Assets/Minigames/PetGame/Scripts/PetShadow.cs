using UnityEngine;
using System.Collections;

public class PetShadow : MonoBehaviour {

	public GameObject preShadow;

	private TwoCommon tCommon;
	//private TwoFlip tFlip;

	void Awake () {
		preShadow = (GameObject)HierarchicalPrefabUtility.Instantiate (preShadow);
		tCommon = GetComponent<TwoCommon> ();
		//tFlip = GetComponent<TwoFlip> ();
	}
	
	// Update is called once per frame
	void Update () {
	
		preShadow.GetComponent<SpriteRenderer> ().sprite = this.GetComponent<SpriteRenderer> ().sprite;
		
		//if (tCommon && tCommon.Frame)
		{
			preShadow.transform.position = this.transform.position + new Vector3 (0.2f, 0.3f, -3.0f);
		}

		/*if (tFlip)
		{
			if (tFlip.Flip < 0)
			{
				transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
			}
			else
			{
				transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			}
		}*/
	}

	/*public GameObject Shadow
	{
		get
		{
			return Shadow;
		}
	}*/
}

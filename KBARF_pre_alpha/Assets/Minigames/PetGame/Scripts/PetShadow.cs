using UnityEngine;
using System.Collections;

public class PetShadow : MonoBehaviour {

	public GameObject preShadow;
	private TwoCommon tCommon;

	void Awake () {
		preShadow = (GameObject)HierarchicalPrefabUtility.Instantiate (preShadow);
		tCommon = GetComponent<TwoCommon> ();
	}
	
	// Update is called once per frame
	void Update () {
	
		preShadow.GetComponent<SpriteRenderer> ().sprite = this.GetComponent<SpriteRenderer> ().sprite;
		
		//if (tCommon && tCommon.Frame)
		{
			preShadow.transform.position = this.transform.position + new Vector3 (0.2f, 0.3f, -3.0f);
		}

	}
}

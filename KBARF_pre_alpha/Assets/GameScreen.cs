using UnityEngine;
using System.Collections;

public class GameScreen : MonoBehaviour {

	private Vector3 m_targetPos 		= Vector3.zero;
	private Vector3 m_targetRot		 	= Vector3.zero;
	[SerializeField] private float dist	= 3.0f;

	// Use this for initialization
	void Awake ()
	{
		m_targetPos = transform.position + transform.up * dist;

		transform.Rotate (new Vector3 (90.0f, 0.0f, 0.0f));
		m_targetRot = transform.eulerAngles + new Vector3(0.0f, 0.0f, 180.0f);
		transform.Rotate (new Vector3 (-90.0f, 0.0f, 0.0f));
	}

	public void Enter(MainInput pass)
	{
		transform.parent.parent.GetComponent<MiniInput> ().SetInput (pass);
	}

	public void Leave()
	{
		transform.parent.parent.GetComponent<MiniInput> ().FreeInput();
	}
	
	public Vector3 targetPos
	{
		get
		{
			return m_targetPos;
		}
	}

	public Vector3 targetRot
	{
		get
		{
			return m_targetRot;
		}
	}
}
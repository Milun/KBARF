using UnityEngine;
using System.Collections;

public class TwoGlobal : MonoBehaviour {
	
	[SerializeField] private float 			frameDelay = 0.1f;
	[SerializeField] private float 			pixelJump = 1.0f;
	[SerializeField] private int			layer = 0;
	[SerializeField] private Vector2		roomSize = Vector2.one;

	private float							frameTime = 0.0f;
	private bool 							frameActive = false;

	private float frameDelayToggle = 0.0f;
	private float pixelJumpToggle = 0.0f;

	[SerializeField] private GameObject frameDelayToggleGo;
	[SerializeField] private GameObject frameDelayJumpGo;

	void Awake()
	{
		if (frameDelay == 0.0f)
		{
			frameActive = true;
		}
	}

	void Start()
	{
		if (frameDelayToggle != null && frameDelayJumpGo != null)
		{
			frameDelayToggleGo.renderer.enabled = false;
			frameDelayJumpGo.renderer.enabled = false;
		}
	}

	// Returns true whenever a frame triggers.
	public bool Frame()
	{
		return frameActive;
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown("f"))
		{
			if (frameDelayToggle == 0.0f)
			{
				frameDelayToggle = 0.04f;
				if (frameDelayToggle != null && frameDelayJumpGo != null) frameDelayToggleGo.renderer.enabled = true;
			}
			else
			{	
				if (frameDelayToggle != null && frameDelayJumpGo != null) frameDelayToggleGo.renderer.enabled = false;
				frameDelayToggle = 0.0f;
				frameDelay = 0.0f;
				frameTime = 0.0f;
				frameActive = true;
			}

			print (frameDelayToggle);
		}

		if (Input.GetKeyDown("p"))
		{
			if (pixelJumpToggle == 0.0f)
			{
				pixelJumpToggle = 1.0f;
				if (frameDelayToggle != null && frameDelayJumpGo != null) frameDelayJumpGo.renderer.enabled = true;
			}
			else
			{
				pixelJumpToggle = 0.0f;
				if (frameDelayToggle != null && frameDelayJumpGo != null) frameDelayJumpGo.renderer.enabled = false;
			}
		}

		if (frameDelay + frameDelayToggle <= 0.01f)
		{
			return;
		}

		if (frameTime < 0.0f)
		{
			frameActive = true;
			frameTime = frameDelay + frameDelayToggle;
		}
		else
		{
			frameActive = false;
		}

		frameTime -= Time.deltaTime;
	}

	public float PIXEL_JUMP
	{
		get
		{
			return pixelJump + pixelJumpToggle;
		}
	}

	public int LAYER
	{
		get
		{
			return layer;
		}
	}

	public Vector2 ROOM_SIZE
	{
		get
		{
			return roomSize;
		}
	}
}

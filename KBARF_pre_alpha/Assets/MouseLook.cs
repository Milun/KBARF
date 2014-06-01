using UnityEngine;
using System.Collections;
[AddComponentMenu("Camera-Control/Mouse Look")]

public class MouseLook : MonoBehaviour {

	private const float GAME_SCREEN_LOOK_DISTANCE  = 4.0f; 
	private const float GAME_SCREEN_LEAVE_DISTANCE = 2.0f;
	private const float GAME_SCREEN_LOOK_SPEED 	   = 0.08f;

	[SerializeField] private float lookSpeed = 1.0f;	// Speed at which the mouse aim moves.
	[SerializeField] private float moveSpeed = 0.1f;	// Speed at which the player moves.

	private float xAim 		= 0.0f;						// The aim of the mouselook.
	private float yAim 		= 0.0f;
	private Quaternion startAim;						// Initial rotation of the camera

	private string state;								// Used for the FSM.

	private GameScreen gameScreen = null;				// The GameScreen the player is currently assigned to.	

	private Vector3 camRotation = Vector3.zero;

	void Start() {

		startAim = transform.localRotation;

		Screen.showCursor = false;
	}

	private void MouseAim()
	{
		// Rotate mouse based on your rotation.
		float x = Input.GetAxis ("Mouse X");
		float y = Input.GetAxis ("Mouse Y");

		//if (transform.eulerAngles.z >= 180.0f) x = -x;
		//if (camRotation.z >= 180.0f) x = -x;

		//print (transform.rotation.x + " " + transform.rotation.y + " " + transform.rotation.z + " " + transform.rotation.w);

		/*Vector2 vecOrigin = new Vector2(x, Input.GetAxis ("Mouse Y"));

		float length = vecOrigin.magnitude;

		float angle 	= Vector2.Angle (Vector2.up, vecOrigin);

		if (vecOrigin.x < 0.0f)
		{
			angle = 360 - angle;
		}

		float angleNew 	= angle - camRotation.z;

		Vector2 vecNew = new Vector2 (Mathf.Cos (angleNew * Mathf.Deg2Rad), Mathf.Sin (angleNew * Mathf.Deg2Rad));
		vecNew.Normalize ();
		vecNew *= length;*/

		// Change the angle of the camera.
		xAim += lookSpeed * y;
		yAim += lookSpeed * x;

		// Wrap the camera angles around.
		if (xAim < -360.0f)
		{
			xAim += 360.0f;
		}
		else if (xAim > 360.0f)
		{
			xAim -= 360.0f;
		}
		
		// Clamp looking up and down.
		if (yAim < -360.0f)
		{
			yAim += 360.0f;
		}
		else if (yAim > 360.0f)
		{
			yAim -= 360.0f;
		}

		// Rotated
		//Vector2 

		transform.Rotate (-Vector3.right * y);
		transform.Rotate (Vector3.up * x);

		//transform.eulerAngles = new Vector3 (-xAim, yAim, camRotation.z);

		// Actually change the angle of the camera.
		//transform.eulerAngles += camRotation;

		// Quaternion xQ = Quaternion.AngleAxis (xAim, Vector3.up);
		// Quaternion yQ = Quaternion.AngleAxis (yAim, -Vector3.right);

		//transform.localRotation = startAim * xQ * yQ;
		//transform.eulerAngles += camRotation;
	}

	private void Move()
	{
		if 		(Input.GetKey("w")) transform.position += transform.forward*moveSpeed;
		else if (Input.GetKey("s")) transform.position -= transform.forward*moveSpeed;
		
		if 		(Input.GetKey("a"))	transform.position -= transform.right*moveSpeed;
		else if (Input.GetKey("d")) transform.position += transform.right*moveSpeed;

		if 		(Input.GetKey("q")) transform.eulerAngles += new Vector3(0.0f, 0.0f, 1.0f);
		else if (Input.GetKey("e")) transform.eulerAngles -= new Vector3(0.0f, 0.0f, 1.0f);
	}

	public void EnterGameScreen(GameScreen gs)
	{
		// Don't set the screen if you're already looking at it.
		if (state == "screen_lock") return;

		gameScreen = gs;
		state = "screen_lock";
	}

	public void LeaveGameScreen()
	{
		// Move away from the screen.
		Vector3 normPos = (this.transform.position - gameScreen.transform.position).normalized;
		Vector3 targetPos = new Vector3 (gameScreen.targetPos.x + normPos.x*GAME_SCREEN_LEAVE_DISTANCE,
		                                 this.transform.position.y,
		                                 gameScreen.targetPos.z + normPos.z*GAME_SCREEN_LEAVE_DISTANCE);

		// If we've finished leaving the screen, free both it and the screen.
		Vector3 move = Stat3DMove.MoveToPos (transform.position,
		                                     targetPos,
		                                     GAME_SCREEN_LOOK_SPEED);
		if (move == Vector3.zero)
		{
			gameScreen = null;
			state = "";
			return;
		}

		transform.position += move;
	}

	private void ClickGameScreen()
	{
		// Only do anything when left click is pressed.
		if (!Input.GetMouseButtonDown (0)) return;

		// Relock the cursor inside the game
		Screen.lockCursor = true;

		// Leave the TV if you're on it.
		if (state == "screen_lock")
		{
			state = "screen_leave";
			return;
		}

		// Check if we clicked a screen.
		RaycastHit 	hit;
		if (Physics.Raycast(GetComponent<Camera>().ScreenPointToRay( new Vector3(Screen.width/2, Screen.height/2, 0.0f) ),
		                    out hit,
		                    GAME_SCREEN_LOOK_DISTANCE) )	// If the screen is too far away, do nothing.
		{
			if (hit.transform.gameObject.GetComponent<GameScreen>())
			{
				EnterGameScreen(hit.transform.gameObject.GetComponent<GameScreen>());
			}
		}
	}
		
		// Update is called once per frame
	void Update () {
	
		// Check if the player has clicked on a game screen.
		ClickGameScreen ();

		// If the player is looking at the screen, move them to the ideal position.
		if (state == "screen_lock")
		{
			transform.position 		+= Stat3DMove.MoveToPos(transform.position, 	gameScreen.targetPos, GAME_SCREEN_LOOK_SPEED);
			transform.eulerAngles 	+= Stat3DMove.MoveToRot(transform.eulerAngles, gameScreen.targetRotation, GAME_SCREEN_LOOK_SPEED);

			// Manipulate the MouseAim to make the game think we're moving the mouse like this.
			xAim = gameScreen.targetRotation.y;
			yAim = gameScreen.targetRotation.x;

			return;
		}

		// Allow player to move around while leaving the screen.
		MouseAim ();

		// Stop looking at a screen you're locked into when you leave.
		if (state == "screen_leave")
		{
			LeaveGameScreen();

			return;
		}

		// Otherwise allow movement normally.
		Move ();
	}
}

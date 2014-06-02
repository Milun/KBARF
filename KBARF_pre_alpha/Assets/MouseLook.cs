using UnityEngine;
using System.Collections;
[AddComponentMenu("Camera-Control/Mouse Look")]

public class MouseLook : MonoBehaviour {

	private const float GAME_SCREEN_LOOK_DISTANCE  = 4.0f; 
	private const float GAME_SCREEN_LEAVE_DISTANCE = 2.0f;
	private const float GAME_SCREEN_LOOK_SPEED 	   = 0.08f;

	[SerializeField] private float lookSpeed = 1.0f;	// Speed at which the mouse aim moves.
	[SerializeField] private float moveSpeed = 0.1f;	// Maximum speed at which the player moves.
	[SerializeField] private float accelSpeed = 0.02f;	// Speed at which the player gains speed.
	[SerializeField] private float friction = 0.92f;	// Speed at which the speed is worn down.

	private float xAim 		= 0.0f;						// The aim of the mouselook.
	private float yAim 		= 0.0f;
	private Quaternion startAim;						// Initial rotation of the camera

	private string state;								// Used for the FSM.

	private GameScreen gameScreen = null;				// The GameScreen the player is currently assigned to.	

	private Vector3 camRotation = Vector3.zero;
	private Vector3 camMove = Vector3.zero;

	void Start() {

		startAim = transform.localRotation;

		Screen.showCursor = false;
	}

	private void MouseAim()
	{
		// Change the angle of the camera.
		xAim = lookSpeed * Input.GetAxis ("Mouse X");
		yAim = lookSpeed * Input.GetAxis ("Mouse Y");

		// Now rotate the camera.
		transform.Rotate (-Vector3.right * yAim);
		transform.Rotate (Vector3.up * xAim);
	}

	private void Move()
	{
		camMove *= friction;

		if 		(Input.GetKey("w")) camMove += transform.forward*accelSpeed;
		else if (Input.GetKey("s")) camMove -= transform.forward*accelSpeed;
		
		if 		(Input.GetKey("a"))	camMove -= transform.right*accelSpeed;
		else if (Input.GetKey("d")) camMove += transform.right*accelSpeed;

		if 		(Input.GetKey("q")) transform.eulerAngles += new Vector3(0.0f, 0.0f, 1.0f);
		else if (Input.GetKey("e")) transform.eulerAngles -= new Vector3(0.0f, 0.0f, 1.0f);

		if (camMove.magnitude > moveSpeed)
		{
			camMove.Normalize();
			camMove *= moveSpeed;
		}

		transform.position += camMove;
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
		                                 gameScreen.targetPos.y + normPos.y*GAME_SCREEN_LEAVE_DISTANCE,
		                                 gameScreen.targetPos.z + normPos.z*GAME_SCREEN_LEAVE_DISTANCE);

		// If we've finished leaving the screen, free both it and the screen.
		Vector3 move = Stat3DMove.MoveToPos (transform.position,
		                                     targetPos,
		                                     GAME_SCREEN_LOOK_SPEED);
		if (move == transform.position)
		{
			gameScreen = null;
			state = "";
			camMove = Vector3.zero;
			return;
		}

		transform.position = move;
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
			transform.position 		= Stat3DMove.MoveToPos(transform.position, 		gameScreen.targetPos, GAME_SCREEN_LOOK_SPEED);
			transform.eulerAngles 	= Stat3DMove.MoveToRot(transform.eulerAngles, 	gameScreen.targetRot, GAME_SCREEN_LOOK_SPEED);

			// Manipulate the MouseAim to make the game think we're moving the mouse like this.
			xAim = gameScreen.targetRot.y;
			yAim = gameScreen.targetRot.x;

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

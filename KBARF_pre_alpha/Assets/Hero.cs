using UnityEngine;
using System.Collections;

[RequireComponent( typeof(MouseLook))]
public class Hero : MonoBehaviour {

	private const float GAME_SCREEN_LOOK_DISTANCE  = 4.0f; 
	private const float GAME_SCREEN_LEAVE_DISTANCE = 2.0f;
	private const float GAME_SCREEN_LOOK_SPEED 	   = 0.08f;

	// Physics
	[SerializeField] private float moveSpeed = 1.0f;	// Maximum speed at which the player moves.
	[SerializeField] private float moveAccel = 0.1f;	// Speed at which the player gains speed.
	[SerializeField] private float rotSpeed  = 1.0f;	// Maximum speed at which the player rotates their view.
	[SerializeField] private float rotAccel  = 0.02f;	// Speed at which the player accelerates their rotation.

	[SerializeField] private float friction = 0.997f;	// Speed at which the speed is worn down.
	private float 				   rot = 0.0f;			// Camera's rotation.

	private string state;								// Used for the FSM.
	
	private GameScreen gameScreen = null;				// The GameScreen the player is currently assigned to.	

	// Components
	MouseLook 			mouseLook;

	// Use this for initialization
	void Awake ()
	{
		mouseLook = GetComponent<MouseLook> ();
	}

	public void EnterGameScreen(GameScreen gs)
	{
		// Don't set the screen if you're already looking at it.
		if (state == "screen_lock") return;
		
		gameScreen = gs;
		gameScreen.Enter (GetComponent<MainInput> ());

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
			gameScreen.Leave();
			gameScreen = null;

			state = "";
			
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
			// Stop any velocity you had.
			rigidbody.velocity = Vector3.zero;
			rot = 0.0f;
			
			// Move towards the screen
			transform.position 		= Stat3DMove.MoveToPos(transform.position, 		gameScreen.targetPos, GAME_SCREEN_LOOK_SPEED);
			transform.eulerAngles 	= Stat3DMove.MoveToRot(transform.eulerAngles, 	gameScreen.targetRot, GAME_SCREEN_LOOK_SPEED);
			
			// Manipulate the MouseAim to make the game think we're moving the mouse like this.
			mouseLook.SetMouseAim(gameScreen.targetRot.y, gameScreen.targetRot.x);
			
			return;
		}
		
		// Allow player to move around while leaving the screen.
		mouseLook.MouseAim ();
		
		// Stop looking at a screen you're locked into when you leave.
		if (state == "screen_leave")
		{
			LeaveGameScreen();
			
			return;
		}
		
		// Otherwise allow movement normally.
		Move ();
	}


	private void Move()
	{
		float frictionExtra = 0.0f;
		if (Input.GetKey("space")) frictionExtra = 0.02f;
		
		rigidbody.velocity *= friction - frictionExtra;
		rot *= friction - frictionExtra;
		
		if 		(Input.GetKey("w")) rigidbody.velocity += transform.forward*moveAccel;
		else if (Input.GetKey("s")) rigidbody.velocity -= transform.forward*moveAccel;
		
		if 		(Input.GetKey("a"))	rigidbody.velocity -= transform.right*moveAccel;
		else if (Input.GetKey("d")) rigidbody.velocity += transform.right*moveAccel;
		
		if 		(Input.GetKey("q")) rot += rotAccel;
		else if (Input.GetKey("e")) rot -= rotAccel;
		
		if (rigidbody.velocity.magnitude > moveSpeed)
		{
			rigidbody.velocity.Normalize();
			rigidbody.velocity *= moveSpeed;
		}
		
		if 		(rot > rotSpeed) 	rot = rotSpeed;
		else if (rot < -rotSpeed) 	rot = -rotSpeed;
		
		//rigidbody.velocity = move;
		transform.eulerAngles += new Vector3(0.0f, 0.0f, rot);
	}
}

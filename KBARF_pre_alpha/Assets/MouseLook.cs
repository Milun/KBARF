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

	void Start() {

		startAim = transform.localRotation;

		Screen.showCursor = false;
	}

	private void MouseAim()
	{
		// Change the angle of the camera.
		xAim += lookSpeed * Input.GetAxis ("Mouse X");
		yAim += lookSpeed * Input.GetAxis ("Mouse Y");

		// Wrap the camera angles around.
		if (xAim < -360.0f)
		{
			xAim += 360.0f;
		}
		else if (xAim > 360.0f)
		{
			xAim -= 360.0f;
		}
		
		if (yAim < -90.0f)
		{
			yAim = -90.0f;
		}
		else if (yAim > 90.0f)
		{
			yAim = 90.0f;
		}

		// Actually change the angle of the camera.
		Quaternion xQ = Quaternion.AngleAxis (xAim, Vector3.up);
		Quaternion yQ = Quaternion.AngleAxis (yAim, -Vector3.right);
		
		transform.localRotation = startAim * xQ * yQ;
	}

	private void Move()
	{
		Vector3 xVec = new Vector3(Mathf.Sin ( Mathf.Deg2Rad*xAim )*moveSpeed,
		                           0.0f,
		                           Mathf.Cos ( Mathf.Deg2Rad*xAim )*moveSpeed);
		
		Vector3 yVec = new Vector3(Mathf.Sin ( Mathf.Deg2Rad*xAim+90.0f )*moveSpeed,
		                           0.0f,
		                           Mathf.Cos ( Mathf.Deg2Rad*xAim+90.0f )*moveSpeed);
		
		
		if 		(Input.GetKey("w")) transform.position += xVec;
		else if (Input.GetKey("s")) transform.position -= xVec;
		
		if 		(Input.GetKey("a"))	transform.position -= yVec;
		else if (Input.GetKey("d")) transform.position += yVec;
	}

	// Returns true if it's moved to the target.
	private bool MoveToPos(Vector3 pos, float speed)
	{
		// Check if the camera is at the correct position.
		if ((pos - transform.position).magnitude <= 0.05f)
		{
			return true;
		}

		// Move the camera.
		transform.position += (pos - transform.position) * speed;

		return false;
	}

	// Returns true if it's rotated to the target.
	private bool MoveToRot (Vector3 rot, float speed)
	{
		// Get the angle between where we want to rotate and where we're currently rotated.
		float minAngleX = rot.x - transform.eulerAngles.x;
		float minAngleY = rot.y - transform.eulerAngles.y;
		
		// Check if the camera is at the correct rotation.
		if (Mathf.Abs(minAngleX) <= 0.05f &&
		    Mathf.Abs(minAngleY) <= 0.05f)
		{
			return true;
		}
		
		// Calculate the correct angle to use.
		while (minAngleX < -180.0f) 	minAngleX += 360.0f;
		while (minAngleX > 180)			minAngleX -= 360;
		while (minAngleY < -180.0f) 	minAngleY += 360.0f;
		while (minAngleY > 180)			minAngleY -= 360;

		// Rotate the camera.
		transform.eulerAngles += new Vector3 (minAngleX * speed, minAngleY * speed, 0.0f);
		
		// Manipulate the MouseAim to make the game think we're moving the mouse like this.
		xAim = rot.y;
		yAim = rot.x;
		
		return false;
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
		if (MoveToPos (targetPos, GAME_SCREEN_LOOK_SPEED))
		{
			gameScreen = null;
			state = "";
		}
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
			MoveToPos(gameScreen.targetPos, GAME_SCREEN_LOOK_SPEED);
			MoveToRot(gameScreen.targetRotation, GAME_SCREEN_LOOK_SPEED);

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

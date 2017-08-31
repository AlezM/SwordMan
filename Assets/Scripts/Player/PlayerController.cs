using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamePad;

public class PlayerController : MonoBehaviour {

	public float movingSpeed = 10;
	public float jumpForceMultiplier = 10;
	public float jumpDelay = 2f;

	[Space()]
	public LayerMask groundLayerMask;

	bool lookRight = true;

	//Inputs
	float horizontal = 0;
	float vertical = 0;
	float jumpForce = 0;

	//Jumping
	bool jumped = false;
	bool doubleJump = false;
	float gravity;

	Rigidbody2D rb2D;
	Animator anim;

	void Start () {
		rb2D = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		gravity = -Physics2D.gravity.y;
	}

	void Update () {
		
	}

	public void InputHandler (JoystickInfo joystickInfo) {
		horizontal = ((Mathf.Abs(joystickInfo.position.x) > 0.1f)? Mathf.Clamp(2 * joystickInfo.position.x, -1, 1) : 0);
		jumpForce = Mathf.Clamp(joystickInfo.deltaPosition.magnitude, -1f, 1f);
		vertical = joystickInfo.position.y;

		Movement ();

		Fliping ();

		ApplyAnimator ();
	}
		
	public void InputHandler (ButtonInfo buttonInfo) {
		if (!doubleJump && (isGrounded () || jumped) && buttonInfo.phase == ButtonPhase.Down) {
			rb2D.velocity = new Vector2 (rb2D.velocity.x, 10 * jumpForceMultiplier);
			if (jumped)
				doubleJump = true;

			jumped = true;
			Invoke ("CanJump", jumpDelay); 
		}
	}

	void Movement () {
		Vector2 velocity = new Vector2 (horizontal * movingSpeed, rb2D.velocity.y);

		rb2D.velocity = velocity;
	}

	void Fliping() {
		if ((lookRight && horizontal < 0) || (!lookRight && horizontal > 0)) {
			lookRight = !lookRight;
			transform.localScale = new Vector3 ((lookRight) ? 1 : -1, 1, 1);
		}
	}

	void ApplyAnimator () {
		anim.SetFloat ("horizontal", Mathf.Abs (horizontal));
	}

	void CanJump () {
		jumped = false;
		doubleJump = false;
	}

	bool isGrounded () {
	//	return rb2D.IsTouchingLayers (groundLayerMask);
		return (Physics2D.Raycast (transform.position, Vector2.down, 0.1f, groundLayerMask).collider != null);
	}
}
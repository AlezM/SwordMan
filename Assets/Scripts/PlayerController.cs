using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float moveSpeed = 10;
	public float jumpSpeed = 10;
	public float speedLimit = 1;

	bool flip = false;
	public SpriteFlipper graphycs;
	public GameObject frontArm;
	public GameObject backArm;

	Rigidbody2D rb2D;

	void Start () {
		rb2D = GetComponent<Rigidbody2D> ();
	}

	void Update () {
		
	}

	public void Movement (JoystickInfo joystickInfo) {
		float horizontal = ((Mathf.Abs(joystickInfo.position.x) > 0.1f)? joystickInfo.position.x : 0) * moveSpeed;
		float vertical = Mathf.Clamp(joystickInfo.deltaPosition.y, -speedLimit, speedLimit)  * jumpSpeed;

	
		if (horizontal > 0 && flip) {
			flip = false;
			graphycs.Flip(false, 0);
			frontArm.SetActive (true);
			backArm.SetActive (false);
		} else if (horizontal < 0 && !flip){
			flip = true;
			graphycs.Flip(true, 3);
			frontArm.SetActive (false);
			backArm.SetActive (true);
		}

		rb2D.velocity = new Vector2 (horizontal, rb2D.velocity.y + vertical);			
	}
}

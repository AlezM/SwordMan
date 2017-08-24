using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float moveSpeed = 10;
	public float jumpSpeed = 10;
	public float speedLimit = 1;

	public GameObject front;

	Rigidbody2D rb2D;

	void Start () {
		rb2D = GetComponent<Rigidbody2D> ();
	}

	void Update () {
		
	}

	public void Movement (JoystickInfo joystickInfo) {
		float horizontal = joystickInfo.position.x * moveSpeed;
		float vertical = Mathf.Clamp(joystickInfo.deltaPosition.y, -speedLimit, speedLimit)  * jumpSpeed;

		transform.eulerAngles = new Vector3 (0, (horizontal > 0)? 0 : 180, 0);
		if (horizontal > 0)
			front.SetActive (false);
		else 
			front.SetActive (true);

		rb2D.velocity = new Vector2 (horizontal, rb2D.velocity.y + vertical);			
	}
}

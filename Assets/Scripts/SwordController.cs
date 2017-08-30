using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamePad;

public class SwordController : MonoBehaviour {

	public float rotationSpeed;
	public float minAngle = -90;
	public float maxAngle = 90;
	Rigidbody2D rb2D;

	float deltaAngle = 0;
	float inputAngle = 0;

	void Start () {
		rb2D = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate () {
		Movement ();	
	}

	void Movement () {
		deltaAngle = inputAngle * transform.root.localScale.x - Vector2.Angle (transform.up, Vector2.up) * Mathf.Sin (Vector3.Cross ( Vector3.up, transform.up).z);;
		rb2D.angularVelocity = deltaAngle * rotationSpeed;
	} 

	public void InputHandler (JoystickInfo joystickInfo) {
		deltaAngle = Vector3.Angle (transform.up, joystickInfo.position) * Mathf.Sign (Vector3.Cross (transform.up, joystickInfo.position).z);

		rb2D.angularVelocity = deltaAngle * rotationSpeed;
	}

	public void InputHandler (LeverInfo leverInfo) {
		//deltaAngle = Mathf.Lerp(minAngle, maxAngle, leverInfo.position/2 + 1);
		//deltaAngle = leverInfo.position * 200;
		inputAngle =  Mathf.Lerp (minAngle, maxAngle, leverInfo.position01);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour {

	public float rotationSpeed;
	public float minAngle = 0;
	public float maxAngle = 0;
	Rigidbody2D rb2D;

	float deltaAngle = 0;

	void Start () {
		rb2D = GetComponent<Rigidbody2D> ();
	}

	public void JoystickInput (JoystickInfo joystickInfo) {
		deltaAngle = Vector3.Angle (transform.up, joystickInfo.position) * Mathf.Sign (Vector3.Cross (transform.up, joystickInfo.position).z);

		rb2D.angularVelocity = deltaAngle * rotationSpeed;
	}

	public void LeverInput (LeverInfo leverInfo) {
		//deltaAngle = Mathf.Lerp(minAngle, maxAngle, leverInfo.position/2 + 1);
		deltaAngle = leverInfo.position * 200;

		rb2D.angularVelocity = deltaAngle * rotationSpeed;
	}
}

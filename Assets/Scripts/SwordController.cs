using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour {

	public float rotationSpeed;
	Rigidbody2D rb2D;

	float deltaAngle = 0;

	void Start () {
		rb2D = GetComponent<Rigidbody2D> ();
	}

	public void Rotate (JoystickInfo joystickInfo) {
		deltaAngle = Vector3.Angle (transform.up, joystickInfo.position) * Mathf.Sign (Vector3.Cross (transform.up, joystickInfo.position).z);

		rb2D.angularVelocity = deltaAngle * rotationSpeed;
	}
}

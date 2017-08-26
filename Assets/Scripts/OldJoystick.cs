using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OldJoystick : MonoBehaviour {

	public enum JoystickType { CJoystick = 1, VJoystick = 2 };

	public JoystickType joystickType = JoystickType.CJoystick;
	//CJoystick
	public float cJoySize = 10;
	public float cJoyRadius = 8;
	//VJoystick
	public float vJoyLength = 10;
	public Vector2 vJoySize;

	public JoystickEvent onClick;

	Camera cam;
	bool clicked = false;
	Transform stick;
	Vector2 stickPrevPos;

	JoystickInfo joystickInfo;

	bool touchSupported;
	Touch touch;
	int fingerId;

	void Start () {
		cam = Camera.main;
		stick = transform.GetChild (0);
		touchSupported = Input.touchSupported;
	}

	void Update () {
		#if UNITY_EDITOR
		EditorControl ();
		#else
		TouchControl ();
		#endif
	}

	void TouchControl () {
		switch(joystickType) {
		case JoystickType.CJoystick:
			CJoystickControl ();
		break;
		
		case JoystickType.VJoystick:
			VJoystickControl ();
			break;
		}
	}

	void CJoystickControl () {
		//Finding current touch by fingerId
		if (clicked) {
			for (int i = 0; i < Input.touchCount; i++) {
				if (Input.GetTouch (i).fingerId == fingerId) {
					touch = Input.GetTouch (i);
					break;
				}
			}
		}

		//Get fingerId from suitable touch
		for (int i = 0; i < Input.touchCount && !clicked; i++) {
			if (Input.GetTouch (i).phase == TouchPhase.Began) {
				Vector2 touchWorldPos = cam.ScreenToWorldPoint (Input.GetTouch (i).position);
				if (Vector2.Distance (touchWorldPos, (Vector2)transform.position) < cJoySize) {
					touch = Input.GetTouch (i);
					fingerId = touch.fingerId;
					stick.position = touchWorldPos;
					if (stick.localPosition.magnitude > cJoyRadius)
						stick.localPosition = stick.localPosition.normalized * cJoyRadius;

					stickPrevPos = stick.localPosition;

					clicked = true;
				}
			}
		}

		if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) {
			stick.localPosition = Vector3.zero;
			clicked = false;
		}

		if (clicked) {
			stick.position = (Vector2)cam.ScreenToWorldPoint (touch.position);
			if (stick.localPosition.magnitude > cJoyRadius)
				stick.localPosition = stick.localPosition.normalized * cJoyRadius;

			Vector2 delta = (Vector2)stick.localPosition - stickPrevPos;
			stickPrevPos = stick.localPosition;
			joystickInfo = new JoystickInfo (stick.localPosition / cJoyRadius, delta / cJoyRadius);

			onClick.Invoke(joystickInfo);
		}
	}

	void VJoystickControl () {
		if (clicked) {
			for (int i = 0; i < Input.touchCount; i++) {
				if (Input.GetTouch (i).fingerId == fingerId) {
					touch = Input.GetTouch (i);
					break;
				}
			}
		}

		for (int i = 0; i < Input.touchCount && !clicked; i++) {
			if (Input.GetTouch (i).phase == TouchPhase.Began) {
				Vector2 touchWorldPos = cam.ScreenToWorldPoint (Input.GetTouch (i).position);
				if ( Mathf.Abs(touchWorldPos.x - transform.position.x) < vJoySize.x / 2 && 
					 Mathf.Abs(touchWorldPos.y - transform.position.y) < vJoySize.y / 2 ) {

					touch = Input.GetTouch (i);
					fingerId = touch.fingerId;
					stick.position = new Vector3(transform.position.x, touchWorldPos.y);
					if (stick.localPosition.y > vJoyLength)
						stick.localPosition = stick.localPosition.normalized * vJoyLength;

					stickPrevPos = stick.localPosition;

					clicked = true;
				}
			}
		}

		if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) {
		//	stick.localPosition = Vector3.zero;
			clicked = false;
		}

		if (clicked) {
			stick.position = new Vector2(transform.position.x, cam.ScreenToWorldPoint (touch.position).y);
			if (stick.localPosition.y > vJoyLength)
				stick.localPosition = stick.localPosition.normalized * vJoyLength;

			Vector2 delta = (Vector2)stick.localPosition - stickPrevPos;
			stickPrevPos = stick.localPosition;
			joystickInfo = new JoystickInfo (stick.localPosition / vJoyLength, delta / vJoyLength);

			onClick.Invoke(joystickInfo);
		}


	}

	void EditorControl () {
		if (Input.GetMouseButtonDown (0)) {
			Vector2 mouseWorldPos = cam.ScreenToWorldPoint (Input.mousePosition);
			if (Vector2.Distance (mouseWorldPos, (Vector2)transform.position) < cJoySize) {
				stick.position = (Vector2)cam.ScreenToWorldPoint (Input.mousePosition);
				if (stick.localPosition.magnitude > cJoyRadius)
					stick.localPosition = stick.localPosition.normalized * cJoyRadius;

				stickPrevPos = stick.localPosition;

				clicked = true;
			}
		}

		if (Input.GetMouseButtonUp (0)) {
			stick.localPosition = Vector3.zero;
			clicked = false;
		}

		if (clicked) {
			stick.position = (Vector2)cam.ScreenToWorldPoint (Input.mousePosition);
			if (stick.localPosition.magnitude > cJoyRadius)
				stick.localPosition = stick.localPosition.normalized * cJoyRadius;

			Vector2 delta = (Vector2)stick.localPosition - stickPrevPos;
			stickPrevPos = stick.localPosition;
			joystickInfo = new JoystickInfo (stick.localPosition / cJoyRadius, delta / cJoyRadius);

			onClick.Invoke(joystickInfo);
		}
	}
}
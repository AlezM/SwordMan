using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GamePad {
	public class Joystick : MonoBehaviour {

		public float touchZoneSize = 18;
		public float radius = 12;

		public JoystickEvent onClick;

		Camera cam;
		bool clicked = false;
		Transform stick;
		Vector2 stickPrevPos;

		Touch touch;
		int fingerId;

		void Start () {
			cam = Camera.main;
			stick = transform.GetChild (0);
		}

		void Update () {
			#if UNITY_EDITOR
			EditorControl ();
			#else
			TouchControl ();
			#endif
		}

		void TouchControl () {
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
					if (Vector2.Distance (touchWorldPos, (Vector2)transform.position) < touchZoneSize) {
						touch = Input.GetTouch (i);
						fingerId = touch.fingerId;
						stick.position = touchWorldPos;
						if (stick.localPosition.magnitude > radius)
							stick.localPosition = stick.localPosition.normalized * radius;

						stickPrevPos = stick.localPosition;

						clicked = true;
					}
				}
			}

			if (clicked) {
				stick.position = (Vector2)cam.ScreenToWorldPoint (touch.position);
				if (stick.localPosition.magnitude > radius)
					stick.localPosition = stick.localPosition.normalized * radius;

				Vector2 delta = (Vector2)stick.localPosition - stickPrevPos;
				stickPrevPos = stick.localPosition;

				if (touch.phase == TouchPhase.Ended) {
					stick.localPosition = Vector3.zero;
				}

				onClick.Invoke(new JoystickInfo (stick.localPosition / radius, delta / radius));
			}

			if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) {
				stick.localPosition = Vector3.zero;
				clicked = false;
			}
		}

		void EditorControl () {
			if (Input.GetMouseButtonDown (0)) {
				Vector2 mouseWorldPos = cam.ScreenToWorldPoint (Input.mousePosition);
				if (Vector2.Distance (mouseWorldPos, (Vector2)transform.position) < touchZoneSize) {
					stick.position = (Vector2)cam.ScreenToWorldPoint (Input.mousePosition);
					if (stick.localPosition.magnitude > radius)
						stick.localPosition = stick.localPosition.normalized * radius;

					stickPrevPos = stick.localPosition;

					clicked = true;
				}
			}

			if (clicked) {
				stick.position = (Vector2)cam.ScreenToWorldPoint (Input.mousePosition);
				if (stick.localPosition.magnitude > radius)
					stick.localPosition = stick.localPosition.normalized * radius;

				Vector2 delta = (Vector2)stick.localPosition - stickPrevPos;
				stickPrevPos = stick.localPosition;

				if (Input.GetMouseButtonUp (0)) {
					stick.localPosition = Vector3.zero;
				}

				onClick.Invoke(new JoystickInfo (stick.localPosition / radius, delta / radius));
			}

			if (Input.GetMouseButtonUp (0)) {
				stick.localPosition = Vector3.zero;
				clicked = false;
			}
		}
	}}
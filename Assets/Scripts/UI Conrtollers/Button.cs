using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GamePad {
	public class Button : MonoBehaviour {

		public float touchZoneSize = 5;

		public ButtonEvent onClick;

		bool clicked = false;
		Touch touch;
		int fingerId;

		Camera cam;

		void Start () {
			cam = Camera.main;
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
						clicked = true;

						onClick.Invoke (new ButtonInfo(ButtonPhase.Down));
					}
				}
			}

			if (touch.phase == TouchPhase.Ended) {
				Vector2 touchWorldPos = cam.ScreenToWorldPoint (touch.position);
				clicked = false;

				if (Vector2.Distance (touchWorldPos, (Vector2)transform.position) < touchZoneSize) {
					onClick.Invoke (new ButtonInfo(ButtonPhase.Up));
				}
			}
		}

		void EditorControl () {
			if (Input.GetMouseButtonDown (0)) {
				Vector2 mouseWorldPos = cam.ScreenToWorldPoint (Input.mousePosition);
				if (Vector2.Distance (mouseWorldPos, (Vector2)transform.position) < touchZoneSize) {
					clicked = true;

					onClick.Invoke (new ButtonInfo(ButtonPhase.Down));
				}
			}

			if (Input.GetMouseButtonUp (0)) {
				onClick.Invoke (new ButtonInfo(ButtonPhase.Up));
				clicked = false;
			}
		}
	}
}
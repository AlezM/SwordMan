using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LeverInfo {
	public float position;
	public float deltaPosition;

	public float position01 { 
		get { 
			return 0.5f * (position + 1); 
		}
	}

	public LeverInfo (float pos, float deltaPos) {
		position = pos;
		deltaPosition = deltaPos;
	}
}

[System.Serializable]
public class LeverEvent : UnityEvent<LeverInfo> {}

public class Lever : MonoBehaviour {

	public Vector2 touchZoneSize = new Vector2(18, 20);
	public float length = 12;

	public LeverEvent onClick;

	Camera cam;
	bool clicked = false;
	Transform stick;
	float stickPrevPos;

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
				if (CheckTouchPosition (touchWorldPos)) {
					touch = Input.GetTouch (i);
					fingerId = touch.fingerId;
					stick.position = new Vector3 (transform.position.x, touchWorldPos.y);
					if (Mathf.Abs (stick.localPosition.y) > length)
						stick.localPosition = stick.localPosition.normalized * length;

					stickPrevPos = stick.localPosition.y;

					clicked = true;
				}
			}
		}

		if (clicked) {
			stick.position = new Vector2(transform.position.x, cam.ScreenToWorldPoint (touch.position).y);
			if (stick.localPosition.y > length)
				stick.localPosition = stick.localPosition.normalized * length;

			float delta = stick.localPosition.y - stickPrevPos;
			stickPrevPos = stick.localPosition.y;

			onClick.Invoke(new LeverInfo (stick.localPosition.y / length, delta / length));
		}

		if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) {
			//	stick.localPosition = Vector3.zero;
			clicked = false;
		}
	}

	void EditorControl () {
		if (Input.GetMouseButtonDown (0)) {
			Vector2 mouseWorldPos = cam.ScreenToWorldPoint (Input.mousePosition);
			if (CheckTouchPosition(mouseWorldPos)) {
				stick.position = new Vector3(transform.position.x, mouseWorldPos.y);
				if (stick.localPosition.magnitude > length)
					stick.localPosition = stick.localPosition.normalized * length;

				stickPrevPos = stick.localPosition.y;

				clicked = true;
			}
		}

		if (clicked) {
			stick.position = new Vector3(transform.position.x, cam.ScreenToWorldPoint (Input.mousePosition).y);
			if (Mathf.Abs (stick.localPosition.y) > length)
				stick.localPosition = stick.localPosition.normalized * length;

			float delta = stick.localPosition.y - stickPrevPos;
			stickPrevPos = stick.localPosition.y;

			onClick.Invoke(new LeverInfo (stick.localPosition.y / length, delta / length));
		}

		if (Input.GetMouseButtonUp (0)) {
			//	stick.localPosition = Vector3.zero;
			clicked = false;
		}
	}

	bool CheckTouchPosition(Vector2 v) {
		return (Mathf.Abs(v.x - transform.position.x) < touchZoneSize.x / 2 && 
				Mathf.Abs(v.y - transform.position.y) < touchZoneSize.y / 2);			
	}
}

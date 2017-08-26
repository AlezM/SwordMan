using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects, CustomEditor(typeof(Joystick))]
public class JoystickEditor : Editor {

	private Joystick joystick;

	public void OnEnable () {
		joystick = (Joystick)target;
	}

	void OnSceneGUI () {
		Handles.color = new Color (0, 0.1f, 1, 0.1f);
		Handles.DrawSolidDisc (joystick.transform.position, Vector3.forward, joystick.touchZoneSize);
		Handles.color = new Color (0, 1, 0, 0.15f);
		Handles.DrawSolidDisc (joystick.transform.position, Vector3.forward, joystick.radius);
	}
}

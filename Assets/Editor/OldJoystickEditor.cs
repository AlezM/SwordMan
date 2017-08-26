using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.AnimatedValues;

[CanEditMultipleObjects, CustomEditor(typeof(OldJoystick))]
public class OldJoystickEditor : Editor {
/*
	Joystick joystick;

	private SerializedProperty joystickType;
	private SerializedProperty cJoySize;
	private SerializedProperty cJoyRadius;

	private SerializedProperty vJoyLength;
	private SerializedProperty vJoySize;

	private SerializedProperty onClick;

	public void OnEnable () {
		joystick = (Joystick)target;
		joystickType = base.serializedObject.FindProperty ("joystickType");
		cJoySize = base.serializedObject.FindProperty ("cJoySize");
		cJoyRadius = base.serializedObject.FindProperty ("cJoyRadius");

		vJoyLength = base.serializedObject.FindProperty ("vJoyLength");
		vJoySize = base.serializedObject.FindProperty ("vJoySize");

		onClick = base.serializedObject.FindProperty ("onClick");
	}

	public override void OnInspectorGUI () {
		base.serializedObject.Update ();

	
		EditorGUILayout.PropertyField (joystickType);
		switch (joystick.joystickType) {
		case Joystick.JoystickType.CJoystick:	
			EditorGUILayout.PropertyField (cJoySize);
			EditorGUILayout.PropertyField (cJoyRadius);
			break;
		case Joystick.JoystickType.VJoystick:
			EditorGUILayout.PropertyField (vJoySize);
			EditorGUILayout.PropertyField (vJoyLength);
			break;

		}
		EditorGUILayout.Space ();
		EditorGUILayout.PropertyField (onClick);

		base.serializedObject.ApplyModifiedProperties ();
	}

	void OnSceneGUI () {
		float handleSize = HandleUtility.GetHandleSize (joystick.transform.position);

		switch (joystick.joystickType) {
		case Joystick.JoystickType.CJoystick:
			Handles.color = new Color (0, 0.1f, 1, 0.1f);
			Handles.DrawSolidDisc (joystick.transform.position, Vector3.forward, joystick.cJoySize);
			Handles.color = new Color (0, 1, 0, 0.15f);
			Handles.DrawSolidDisc (joystick.transform.position, Vector3.forward, joystick.cJoyRadius);
			break;

		case Joystick.JoystickType.VJoystick:
			Handles.DrawSolidRectangleWithOutline (new Rect((Vector2)joystick.transform.position - joystick.vJoySize/2, joystick.vJoySize), 
												   new Color (0, 0.1f, 1, 0.1f), new Color (0, 0.1f, 1, 0.2f));
			
			Handles.color = new Color (0, 1, 0);
			Handles.DrawLine (joystick.transform.position - Vector3.up * joystick.vJoyLength/2, joystick.transform.position + Vector3.up * joystick.vJoyLength/2);
			break;
		}
	}
	*/
}

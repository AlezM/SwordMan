using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects, CustomEditor(typeof(Lever))]
public class LeverEditor : Editor {

	Lever lever;

	public void OnEnable () {
		lever = (Lever)target;
	}

	void OnSceneGUI () {
		Handles.DrawSolidRectangleWithOutline (new Rect((Vector2)lever.transform.position - lever.touchZoneSize/2, lever.touchZoneSize), 
			new Color (0, 0.1f, 1, 0.1f), new Color (0, 0.1f, 1, 0.2f));

		Handles.color = new Color (0, 1, 0);
		Handles.DrawLine (lever.transform.position - Vector3.up * lever.length, lever.transform.position + Vector3.up * lever.length);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace GamePad {
	[CanEditMultipleObjects, CustomEditor(typeof(Button))]
	public class ButtonEditor : Editor {

		private Button joystick;

		public void OnEnable () {
			joystick = (Button)target;
		}

		void OnSceneGUI () {
			Handles.color = new Color (0, 1, 0, 0.15f);
			Handles.DrawSolidDisc (joystick.transform.position, Vector3.forward, joystick.touchZoneSize);
		}
	}
}
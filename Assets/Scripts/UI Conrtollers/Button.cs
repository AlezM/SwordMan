using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GamePad {
	public class Button : MonoBehaviour {

		public float size = 5;

		public ButtonEvent onClick;

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

		}

		void EditorControl () {

		}
	}
}
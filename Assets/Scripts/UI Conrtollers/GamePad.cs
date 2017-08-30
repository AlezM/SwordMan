using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GamePad {
	
//CLASSES
	public class JoystickInfo {
		public Vector2 position;
		public Vector2 deltaPosition;

		public float magnitude {
			get { return position.magnitude; }
		}

		public JoystickInfo () {
			position = Vector2.zero;
			deltaPosition = Vector2.zero;
		}

		public JoystickInfo (Vector2 pos, Vector2 deltaPos) {
			position = pos;
			deltaPosition = deltaPos;
		}
	}

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

//EVENTS
	[System.Serializable]
	public class JoystickEvent : UnityEvent<JoystickInfo> {}


	[System.Serializable]
	public class LeverEvent : UnityEvent<LeverInfo> {}
}

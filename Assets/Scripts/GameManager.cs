using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI () {
		GUI.TextField (new Rect (), (1f / Time.deltaTime).ToString (" ####") + " fps", new GUIStyle ());
	}
}

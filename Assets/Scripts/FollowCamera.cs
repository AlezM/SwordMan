using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {

	public Transform target;
	public Vector3 offset = Vector3.zero;
	public float speed = 15f;

	void Update () {
		if (target) {
			transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * speed);
		}	
	}
}

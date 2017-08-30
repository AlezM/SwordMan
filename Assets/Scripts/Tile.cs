using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Tile : MonoBehaviour {

	public Vector2 size;
	[Header("Collider offset:")]
	public float upOffset = 0;
	public float downOffset = 0;
	public float sideOffset = 0;

	SpriteRenderer sprite;
	BoxCollider2D coll;

	void OnEnable () {
		sprite = GetComponent<SpriteRenderer> ();
		coll = GetComponent<BoxCollider2D> ();
	}
		
	void OnValidate () {
		if (sprite && coll) {
			sprite.size = size;
			coll.size = size - new Vector2 (2 * sideOffset, upOffset + downOffset);
			coll.offset = new Vector2 (0, -0.5f * coll.size.y - upOffset);
		}
	}
}

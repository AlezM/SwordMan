using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlipper : MonoBehaviour {

	public Sprite normal;
	public Sprite fliped;

	public SpriteRenderer sprite;

	void Start () {
		if (!sprite)
			sprite = GetComponent<SpriteRenderer> ();
	}

	public void Flip (bool flip, int sort) {
		if (!flip) {
			sprite.sprite = normal;
			sprite.sortingOrder = sort;
		} else {
			sprite.sprite = fliped;
			sprite.sortingOrder = sort;
		}			
	}
}

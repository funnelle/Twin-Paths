using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBox : MonoBehaviour {
	private BoxCollider2D box;
	private SpriteRenderer boxSprite;

	void Start () {
		box = GetComponent<BoxCollider2D> ();
		boxSprite = GetComponent<SpriteRenderer> ();
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Knight") {
			//play destruction animation
			//disable trigger collider
			box.enabled = false;
			boxSprite.enabled = false;
			Debug.Log ("Box Broke");
		}
	}
}

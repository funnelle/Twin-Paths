using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLeg : MonoBehaviour {
	public GameObject leg;
	private SpriteRenderer rend;

	public Sprite leftLeg;
	public Sprite rightLeg;

	void Start() {
		rend = GetComponentInParent<SpriteRenderer> ();
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Knight") {
			if (leg.name.Contains("Left")) {
				rend.sprite = leftLeg;
			}
			if (leg.name.Contains("Right")) {
				rend.sprite = rightLeg;
			} 
			leg.SetActive(false);
		}
	}
}

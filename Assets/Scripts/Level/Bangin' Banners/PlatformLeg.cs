﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLeg : MonoBehaviour {
	BoxCollider2D leg;
	// Use this for initialization
	void Awake () {
		leg = GetComponent<BoxCollider2D> ();
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Knight") {
			//play destruction animation
			//disable trigger collider
			leg.enabled = false;
			Debug.Log ("Leg Broke");
		}
	}
}

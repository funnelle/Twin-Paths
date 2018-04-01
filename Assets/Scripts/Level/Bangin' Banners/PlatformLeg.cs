using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLeg : MonoBehaviour {
	public GameObject leg;

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Knight") {
			//play destruction animation
			leg.SetActive(false);
		}
	}
}

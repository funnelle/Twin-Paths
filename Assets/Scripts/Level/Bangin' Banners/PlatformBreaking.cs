using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBreaking : MonoBehaviour {
	private BoxCollider2D leftLeg;
	private BoxCollider2D rightLeg;
	private Rigidbody2D rb2d;

	// Use this for initialization
	void Awake () {
		rb2d = GetComponent<Rigidbody2D> ();
		leftLeg = GetComponentInChildren<BoxCollider2D> ();
		rightLeg = GetComponentInChildren<BoxCollider2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if ((leftLeg.isActiveAndEnabled == false) && (rightLeg.isActiveAndEnabled == false)) {
			//play destruction animation
			Debug.Log("Platform Broke!");
			rb2d.bodyType = RigidbodyType2D.Dynamic;
		}
	}
}

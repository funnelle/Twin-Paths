using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBreaking : MonoBehaviour {
	public GameObject leftLeg;
	public GameObject rightLeg;
	private Rigidbody2D rb2d;

	// Use this for initialization
	void Awake () {
		rb2d = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if ((leftLeg.activeSelf == false) && (rightLeg.activeSelf == false)) {
			//play destruction animation
			rb2d.bodyType = RigidbodyType2D.Dynamic;
			PlatformEffector2D effector = GetComponent<PlatformEffector2D> ();
			effector.colliderMask ^= (1 << 13);
		}
	}
}

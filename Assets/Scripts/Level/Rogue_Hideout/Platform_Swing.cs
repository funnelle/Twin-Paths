using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Swing : MonoBehaviour {
	private Rigidbody2D platform;
	public GameObject knight;

	public float hitForce;

	private bool hit = false;

	// Use this for initialization
	void Start () {
		platform = GetComponent<Rigidbody2D> ();
	}

	void OnTriggerEnter2D (Collider2D coll) {
		if (hit == false) {
			if (coll.gameObject.name.Contains ("KnightAttackBox")) {
				Debug.Log ("Attack coming");
				hit = true;
				if (knight.GetComponent<KnightController> ().facingRight == false) {
					platform.AddForce (new Vector2 (-1 * (hitForce), 0));
					Debug.Log ("Hit left!");
				}
				if (knight.GetComponent<KnightController> ().facingRight == true) {
					platform.AddForce (new Vector2 (hitForce, 0));
					Debug.Log ("Hit right!");
				}
			}
		}
		hit = false;
	}
}

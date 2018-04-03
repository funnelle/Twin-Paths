using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBreaking : MonoBehaviour {
	public GameObject leftLeg;
	public GameObject rightLeg;
	private Rigidbody2D rb2d;

	//Disable collisions
	GameObject knight;
	GameObject rogue;

	// Use this for initialization
	void Awake () {
		rb2d = GetComponent<Rigidbody2D> ();
		knight = GameObject.Find ("RoguePlayer");
		rogue = GameObject.Find ("KnightPlayer");
	}
	
	// Update is called once per frame
	void Update () {
		if ((leftLeg.activeSelf == false) && (rightLeg.activeSelf == false)) {
			//play destruction animation
			rb2d.bodyType = RigidbodyType2D.Dynamic;
			Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), knight.GetComponent<BoxCollider2D>());
			Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), rogue.GetComponent<BoxCollider2D>());
		}
	}
}

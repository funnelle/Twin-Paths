using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBreaking : MonoBehaviour {
	public GameObject leftLeg;
	public GameObject rightLeg;
	public float destroyTime;

	private Animator anim;
	private BoxCollider2D plat;
	private SpriteRenderer platSprite;

	//Disable collisions
	GameObject knight;
	GameObject rogue;

	// Use this for initialization
	void Awake () {
		knight = GameObject.Find ("RoguePlayer");
		rogue = GameObject.Find ("KnightPlayer");
		anim = GetComponent<Animator> ();
		plat = GetComponent<BoxCollider2D> ();
		platSprite = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if ((leftLeg.activeSelf == false) && (rightLeg.activeSelf == false)) {
			anim.SetBool ("Destroy", true);
			plat.enabled = false;
			StartCoroutine (DestroyPlatform());
			this.enabled = false;
		}
	}

	private IEnumerator DestroyPlatform() {
		yield return new WaitForSeconds (destroyTime);
		anim.SetBool ("Destroy", false);
		platSprite.enabled = false;
	}
}

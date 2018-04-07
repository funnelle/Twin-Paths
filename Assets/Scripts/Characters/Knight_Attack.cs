﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight_Attack : MonoBehaviour {
	private BoxCollider2D sword;
	private KnightController knight;
	private Animator anim;

	public float attackTime;
	public GameObject rogue;

	// Used for interacting with hearts in UI Overlay
	public GameObject UIOverlay;
	public GameObject heartFull;
	public GameObject heartEmpty;

	void Awake() {
		sword = GetComponent<BoxCollider2D> ();
		anim = GetComponentInParent<Animator> ();
		knight = GetComponentInParent<KnightController> ();
		rogue = GameObject.Find("RoguePlayer");

		UIOverlay = GameObject.Find("UIOverlay");

		heartFull = UIOverlay.transform.Find("Hearts/RHeartFull1").gameObject;
		heartEmpty = UIOverlay.transform.Find("Hearts/RHeartEmpty1").gameObject;
	}

	public void KnightAttack() {
		StartCoroutine (AttackLength (attackTime));
	}

	private IEnumerator AttackLength (float attackTime) {
		yield return new WaitForSeconds (attackTime);
		anim.SetBool ("Attack", false);
		knight.canAttack = true;
		knight.allowedMovement = true;
	}

	void OnTriggerEnter2D(Collider2D coll) {
		RogueController damage = rogue.GetComponent<RogueController> ();
		if (coll.gameObject.tag == "Rogue") {
			heartEmpty.SetActive(true);
			heartFull.SetActive(false);
			
			damage.TakeDamage ();
		}
	}
}

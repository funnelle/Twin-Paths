using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight_Attack : MonoBehaviour {
	BoxCollider2D sword;
	public float attackTime;
	Animator anim;
	GameObject rogue;

	void Awake() {
		sword = GetComponent<BoxCollider2D> ();
		anim = GetComponentInParent<Animator> ();
		rogue = GameObject.Find ("RoguePlayer");
	}

	public void KnightAttack() {
		sword.enabled = true;
		StartCoroutine (AttackLength (attackTime));
	}

	private IEnumerator AttackLength (float attackTime) {
		yield return new WaitForSeconds (attackTime);
		sword.enabled = false;
		anim.SetBool ("Attack", false);
	}

	void OnTriggerEnter2D(Collider2D coll) {
		Debug.Log ("Hit");
		RogueController damage = rogue.GetComponent<RogueController> ();
		if (coll.gameObject.tag == "Rogue") {
			Debug.Log ("Deal damage to Rogue");
			damage.TakeDamage ();
		}
	}
}

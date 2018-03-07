using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rogue_Attack : MonoBehaviour {
	BoxCollider2D knife;
	public float attackTime;
	GameObject knight;

	void Awake() {
		knife = GetComponent<BoxCollider2D> ();
		knight = GameObject.Find ("KnightPlayer");
	}

	public void KnifeAttack() {
		knife.enabled = true;
		StartCoroutine (AttackLength (attackTime));
	}

	private IEnumerator AttackLength (float attackTime) {
		yield return new WaitForSeconds (attackTime);
		knife.enabled = false;
	}

	void OnTriggerEnter2D(Collider2D coll) {
		KnightController damage = knight.GetComponent<KnightController> ();
		if (coll.gameObject.tag == "Knight") {
			damage.TakeDamage ();
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rogue_Attack : MonoBehaviour {
	BoxCollider2D knife;
	public float attackTime;

	void Awake() {
		knife = GetComponent<BoxCollider2D> ();
	}

	public void KnifeAttack() {
		knife.enabled = true;
		StartCoroutine (AttackLength (attackTime));
	}

	private IEnumerator AttackLength (float attackTime) {
		yield return new WaitForSeconds (attackTime);
		knife.enabled = false;
	}
}

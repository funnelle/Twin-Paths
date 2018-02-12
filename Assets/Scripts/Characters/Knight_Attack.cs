using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight_Attack : MonoBehaviour {
	BoxCollider2D sword;
	public float attackTime;

	void Awake() {
		sword = GetComponent<BoxCollider2D> ();
	}

	public void KnightAttack() {
		sword.enabled = true;
		StartCoroutine (AttackLength (attackTime));
	}

	private IEnumerator AttackLength (float attackTime) {
		yield return new WaitForSeconds (attackTime);
		sword.enabled = false;
	}
}

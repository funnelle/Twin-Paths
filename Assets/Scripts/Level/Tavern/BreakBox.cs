using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBox : MonoBehaviour {
	private BoxCollider2D box;
	private SpriteRenderer boxSprite;
	private Animator anim;
	public float destructionTime;

	void Start () {
		box = GetComponent<BoxCollider2D> ();
		boxSprite = GetComponent<SpriteRenderer> ();
		anim = GetComponent<Animator> ();
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Knight") {
			//play destruction animation
			anim.SetBool("Destroy", true);
			box.enabled = false;
			StartCoroutine(DestroyBox ());
		}
	}

	private IEnumerator DestroyBox() {
		yield return new WaitForSeconds (destructionTime);
		anim.SetBool("Destroy", false);
		boxSprite.enabled = false;
	}
}

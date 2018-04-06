using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KnightController : MonoBehaviour {
	private Rigidbody2D rb2d;
	private Animator anim;
	private GameObject knight;

	//general character knowledge
	public bool facingRight = false;
	float moveHorizontal;
	public float maxSpeed;
	public float health;
	public float jumpForce;
	public bool allowedMovement = true;
	public bool canAttack = true;

	//Button variables
	public string horizontalButton = "Horizontal_p2";
	public string jumpButton = "Jump_p2";
	public string attackButton = "Attack_p2";
	public string damageButton = "Ouch_p2";

	void Start() {
		rb2d = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		knight = GameObject.Find ("KnightPlayer");

	}

	void Update() {
		//attack
		if (Input.GetButtonDown (attackButton) && canAttack) {
			canAttack = false;
			allowedMovement = false;
			Knight_Attack attack = gameObject.GetComponentInChildren<Knight_Attack> ();
			anim.SetBool ("Attack", true);
			attack.KnightAttack ();
		}

		//Test damage taken
		if (Input.GetButtonDown(damageButton)) {
			TakeDamage ();
		}
	}

	void FixedUpdate() {
		if (allowedMovement == true) {
			moveHorizontal = Input.GetAxis (horizontalButton);

			rb2d.velocity = new Vector2 (moveHorizontal * maxSpeed, rb2d.velocity.y);

			if (moveHorizontal > 0 && !facingRight) {
				Flip ();
			} else if (moveHorizontal < 0 && facingRight) {
				Flip ();
			}
		}
		//movement animation
		if (Mathf.Abs (rb2d.velocity.x) > 0.01f) {
			anim.SetBool ("Walk", true);
		} else {
			anim.SetBool ("Walk", false);
		}
	}

	void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public void TakeDamage() {
		SoundManagerScript.PlaySound ("Knight Damage");
		health -= 1;

		if (health <= 0) {
			allowedMovement = false;
			canAttack = false;
			rb2d.AddForce (new Vector2 (rb2d.velocity.x, (jumpForce/2)));
			knight.GetComponent<BoxCollider2D> ().enabled = false;
		}
	}

	void OnBecameInvisible() {
		Destroy (knight);
	}

	void WalkSound() {
		SoundManagerScript.PlaySound ("Knight Footstep");
	}
}
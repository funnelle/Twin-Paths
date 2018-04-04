using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RogueController : MonoBehaviour {
	private Rigidbody2D rb2d;
	private Animator anim;
	private GameObject rogue;
	private GameObject knight;

	//general character knowledge
	bool facingRight = true;
	public float maxSpeed;
	public float health;
	public LayerMask wall;
	public bool allowedMovement = true;
	float moveHorizontal;

	//attacking variables 
	public LayerMask Enemy;
	public float attackTime;
	public bool canAttack = true;

	//dash variables
	bool dash = false;
	public bool canDash = true;
	public Vector2 dashDistance;
	public float dashTime;
	public float dashDelay;
	public bool delayDone = true;

	//jumping variables
	public bool grounded = false;
	float groundRadius = 0.1f;
	public Transform groundCheck;
	public LayerMask whatIsGround;
	public float jumpForce;

	//Button variables
	public string horizontalButton = "Horizontal_P1";
	public string jumpButton = "Jump";
	public string dashButton = "Dash";
	public string attackButton = "Attack";
	public string damageButton = "Ouch";

	void Start() {
		rb2d = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		rogue = GameObject.Find ("RoguePlayer");
		knight = GameObject.Find ("KnightPlayer");
	}

	void Update() {
		//determines if Rogue can Dash
		if (grounded && delayDone) {
			anim.SetBool ("Jump", false);
			canDash = true;
		}

		//get jump input
		if ((dash == false) && grounded && allowedMovement && Input.GetButtonDown(jumpButton)) {
			if (Mathf.Abs (moveHorizontal) < 0.01f) {
				anim.SetBool ("Jump", true);
			}
			SoundManagerScript.PlaySound ("Rogue Jump");
			rb2d.AddForce (new Vector2 (0, jumpForce));
		}

		//attack
		if (Input.GetButtonDown (attackButton) && (canAttack == true) && allowedMovement) {
			anim.SetBool ("Attack", true);
		}
	}

	void FixedUpdate() {
		//ground check
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);

		//horizontal movement
		if (allowedMovement == true) {
			moveHorizontal = Input.GetAxis (horizontalButton);

			rb2d.velocity = new Vector2 (moveHorizontal * maxSpeed, rb2d.velocity.y);

			if (Mathf.Abs(rb2d.velocity.x) > 0.01f) {
				anim.SetBool ("Walk", true);
			}
			else {
				anim.SetBool ("Walk", false);
			}
				
			if (moveHorizontal > 0 && !facingRight) {
				Flip ();
			} else if (moveHorizontal < 0 && facingRight) {
				Flip ();
			}
		}

		//dash
		if (Input.GetButtonDown(dashButton) && canDash && allowedMovement && dash != true) {
			canAttack = false;
			anim.SetBool ("Attack", false);
			rb2d.velocity = new Vector2 (0,0);
			allowedMovement = false;
			canDash = false;
			delayDone = false;
			anim.SetBool ("Dash", true);
			Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), knight.GetComponent<BoxCollider2D>());
			Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), knight.GetComponentInChildren<BoxCollider2D>());
			dash = true;
			if (facingRight) {
				StartCoroutine (MoveOverSeconds (rogue, rb2d.position + dashDistance, dashTime));
			}
			else if (!facingRight) {
				StartCoroutine (MoveOverSeconds (rogue, rb2d.position - dashDistance, dashTime));
			}
		}

		//Raycast to check for map edges
		Vector2 position = transform.position;
		Vector2 direction = Vector2.right;
		if (facingRight) {
			direction = Vector2.right;
		}
		else if (!facingRight) {
			direction = Vector2.left;
		}
		float distance = 0.2f;
		RaycastHit2D hit = Physics2D.Raycast (position, direction, distance, wall);

		if (hit.collider != null) {
			dash = false;
		}
	}
		
	void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	//Interpolates Point A to Point B and translates Rogue between those points in a certain time. Results in Dash
	public IEnumerator MoveOverSeconds (GameObject objectToMove, Vector2 end, float seconds) {
		float elapsedTime = 0;
		Vector2 startingPos = objectToMove.transform.position;
		rb2d.gravityScale = 0.0f;
		bool dashSoundPlayable = true;
		while (elapsedTime < seconds) {
			if ((elapsedTime > 0) && (dashSoundPlayable)) {
				SoundManagerScript.PlaySound ("Rogue Dash V3");
				dashSoundPlayable = false;
			}
			objectToMove.transform.position = Vector2.Lerp(startingPos, end, (elapsedTime / seconds));
			elapsedTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();
			if (dash == false) {
				break;
			}
		}
		if (dash == false) {
			objectToMove.transform.position = transform.position;
		} else {
			objectToMove.transform.position = end;
			dash = false;
		}
		anim.SetBool ("Dash", false);
		rb2d.gravityScale = 1.0f;
		Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), knight.GetComponent<BoxCollider2D>(), false);
		Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), knight.GetComponentInChildren<BoxCollider2D>(), false);
		allowedMovement = true;
		canAttack = true;
		StartCoroutine (DelayDash (dashDelay));
	}

	public void Attack() {
		//attack
		canAttack = false;
		canDash = false;
		Debug.Log ("Attacking");
		SoundManagerScript.PlaySound ("Rogue Attack");
		StartCoroutine(AttackLength (attackTime));
		anim.SetBool ("Attack", false);
	}

	//Length of time the Rogue has his blade out and can hit his attack
	private IEnumerator AttackLength (float attackTime) {
		//Attack with raycast
		Vector2 position = transform.position;
		position -= new Vector2 (0, 0.25f);
		Vector2 direction = Vector2.right;
		if (facingRight) {
			direction = Vector2.right;
		} else if (!facingRight) {
			direction = Vector2.left;
		}
		float distance = 0.4f;
		RaycastHit2D hit = Physics2D.Raycast (position, direction, distance, Enemy);

		if (hit.collider != null) {
			KnightController knightHit = knight.GetComponent<KnightController> ();
			knightHit.TakeDamage ();
		}

		yield return new WaitForSeconds (attackTime);

		canAttack = true;
		canDash = true;
	}

	//Delay between dashes
	private IEnumerator DelayDash(float delayTime) {
		yield return new WaitForSeconds (delayTime);
		delayDone = true;
	}

	//When hit, take damage
	public void TakeDamage() {
		//play death sound
		//play death animation
		canAttack = false;
		allowedMovement = false;
		if (dash != true) {
			health -= 1;
			if (health <= 0) {
				rb2d.AddForce (new Vector2 (rb2d.velocity.x, (jumpForce / 2)));
				rogue.GetComponent<BoxCollider2D> ().enabled = false;
			}
		}
	}

	//If the Rogue leaves the screen, kill the rogue
	void OnBecameInvisible() {
		Destroy (rogue);
	}

	//Called by Animator to play walk sound
	void WalkSound() {
		SoundManagerScript.PlaySound ("Rogue Footstep");
	}
}
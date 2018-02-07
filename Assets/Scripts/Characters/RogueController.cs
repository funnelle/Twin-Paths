using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueController : MonoBehaviour {
	private Rigidbody2D rb2d;
	private Animator anim;
	private GameObject rogue;

	//general character knowledge
	bool facingRight = true;
	public float maxSpeed = 10f;
	public LayerMask wall;

	//dash variables
	bool dash = false;
	bool canDash = true;
	bool movementDisabled = false;
	public Vector2 dashDistance;
	public float dashTime;

	//jumping variables
	public bool grounded = false;
	float groundRadius = 0.2f;
	public Transform groundCheck;
	public LayerMask whatIsGround;
	public float jumpForce;

	//Button variables
	public string horizontalButton = "Horizontal_P1";
	public string jumpButton = "Jump";
	public string dashButton = "Dash";
	public string attackButton = "Attack";

	void Start() {
		rb2d = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		rogue = GameObject.Find ("RoguePlayer");

	}

	void Update() {
		//get dash input
		if (Input.GetButtonDown(dashButton) && canDash && dash != true) {
			rb2d.velocity = new Vector2 (0,0);
			movementDisabled = true;
			canDash = false;
			anim.SetBool ("Dash", true);
			dash = true;
			if (facingRight) {
				StartCoroutine (MoveOverSeconds (rogue, rb2d.position + dashDistance, dashTime));
			}
			else if (!facingRight) {
				StartCoroutine (MoveOverSeconds (rogue, rb2d.position - dashDistance, dashTime));
			}
			movementDisabled = false;
		}

		if (grounded) {
			anim.SetBool ("Jump", false);
			canDash = true;
		}

		//get jump input
		if ((dash == false) && grounded && Input.GetButtonDown(jumpButton)) {
			anim.SetBool ("Ground", false);
			anim.SetBool ("Jump", true);
			SoundManagerScript.PlaySound ("Rogue Jump");
			rb2d.AddForce (new Vector2 (0, jumpForce));
		}

		//Raycast to check for objects
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

		//attack
		if (Input.GetButtonDown(attackButton)) {
			Rogue_Attack attack = gameObject.GetComponentInChildren<Rogue_Attack> ();
			attack.KnifeAttack ();
		}
	}

	void FixedUpdate() {
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("Ground", grounded);

		if (movementDisabled == false) {
			float moveHorizontal = Input.GetAxis (horizontalButton);

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
	}
		
	void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

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
	}
}
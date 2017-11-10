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

	//dash variables
	bool dash = false;
	public Vector2 dashDistance;
	public float dashTime;

	//jumping variables
	bool grounded = false;
	float groundRadius = 0.2f;
	public Transform groundCheck;
	public LayerMask whatIsGround;
	public float jumpForce;

	//Button variables
	public string horizontalButton = "Horizontal_P1";
	public string jumpButton = "Jump";
	public string dashButton = "Dash";

	void Start() {
		rb2d = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		rogue = GameObject.Find ("RoguePlayer");
	}

	void Update() {
		//get dash input
		if (Input.GetButtonDown(dashButton) && dash != true) {
			anim.SetBool ("Dash", true);
			dash = true;
			if (facingRight) {
				StartCoroutine (MoveOverSeconds (rogue, rb2d.position + dashDistance, dashTime));
			}
			else if (!facingRight) {
				StartCoroutine (MoveOverSeconds (rogue, rb2d.position - dashDistance, dashTime));
			}
			dash = false;
		}

		//get jump input
		if ((dash == false) && grounded && Input.GetButtonDown(jumpButton)) {
			anim.SetBool ("Ground", false);
			rb2d.AddForce (new Vector2 (0, jumpForce));
		}
	}

	void FixedUpdate() {
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("Ground", grounded);

		float moveHorizontal = Input.GetAxis (horizontalButton);

		rb2d.velocity = new Vector2 (moveHorizontal * maxSpeed, rb2d.velocity.y);

		if (moveHorizontal > 0 && !facingRight) {
			Flip ();
		}
		else if (moveHorizontal < 0 && facingRight) {
			Flip ();
		}
	}

	void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public IEnumerator MoveOverSeconds (GameObject objectToMove, Vector2 end, float seconds)
	{
		float elapsedTime = 0;
		Vector2 startingPos = objectToMove.transform.position;
		rb2d.gravityScale = 0.0f;
		while (elapsedTime < seconds) {
			objectToMove.transform.position = Vector2.Lerp(startingPos, end, (elapsedTime / seconds));
			elapsedTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		objectToMove.transform.position = end;
		anim.SetBool ("Dash", false);
		rb2d.gravityScale = 1.0f;
	}
		

}
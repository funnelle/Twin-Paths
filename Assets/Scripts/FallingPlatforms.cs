using UnityEngine;
using System.Collections;

//Three things to reference
//1. OnCollisionEnter2D: Called when this collider and another rigidbody touch each other
//2. OnCollisionStay2D: Called once per frame a collider on another object is touching this collider
//3. OnCollisionExit2D: Sent when a collider on another object stops touching this object's collider
//What is done
//1. OnCollisionEnter2D: Enable rigidbody/give mass/set gravity scale from 0 to 1
//2. OnCollisionStay2D: Determine if player is still on platform, distinguish between new collisions
//3. OnCollisionExit2D: Determine when player has left the platform
//4. Determine when platform has contacted the ground and reset rigidbody/mass/gravity scale


public class FallingPlatforms : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float fallDelay;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    
    void OnCollisionEnter2D(Collision2D col)
	{
        if (col.collider.CompareTag("Player"))
        {
            StartCoroutine(Fall());
        }
	}
    IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay);
		rb2d.isKinematic = false;
        yield return 0;
    }
    
}

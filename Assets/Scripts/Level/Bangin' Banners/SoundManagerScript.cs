using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour {

	public static AudioClip rogueDashSound, rogueWalkSound, rogueJumpSound, rogueAttackSound;
	public static AudioClip knightWalkSound, knightDamageSound, knightAttackSound;
	static AudioSource audioSrc;

	// Use this for initialization
	void Start () {
		rogueDashSound = Resources.Load<AudioClip> ("Audio/Character Sounds/Rogue/Rogue Dash V4");
		rogueWalkSound = Resources.Load<AudioClip> ("Audio/Character Sounds/Rogue/Rogue Footstep");
		rogueJumpSound = Resources.Load<AudioClip> ("Audio/Character Sounds/Rogue/Rogue Jump");
		rogueAttackSound = Resources.Load<AudioClip> ("Audio/Character Sounds/Rogue/Rogue Attack Rev");

		knightWalkSound = Resources.Load<AudioClip> ("Audio/Character Sounds/Knight/Knight Footstep");
		knightDamageSound = Resources.Load<AudioClip> ("Audio/Character Sounds/Knight/Knight First Hit");

		audioSrc = GetComponent<AudioSource> ();
	}

	public static void PlaySound (string clip) {
		switch (clip) {
		case "Rogue Dash V3":
			audioSrc.PlayOneShot (rogueDashSound);
			break;
		case "Rogue Footstep":
			audioSrc.PlayOneShot (rogueWalkSound);
			break;
		case "Rogue Jump":
			audioSrc.PlayOneShot (rogueJumpSound);
			break;
		case "Rogue Attack":
			audioSrc.PlayOneShot (rogueAttackSound);
			break;
		case "Knight Footstep":
			audioSrc.PlayOneShot (knightWalkSound);
			break;
		case "Knight Damage":
			audioSrc.PlayOneShot (knightDamageSound);
			break;
		}
	}
}

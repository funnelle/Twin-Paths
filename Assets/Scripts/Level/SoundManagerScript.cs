using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour {

	public static AudioClip rogueDashSound, rogueWalkSound, rogueJumpSound;
	static AudioSource audioSrc;

	// Use this for initialization
	void Start () {
		rogueDashSound = Resources.Load<AudioClip> ("Audio/Character Sounds/Rogue/Rogue Dash V4");
		rogueWalkSound = Resources.Load<AudioClip> ("Audio/Character Sounds/Rogue/Rogue Footstep");
		rogueJumpSound = Resources.Load<AudioClip> ("Audio/Character Sounds/Rogue/Rogue Jump");

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
		}
	}
}

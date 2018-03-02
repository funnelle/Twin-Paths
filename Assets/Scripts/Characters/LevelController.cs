using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

	public Scene currentScene;

	public GameObject knight;
	public GameObject rogue;

	public bool kRPlayed;

	// Use this for initialization
	void Awake () {
		
		Scene currentScene = SceneManager.GetActiveScene ();

		if (currentScene == "Bangin' Banners") {
			try {
				knight = GameObject.Find ("KnightPlayer");
			}
			catch (NullReferenceException e){
				kRPlayed = false;
			}

		}

	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void DGCLevel () {

	}
}

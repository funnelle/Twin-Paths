using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.VersionControl;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

	// Integers
	public int Delay = 3;
	public int SceneIndex = 0;
	
	// Button Variables
	public string menuButton = "Menu";
	
	// Components
	public GameObject UIOverlay;
	public GameObject PauseScreen;
	public GameObject VictoryScreen;
	public GameObject Knight;
	public GameObject Rogue;
	
	// Bools
	private bool isPauseShowing;
	private bool KnightWin;
	private bool RogueWin;

	private void Awake()
	{
		Knight = GameObject.Find("KnightPlayer");
		Rogue = GameObject.Find("RoguePlayer");
	}

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (SceneManager.GetActiveScene().name == "Throne_Room" || SceneManager.GetActiveScene().name == "Rogue_Hideout")
		{
			KnightWin = GameObject.Find("LevelChanger").GetComponent<RKLevelController>().KnightWin;
			RogueWin = GameObject.Find("LevelChanger").GetComponent<RKLevelController>().RogueWin;
		}

		if (Input.GetButtonDown(menuButton))
		{
			isPauseShowing = !isPauseShowing;
			PauseScreen.SetActive(isPauseShowing);
			Knight.GetComponent<KnightController>().allowedMovement = false;
			Rogue.GetComponent<RogueController>().allowedMovement = false;
		}

		if (!isPauseShowing)
		{
			if (Knight != null)
			{
				Knight.GetComponent<KnightController>().allowedMovement = true;
			}

			if (Rogue != null)
			{
				Rogue.GetComponent<RogueController>().allowedMovement = true;
			}
		}

		if (KnightWin || RogueWin)
		{
			VictoryScreen.SetActive(true);

			if (Knight != null)
			{
				Knight.GetComponent<KnightController>().allowedMovement = false;

			}
			if (Rogue != null)
			{
				Rogue.GetComponent<RogueController>().allowedMovement = false;
			}
			
			Invoke("BackToMenu", Delay);
		}
	}

	void BackToMenu()
	{
		SceneManager.LoadScene(SceneIndex);
	}
}

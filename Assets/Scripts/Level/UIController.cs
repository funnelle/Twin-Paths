using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.VersionControl;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour {

	// Button Variables
	public string menuButton = "Menu";
	
	// Components
	public GameObject UIOverlay;
	public GameObject PauseScreen;
	public GameObject VictoryScreen;
	
	// Bools
	private bool isPauseShowing;
	public bool KnightWin;
	public bool RogueWin;
	
	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{

		KnightWin = GameObject.Find("LevelChanger").GetComponent<RKLevelController>().KnightWin;
		RogueWin = GameObject.Find("LevelChanger").GetComponent<RKLevelController>().RogueWin;

		if (Input.GetButtonDown(menuButton))
		{
			isPauseShowing = !isPauseShowing;
			PauseScreen.SetActive(isPauseShowing);
		}

		if (KnightWin || RogueWin)
		{
			VictoryScreen.SetActive(KnightWin || RogueWin);
		}
	}
}

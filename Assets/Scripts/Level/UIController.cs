using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

	// Button Variables
	public string menuButton = "Menu";
	
	// Components
	private GameObject UIOverlay;
	
	// Use this for initialization
	void Start () {
		
		UIOverlay = GameObject.Find("UIOverlay");
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

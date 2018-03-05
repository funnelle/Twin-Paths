using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

	private Scene _currentScene;

	private string _currentSceneName, _targetSceneName;
	private List<string> _rkScenesList;

	private int _currentSceneIndex;

	public GameObject Knight;
	public GameObject Rogue;

	public bool RogueWin = false;
	public bool KnightWin = false;

	// Use this for initialization
	void Awake () {
		
		_currentScene = SceneManager.GetActiveScene ();
		_currentSceneName = _currentScene.name;
		
		_rkScenesList = new List<string> ();
		
		// List will contain 5 scene names when game is completed -> REMEMBER TO ADD AS LISTED, ORDER MATTERS
		_rkScenesList.Add("Rogue_Hideout");
		// _rkScenesList.Add("Tavern");
		// _rkScenesList.Add("Courtyard");
		_rkScenesList.Add("Bangin' Banners");
		// _rkScenesList.Add("Throneroom");

		_currentSceneIndex = _rkScenesList.FindIndex(s => s.Equals(_currentSceneName));

		Knight = GameObject.Find ("KnightPlayer");
		Rogue = GameObject.Find ("RoguePlayer");
	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		// If Knight is destroyed -> Progress down/right in list
		if (Knight == null)
		{
			if (InSceneRange(_rkScenesList, _currentSceneIndex))
			{
				_targetSceneName = _rkScenesList[_currentSceneIndex + 1];
				SceneManager.LoadScene(_targetSceneName, LoadSceneMode.Single);
				Debug.Log("Scene loading: " + _targetSceneName);
			}
			else
			{
				RogueWin = true;
			}
			

		}
		
		// If Rogue is destroyed -> Progress up/left in list
		if (Rogue == null)
		{
			if (InSceneRange(_rkScenesList, _currentSceneIndex))
			{
				_targetSceneName = _rkScenesList[_currentSceneIndex - 1];
				SceneManager.LoadScene(_targetSceneName, LoadSceneMode.Single);
				Debug.Log("Scene loading: " + _targetSceneName);
			}
			else
			{
				KnightWin = true;
			}

		}
	}

	// Used to check if moving left or right in list will throw an error, tells us if we're at edge or not
	private bool InSceneRange (List<string> sceneList, int sceneIndex)
	{
		try
		{
			if ((Convert.ToBoolean(sceneList[sceneIndex + 1])) | (Convert.ToBoolean(sceneList[sceneIndex - 1])))
			{
				return false;
			}
		}
		catch (ArgumentOutOfRangeException e)
		{
			Debug.Log("Must be at the end of stages (left or right)");
		}

		return true;
	}
}

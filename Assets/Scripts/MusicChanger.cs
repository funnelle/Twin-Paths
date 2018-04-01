using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicChanger : MonoBehaviour {

	private List<string> _rkScenesList;
	
	private Scene _currentScene;
	private string _currentSceneName;

	public GameObject BGM;
	public AudioSource _musicManager;

	private GameObject FBGM;
	private AudioSource _finalMusic;

	//private AudioClip _currentAudioClip;
	//public AudioClip BattleTheme;
	//public AudioClip FinalBattleTheme;

	private static bool AudioBegin = false;
	
	// On Awake
	void Awake()
	{
		// List will contain 5 scene names when game is completed -> REMEMBER TO ADD AS LISTED, ORDER MATTERS
		// Reference list of levels:
		// "Rogue_Hideout"
		// "Tavern"
		// "Courtyard"
		// "Bangin' Banners"
		// "Throne_Room"

		_currentScene = SceneManager.GetActiveScene();
		_currentSceneName = _currentScene.name;

		_musicManager = GetComponent<AudioSource>();
		
		// Potentially useful code incase of rework:
		//BattleTheme = Resources.Load<AudioClip>("Resources/Audio/Level Music/Battle_Theme_Final");
		//FinalBattleTheme = Resources.Load<AudioClip>("Resources/Audio/Level Music/Final_Fight_Draft1");
		
		BGM = GameObject.Find("BackgroundMusic");
		
		if (!AudioBegin)
		{
			_musicManager.Play();
			DontDestroyOnLoad(BGM);
			AudioBegin = true;
		}
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		_currentScene = SceneManager.GetActiveScene();
		_currentSceneName = _currentScene.name;
		
		if (_currentSceneName == "Throne_Room" || _currentSceneName == "Rogue_Hideout" || _currentSceneName == "StartScreen")
		{
			_musicManager.Pause();
		}

		else
		{
			_musicManager.UnPause();
		}
	}
}


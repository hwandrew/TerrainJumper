﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	string currentScene;
    int levelNum = 1;

	// Use this for initialization
	void Awake () {
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			DontDestroyOnLoad(this);
			instance = this;
		}

        currentScene = "Level_1";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ResetLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void LoadNextLevel()
	{
        string[] split = currentScene.Split('_');
        int lvlNum = int.Parse(split[1]);
        lvlNum++;
        string newLevel = split[0] + lvlNum.ToString();
        SceneManager.LoadScene(newLevel);
        currentScene = split[0] + '_' + lvlNum.ToString();
	}
}
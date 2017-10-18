using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * Levels are split into two parts/scenes -- animation scene and playable scene
 * 
 * The animation scene is a quick camera animation that shows the level. The animation
 * lasts 3 seconds and pauses for two seconds before the playable scene is then loaded
 */

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	public PauseMenuManager pauseInstance;
	public Coroutine levelLoader;

	GameObject flyThrough;
	GameObject mainCam;
	GameObject player;
	string currentScene;
	int resetCounter = 0;

	// Use this for initialization
	void Awake () {
        // make this a singleton
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
		StartCoroutine(LoadLevel("Level1"));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /*
     * Reset the current level by reloading the scene
     */
	public void ResetLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

    /*
     * Loads the next animation-level taken from "currentScene" string, then calls a coroutine
     * to load the playable level after the animation time is finished
     */
	public void LoadNextLevel()
	{
        string[] split = currentScene.Split('_');
        int lvlNum = int.Parse(split[1]);
        lvlNum++;
        string newLevel = split[0] + lvlNum.ToString();
        SceneManager.LoadScene(newLevel + "Anim");
        currentScene = split[0] + '_' + lvlNum.ToString();
		levelLoader = StartCoroutine(LoadLevel(newLevel));
	}

    /*
     * Waits 5 seconds so that animation level completes, then loads the actual level
     */
	private IEnumerator LoadLevel(string level)
	{
		// yield return null;
		pauseInstance.enabled = false;
		yield return new WaitForSeconds(5);
		SceneManager.LoadScene(level);
		pauseInstance.enabled = true;
	}

    // Sets currentScene
	public void SetCurrentLevel(string newSceneName)
	{
		currentScene = newSceneName;
	}

    // Gets currentScene
	public string GetCurrentLevel()
	{
		return currentScene;
	}
}

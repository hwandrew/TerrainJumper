using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        SceneManager.LoadScene(newLevel + "Anim");
        currentScene = split[0] + '_' + lvlNum.ToString();
		levelLoader = StartCoroutine(LoadLevel(newLevel));
	}

	private IEnumerator LoadLevel(string level)
	{
		// yield return null;
		pauseInstance.enabled = false;
		yield return new WaitForSeconds(5);
		SceneManager.LoadScene(level);
		pauseInstance.enabled = true;
	}

	public void SetCurrentLevel(string newSceneName)
	{
		currentScene = newSceneName;
	}

	public string GetCurrentLevel()
	{
		return currentScene;
	}
}

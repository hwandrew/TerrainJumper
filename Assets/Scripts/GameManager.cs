using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	string currentScene;

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
		SceneManager.LoadScene("Master");
	}
}

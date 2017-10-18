using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour {

	public static PauseMenuManager pauseInstance;
	public GameObject panel;
	public bool paused = false;

	GameManager instance;
	GameObject mainCam;
	GameObject player;

	void Awake()
	{
		if (pauseInstance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			DontDestroyOnLoad(this);
			pauseInstance = this;
		}

		instance = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(this);
		panel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		// press escape to allow cursor to exit the game screen
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			mainCam = GameObject.Find("Main Camera");
			player = GameObject.Find("Player");
			if (paused)
			{
				// unpause
				Cursor.visible = false;
				Cursor.lockState = CursorLockMode.Locked;
				mainCam.GetComponent<MouseLook>().enabled = true;
				player.GetComponent<PlayerController>().EnableInput();
				Time.timeScale = 1.0f;
				paused = false;
				panel.SetActive(false);
			}
			else
			{
				// pause
				Cursor.visible = true;
				Cursor.lockState = CursorLockMode.None;
				mainCam.GetComponent<MouseLook>().enabled = false;
				player.GetComponent<PlayerController>().DisableInput();
				Time.timeScale = 0.0f;
				paused = true;
				panel.SetActive(true);
			}
		}
	}

    // Resets the current level when player clicks the "Reset Level" button in the pause menu
	public void ResetLevel()
	{
		Time.timeScale = 1.0f;
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.None;
		mainCam.GetComponent<MouseLook>().enabled = true;
		player.GetComponent<PlayerController>().EnableInput();
		paused = false;
		panel.SetActive(false);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

    // Resets the game when player clicks the "Reset Game" button in the pause menu
	public void ResetGame()
	{
		Time.timeScale = 1.0f;
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.None;
		mainCam.GetComponent<MouseLook>().enabled = true;
		player.GetComponent<PlayerController>().EnableInput();
		paused = false;
		panel.SetActive(false);
		instance.SetCurrentLevel("Level_1");
		SceneManager.LoadScene("Level1");
	}

    // Quits the application when player clicks the "Quit Game" button in the pause menu
	public void QuitGame()
	{
		Time.timeScale = 1.0f;
		Application.Quit();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public GameObject[] scoreGrids;
	public GameObject scorePrefab;

	public GameObject gamePanel;
	public GameObject menuPanel;
	public GameObject pauseMenuScreen;

	public LevelManager level;

	public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
			Debug.LogError("More than one GameManager instance!");
        }
		Instance = this;
    }

	// Create new instance of GameManager
	void NewInstance()
	{
		if (Instance == null)
		{
			Debug.Log("New instance instantiated");
			Instance = this;
		}
	}

	public void PlayGame()
	{
		menuPanel.SetActive(false);
		gamePanel.SetActive(true);

		level.PlayGame();
	}

	public void MainMenu()
	{
		menuPanel.SetActive(true);
		gamePanel.SetActive(false);

		NewInstance();
	}

	// Pauses the game; activates pause panel
	public void PauseGame()
	{

		Time.timeScale = 0f;

		pauseMenuScreen.SetActive(true);                                    // Activate Pause Panel

		Debug.Log("Game is paused");

	}

	// Resumes the game; deactivates pause panel
	public void ResumeGame()
	{

		pauseMenuScreen.SetActive(false);                                   // Deactivate pause panel

		Time.timeScale = 1f;

		Debug.Log("Game is unpaused");

	}

	public void QuitGame()
    {
		Application.Quit();
		Debug.Log("Application is exiting");
	}

	// Add score indicator to the scoring player
    public void AddScore(int player)
	{
		GameObject point = Instantiate(scorePrefab, transform.position, Quaternion.identity) as GameObject;
		point.transform.SetParent(scoreGrids[player].transform);
	}
}

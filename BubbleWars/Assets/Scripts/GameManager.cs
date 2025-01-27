using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
	public GameObject gamePanel;
	public GameObject menuPanel;
	public GameObject pauseMenuPanel;
	public GameObject winnerPanel;

	public GameObject winnerText;

	public LevelManager level;
	SoundManager sm;

	public static GameManager Instance { get; private set; }

	public bool isGamePaused = false;

    private void Awake()
    {
        if (Instance != null)
        {
			Debug.LogError("More than one GameManager instance!");
        }
		Instance = this;

		sm = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManager>();
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

		level.StopAllCoroutines();
		level.countdown = false;
        isGamePaused = false;
        ResumeGame();

		winnerPanel.SetActive(false);

		NewInstance();
	}

	public void GameWin()
    {
		Debug.Log("check if game over");
		sm.PlaySFX(sm.win);
		winnerPanel.SetActive(true);

        if (level.p1Win)
        {
			winnerText.GetComponent<TMP_Text>().text = "Player 1 WINS!";
		}
        else if(level.p2Win)
        {
			winnerText.GetComponent<TMP_Text>().text = "Player 2 WINS!";
		}

	}

	// Pauses the game; activates pause panel
	public void PauseGame()
	{
        
        Time.timeScale = 0f;

		pauseMenuPanel.SetActive(true); // Activate Pause Panel

		Debug.Log("Game is paused");

	}

	// Resumes the game; deactivates pause panel
	public void ResumeGame()
	{

		pauseMenuPanel.SetActive(false); // Deactivate pause panel

		Time.timeScale = 1f;

		Debug.Log("Game is unpaused");

	}

	public void QuitGame()
    {
		Application.Quit();
		Debug.Log("Application is exiting");
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !menuPanel.activeSelf)
        {
            if (isGamePaused)
            {
				isGamePaused = false;
                ResumeGame();
            }
            else
            {
                isGamePaused = true;
                PauseGame();
            }
        }
    }
}

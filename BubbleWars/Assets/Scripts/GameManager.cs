using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public GameObject[] scoreGrids;
	public GameObject scorePrefab;

	public GameObject gamePanel;
	public GameObject menuPanel;

	public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
			Debug.LogError("More than one GameManager instance!");
        }
		Instance = this;
    }

	public void PlayGame()
	{
		menuPanel.SetActive(false);

		gamePanel.SetActive(true);
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

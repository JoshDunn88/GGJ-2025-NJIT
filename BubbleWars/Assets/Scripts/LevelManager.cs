using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
	WaitForSeconds oneSec; // Coroutine varible
	public Transform[] spawnPositions; // Player spawn points 
	public GameObject[] players;

	public int rounds = 3; // Default rounds to 3.
	int currentRound = 1; // Start current round at 1.

	// Countdown vaiables
	public bool countdown;
	public int roundTimer = 30; // Default time for each round.
	int currentTimer;
	float internalTimer;

	public GameObject announcerText;
	public GameObject timerText;

	/*
	void Start()
    {
		// Initiate the WaitForSeconds
		oneSec = new WaitForSeconds(1);

		StartCoroutine("StartGame");
		announcerText.gameObject.SetActive(false);
	}*/

    private void Update()
    {
        if (countdown)
        {
			HandleRoundTimer(); // Controls the timer
        }
    }

	public void PlayGame()
	{
		// Initiate the WaitForSeconds
		oneSec = new WaitForSeconds(1);

		StartCoroutine("StartGame");
		announcerText.gameObject.SetActive(false);
	}

	void HandleRoundTimer()
    {
		timerText.GetComponent<Text>().text = "Time: " + currentTimer.ToString();

		internalTimer += Time.deltaTime; // every one second based on frames

		if (internalTimer > 1)
        {
			currentTimer--;
			internalTimer = 0;
        }

		if (currentTimer <= 0)
        {
			EndRound(true);
			countdown = false;
        }
    }

	public void EndRound(bool timeOut = false)
    {
		countdown = false;
		timerText.GetComponent<Text>().text = roundTimer.ToString();

		if (timeOut)
        {
			announcerText.gameObject.SetActive(true);
			announcerText.GetComponent<Text>().text = "Time Out!";
			announcerText.GetComponent<Text>().color = Color.cyan;
		}
        else
        {
			announcerText.gameObject.SetActive(true);
			announcerText.GetComponent<Text>().text = "K.O.";
			announcerText.GetComponent<Text>().color = Color.red;
		}

		// Disable controls
		DisableControl();

		// Start routine for end round
		StartCoroutine("EndRound");

    }

    private void DisableControl()
	{
		// Call to pause player controls
	}

    IEnumerator StartGame()
    {
		yield return InitRound();
		yield return EnableControl();
	}

	IEnumerator InitRound()
    {
		announcerText.gameObject.SetActive(false);

		currentTimer = roundTimer;
		countdown = false;

		yield return EnableControl();
	}

	IEnumerator EndRound()
    {
		yield return oneSec;
		yield return oneSec;
		yield return oneSec;

		// Find player that won
		// vPlayer = FindWinner();

		// If time run out; its null
		/*
		 * if (vPlayer == null)
		 * {
		 *		announcerText.gameObject.SetActive(true);
				announcerText.GetComponent<Text>().text = "Draw";
				announcerText.GetComponent<Text>().color = Color.blue;	
		 * }
		 * else
		 * {
		 *		announcerText.gameObject.SetActive(true);
				announcerText.GetComponent<Text>().text = vPlayer.playerId + "Wins!";
				announcerText.GetComponent<Text>().color = Color.red;
		 */

		yield return oneSec;
		yield return oneSec;
		yield return oneSec;

		currentRound++;

		bool matchOver = isMatchOver();

		if (!matchOver)
        {
			StartCoroutine("InitRound");
        }
        else
        {
			// Disable player control
			// Show End game panel
        }
	}

	bool isMatchOver()
    {
		bool retVal = false;

		for (int i = 0; i < 2; i++)
		{
			/*
			if (players[i].score >= rounds)
			{
				retVal = true;
				break;
			}
			*/
		}

		return retVal;
    }

	IEnumerator EnableControl()
    {
		announcerText.gameObject.SetActive(true);
		announcerText.GetComponent<Text>().text = "Round " + currentRound;
		announcerText.GetComponent<Text>().color = Color.white;
		yield return oneSec;
		yield return oneSec;
		yield return new WaitForSeconds(2);

		// Change UI text for each second that passes
		announcerText.GetComponent<Text>().text = "3";
		announcerText.GetComponent<Text>().color = Color.green;
		yield return oneSec;
		announcerText.GetComponent<Text>().text = "2";
		announcerText.GetComponent<Text>().color = Color.yellow;
		yield return oneSec;
		announcerText.GetComponent<Text>().text = "1";
		announcerText.GetComponent<Text>().color = Color.red;
		yield return oneSec;
		announcerText.GetComponent<Text>().text = "FIGHT!";
		announcerText.GetComponent<Text>().color = Color.red;

		/*
		 * ENABLER PLAYER CONTROL HERE
		 * ih.enabled = true;
		 */

		yield return oneSec;
		announcerText.gameObject.SetActive(false);
		countdown = true;
	}

}
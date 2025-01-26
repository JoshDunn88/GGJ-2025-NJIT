using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
	WaitForSeconds oneSec; // Coroutine varible
	public Transform[] spawnPositions; // Player spawn points 
	public GameObject[] players;

	public int rounds = 3; // Default rounds to 3.
	int currentRound = 1; // Start current round at 1.

	// Countdown vaiables
	public bool countdown;
	public int setupTime;
	int currentTimer;
	float internalTimer;

	public GameManager gm;

	public int p1Score, p2Score;
	public bool p1Win, p2Win;

	public GameObject announcerText;
	public GameObject timerText;

	public GameObject[] scoreGrids;
	public GameObject scorePrefab;

	private void Update()
    {
        if (countdown)
        {
			HandleRoundTimer(); // Controls the timer
        }
	}

	public void SetRounds(int roundAmt)
    {
		switch (roundAmt)
		{
			case 1:
				rounds = 1;
				break;
			case 2:
				rounds = 2;
				break;
			case 3:
				rounds = 3;
				break;
			default:
				rounds = 1;
				break;
		}
	}

	public void SetTimer(int time)
    {
		switch (time)
		{
			case 5:
				setupTime = 5;
				break;
			case 10:
				setupTime = 10;
				break;
			case 15:
				setupTime = 15;
				break;
			default:
				setupTime = 5;
				break;
		}
	}

	public void PlayGame()
	{
		// Initiate the WaitForSeconds
		oneSec = new WaitForSeconds(1);

		StartCoroutine(nameof(InitRound));
		//announcerText.gameObject.SetActive(true); was false
	}

	void HandleRoundTimer()
    {
		timerText.GetComponent<TMP_Text>().text = currentTimer.ToString();

		internalTimer += Time.deltaTime; // every one second based on frames

		if (internalTimer > 1)
        {
			currentTimer--;
			internalTimer = 0;
        }

		if (currentTimer <= 0)
        {
			timerText.gameObject.SetActive(false);
			countdown = false;
        }
    }

	public void EndRound()
    {
		announcerText.gameObject.SetActive(true);
		announcerText.GetComponent<TMP_Text>().text = "POPPED!";
		announcerText.GetComponent<TMP_Text>().color = Color.red;

		// Disable controls
		DisableControl();

		// Start routine for end round
		StartCoroutine(nameof(NextRound));

    }

    private void DisableControl()
	{
		// Call to pause player controls
		players[0].GetComponent<PlayerMovement>().canMove = false;
        players[0].GetComponent<PlayerMovement>().canBlow = false;
        players[0].GetComponent<PlayerMovement>().canShoot = false;

        players[1].GetComponent<PlayerMovement>().canMove = false;
        players[1].GetComponent<PlayerMovement>().canBlow = false;
        players[1].GetComponent<PlayerMovement>().canShoot = false;
    }

    IEnumerator StartGame()
    {
		yield return InitRound();
		yield return EnableControl();
	}

	IEnumerator InitRound()
    {
		announcerText.gameObject.SetActive(false);

		currentTimer = setupTime;
		countdown = false;

		timerText.GetComponent<TMP_Text>().text = currentTimer.ToString();

		yield return EnableControl();
	}

	IEnumerator NextRound()
    {
		yield return oneSec;
		yield return oneSec;
		yield return oneSec;

		bool matchOver = isMatchOver();

		if (!matchOver)
        {
			StartCoroutine("InitRound");
        }
        else
        {
			DisableControl();
			gm.GameWin();
		}

		currentRound++;
	}

	// Add score indicator to the scoring player
	public void AddScore(int playerID)
	{
		GameObject point = Instantiate(scorePrefab, transform.position, Quaternion.identity) as GameObject;
		point.transform.SetParent(scoreGrids[playerID].transform);

		if (playerID == 0)
        {
			p1Score++;
        }
        else
        {
			p2Score++;
        }

	}

	bool isMatchOver()
    {
		bool retVal = false;

		if (p1Score >= rounds || p2Score >= rounds)
		{
			Debug.Log("check if rounds reached");

			if (p1Score >= rounds)
			{
				Debug.Log("check if rounds reached for p1");
				p1Win = true;
			}
			else if (p2Score >= rounds)
			{
				Debug.Log("check if rounds reached for p2");
				p2Win = true;
			}

			int totalScore = p1Score + p2Score;

			ResetScore(totalScore);

			p1Score = 0;
			p2Score = 0;

			retVal = true;
		}

		return retVal;
    }

	void ResetScore(int totalScore)
    {
		Debug.Log("Total score to remove: " + totalScore);
		GameObject[] point = GameObject.FindGameObjectsWithTag("Score");

		for (int i = 0; i < totalScore; i++)
        {
			Destroy(point[i], 1.0f);
		}
	}

	IEnumerator EnableControl()
    {
		
		announcerText.gameObject.SetActive(true);
		timerText.gameObject.SetActive(true);

		announcerText.GetComponent<TMP_Text>().text = "Round " + currentRound;
		announcerText.GetComponent<TMP_Text>().color = Color.white;
		yield return oneSec;
		announcerText.GetComponent<TMP_Text>().text = "Get Ready!";
		announcerText.GetComponent<TMP_Text>().color = Color.cyan;
		yield return oneSec;
		announcerText.GetComponent<TMP_Text>().text = "Blow your DEFENSE!";
		announcerText.GetComponent<TMP_Text>().color = Color.blue;

        //Resurrect players
        players[0].SetActive(true);
        players[1].SetActive(true);

        // Allow players to blow bubbles for defense
        players[0].GetComponent<PlayerMovement>().canBlow = true;
		players[0].GetComponent<PlayerMovement>().canMove = true;

		players[1].GetComponent<PlayerMovement>().canBlow = true;
		players[1].GetComponent<PlayerMovement>().canMove = true;

		// Start setup time
		Debug.Log("Countdown initiated");
		countdown = true;
		yield return new WaitForSeconds(setupTime);
		yield return oneSec;

		// Change UI text for each second that passes
		announcerText.GetComponent<TMP_Text>().text = "3";
		announcerText.GetComponent<TMP_Text>().color = Color.green;
		yield return oneSec;
		announcerText.GetComponent<TMP_Text>().text = "2";
		announcerText.GetComponent<TMP_Text>().color = Color.yellow;
		yield return oneSec;
		announcerText.GetComponent<TMP_Text>().text = "1";
		announcerText.GetComponent<TMP_Text>().color = Color.red;
		yield return oneSec;
		announcerText.GetComponent<TMP_Text>().text = "SLING!";
		announcerText.GetComponent<TMP_Text>().color = Color.red;

        players[0].GetComponent<PlayerMovement>().canShoot = true;

        players[1].GetComponent<PlayerMovement>().canShoot = true;

        yield return oneSec;
		announcerText.gameObject.SetActive(false);
		countdown = false; // Reset timer status
	}

}

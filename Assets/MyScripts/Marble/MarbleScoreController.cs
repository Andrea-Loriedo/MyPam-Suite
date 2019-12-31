using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MarbleScoreController : MonoBehaviour {

	[SerializeField] TextMeshProUGUI scoreText;
	int score;

	// Use this for initialization
	void Awake()
	{
		score = MyPamSessionManager.Instance.player.score;
		// Set the text
		scoreText.text = "Score: " + score;
	}

	// Update is called once per frame
	void Update () 
	{
		// Needs optimising
		score = MyPamSessionManager.Instance.player.score;
		scoreText.text = "Score: " + score;
	}
}

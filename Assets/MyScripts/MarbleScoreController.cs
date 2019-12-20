using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MarbleScoreController : MonoBehaviour {

	[SerializeField] TilemapGenerator game;
	[SerializeField] TextMeshProUGUI scoreText;
	int score;

	// Use this for initialization
	void Awake()
	{
		score = game.GetScore();
		// Set the text
		scoreText.text = "Score: " + score;
	}

	// Update is called once per frame
	void Update () {
		{
			// Needs optimising
			score = game.GetScore();
			scoreText.text = "Score: " + score;
		}
	}
}

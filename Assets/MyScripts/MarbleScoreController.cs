﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MarbleScoreController : MonoBehaviour {

	[SerializeField] TilemapGenerator collisionDetection;
	[SerializeField] TextMeshProUGUI scoreText;
	int score;

	// Use this for initialization
	void Awake()
	{
		score = 0;
		// Set the text
		scoreText.text = "Score: " + score;
	}

	// Update is called once per frame
	void Update () {
		{
			if(collisionDetection.CheckFall() == true)
			{
				score++;
				scoreText.text = "Score: " + score;
				Debug.Log("Score = " + score);
			}
		}
	}
}

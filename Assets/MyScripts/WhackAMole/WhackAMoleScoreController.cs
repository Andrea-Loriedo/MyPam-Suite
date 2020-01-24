using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WhackAMoleScoreController : MonoBehaviour {

	[SerializeField] TextMeshProUGUI scoreText;
	int score;

	void Update () 
	{
		// Needs optimising
		score = MyPamSessionManager.Instance.player.score;
		scoreText.text = "Score: " + score;
	}
}
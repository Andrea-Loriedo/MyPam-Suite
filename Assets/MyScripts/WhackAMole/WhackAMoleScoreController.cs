using UnityEngine;
using TMPro;
using UXF;

public class WhackAMoleScoreController : MonoBehaviour {

	[SerializeField] TextMeshProUGUI scoreText;
	int score;

	public Session session;

	void Update () 
	{
		// Needs optimising
		score = MyPamSessionManager.Instance.player.score;
		scoreText.text = "Score: " + score;
	}

	public void RecordScore()
	{
		if (session.InTrial) session.CurrentTrial.result["total_game_score"] = score;
	}
}
using UnityEngine;

public class Player : IPlayer
{
    public int marbleScore { get; set; }
    public IPlayerInput PlayerInput { get; set; }
    public Vector3 initialPosition { get; set; }

    public Player()
	{
        // if(PlayerInput == null)
        //     PlayerInput = new PlayerInput();
        marbleScore = 0;
        initialPosition = new Vector3(7.78f, 2.141f, 8f);
		// Create a new player input
	}

    public void UpdateScore()
    {
        marbleScore++;
    }
}
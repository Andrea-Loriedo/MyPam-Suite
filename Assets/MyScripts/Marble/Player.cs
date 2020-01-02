using UnityEngine;

public class Player : MonoBehaviour
{
    public int score { get; set; }
    public IPlayerInput PlayerInput { get; set; }
    
    void Awake()
    {
        score = 0;
        InitInput();
    }

    void InitInput()
	{
		// Create a new player input
		if (PlayerInput == null)
			PlayerInput = new PlayerInput();
	}
}

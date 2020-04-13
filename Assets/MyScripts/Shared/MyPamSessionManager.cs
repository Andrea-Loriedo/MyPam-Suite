using UnityEngine;

public class MyPamSessionManager: MonoBehaviour
{
    [HideInInspector] public Player player;
    
    public static MyPamSessionManager Instance { get; private set; } // static singleton
    
    void Awake() 
    {
        if (Instance == null) { Instance = this;  }
        else { Destroy(gameObject); }
        // Cache reference to player
        player = FindObjectOfType<Player>();
    }
}

using UnityEngine;

public class Player : MonoBehaviour
{
    public int score { get; set; }
    
    void Awake()
    {
        score = 0;
    }
}

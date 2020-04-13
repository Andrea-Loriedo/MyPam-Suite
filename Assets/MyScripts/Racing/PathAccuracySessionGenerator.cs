using System.Collections.Generic;
using UnityEngine;
using UXF;

public class PathAccuracySessionGenerator : MonoBehaviour
{
    [SerializeField] CarGameManager gameManager;

    // assign this method to the Session OnSessionBegin UnityEvent in its inspector
    public void Generate(Session session) 
    {       
        // Load tracks from JSON
        Dictionary<string, object> tracks = session.settings.GetDict("tracks"); 
        
        // Create as many trials as there are tracks in the settings file
        int numTrials = tracks.Count;

        // Creating a block of 10 trials
        var Block1 = session.CreateBlock(numTrials);

        // Start the game with the imported car tracks
        gameManager.StartSession(tracks);
    }
}
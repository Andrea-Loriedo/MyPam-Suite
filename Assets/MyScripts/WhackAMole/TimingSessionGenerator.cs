using UnityEngine;
using UXF;

public class TimingSessionGenerator : MonoBehaviour
{
    [SerializeField] TimingResults results;
    // assign this method to the Session OnSessionBegin UnityEvent in its inspector
    public void Generate(Session session) 
    {       
        int numTrials = session.settings.GetInt("number_of_trials");

        // Creating a block of 10 trials
        var Block1 = session.CreateBlock(numTrials);
    }
}
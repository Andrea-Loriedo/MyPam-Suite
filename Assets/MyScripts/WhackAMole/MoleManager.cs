using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoleManager : MonoBehaviour
{
    [Tooltip("Mole prefabs to be randomly instanced")]
    List<MoleController> moles = new List<MoleController>();	
    [SerializeField] public HolePositioner holePositioner;
    List<int> previousMoles;
    int moleIndex;
    bool spawn;

    void Start()
    {
        spawn = false;

        foreach (Transform child in transform)
            moles.Add(child.GetComponent<MoleController>());
    }

    public void StartSpawn()
	{
        // StartCoroutine ("SpawnRandom");
	}

    public void StopSpawn()
	{
		spawn = false;
	}

    void SpawnRandom()
	{
        Vector3 randomHole = holePositioner.holes[holePositioner.Shuffle()];
        MoleController randomMole = moles[Shuffle(moles.Count)];
        // // Spawn a random mole
        randomMole.gameObject.transform.position = randomMole;
        randomMole.Up();				
            // yield return new WaitForSeconds (waitTime);
	}

    public int Shuffle(int molesCount)
    {
        int lastMole = UnityEngine.Random.Range(0, molesCount);
        moleIndex = UnityEngine.Random.Range(0, molesCount);

        // Always pick a map different from all the previously used ones
        while (previousMoles.Contains(moleIndex) || moleIndex == lastMole)
            moleIndex = UnityEngine.Random.Range(0, molesCount);
        
        previousMoles.Add(moleIndex);

        if(previousMoles.Count == molesCount)
        {
            lastMole = previousMoles[previousMoles.Count-1];
            previousMoles.Clear();
        }

        // Logger.Debug("Generated map number " + mapNumber);
        return moleIndex;
    }
}
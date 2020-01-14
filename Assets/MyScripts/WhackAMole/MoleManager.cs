using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleManager : MonoBehaviour
{
    [Tooltip("Mole prefabs to be randomly instanced")]
    MoleController[] moles;	
    [HideInInspector] public float waitTime = 1.0f;
    List<int> previousMoles;
    int moleIndex;
    bool spawn;

    void Start()
    {
        molePrefabs = new GameObject[10];
        spawn = false;

        foreach (Transform child in transform)
            moles.Add(child.GetComponent<MoleController>());
    }

    public void StartSpawn()
	{
        StartCoroutine (SpawnRandom());
	}

    public void StopSpawn()
	{
		spawn = false;
	}

    void SpawnRandom()
	{
		spawn = true;
        
        // Spawn a random mole
        moles[Shuffle(moles.Length)].Up();				
            yield return new WaitForSeconds (waitTime);
		}
	}

    public string Shuffle(int molesCount)
    {
        int lastMole = UnityEngine.Random.Range(0, molesCount);
        moleIndex = UnityEngine.Random.Range(0, molesCount);

        // Always pick a map different from all the previously used ones
        while (previousMoles.Contains(moleIndex) || moleIndex == lastMole)
        {
            moleIndex = UnityEngine.Random.Range(0, molesCount);
        }
        
        if(previousMoles.Count == molesCount)
        {
            lastMole = previousMoles[previousMoles.Count-1];
            previousMoles.Clear();
        }

        // Logger.Debug("Generated map number " + mapNumber);
        return moleIndex.ToString();
    }
}
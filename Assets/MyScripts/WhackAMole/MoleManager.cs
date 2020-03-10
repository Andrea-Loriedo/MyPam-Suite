using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoleManager : MonoBehaviour
{
    [Tooltip("Mole prefabs to be randomly instantiated")]
    public GameObject[] moles;
    public HolePositioner holePositioner;

    List<int> previousMoles = new List<int>();
    int moleIndex;

    public void SpawnRandom()
	{
        StartCoroutine(Spawn());
        Logger.Debug("Spawn");
	}

    public void StopSpawning()
    {
        StopAllCoroutines();
        Logger.Debug("Stop spawning");
    }

    IEnumerator Spawn()
    {
        GameObject randomMole = (GameObject)Instantiate(moles[Shuffle(moles.Length)]); // Instantiate a random mole
        Vector3 randomHole = holePositioner.holes[holePositioner.Shuffle()]; 
        randomMole.transform.parent = transform; // Gather under common transform
        randomMole.transform.position = randomHole; // Place in a random hole
        MoleController mole = randomMole.GetComponent<MoleController>();
        
        if (mole != null && mole.state == MoleState.BELOW_GROUND)
            mole.SetState(MoleState.UP);
        
        yield return null;
    }

    public int Shuffle(int molesCount)
    {
        int lastMole = UnityEngine.Random.Range(0, molesCount);
        moleIndex = UnityEngine.Random.Range(0, molesCount);

        // // Always pick a map different from all the previously used ones
        // while (previousMoles.Contains(moleIndex) || moleIndex == lastMole)
        //     moleIndex = UnityEngine.Random.Range(0, molesCount);
        
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


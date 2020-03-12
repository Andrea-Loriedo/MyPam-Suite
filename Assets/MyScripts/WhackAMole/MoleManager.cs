using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoleManager : MonoBehaviour
{
    [Tooltip("Mole prefabs to be randomly instantiated")]
    public GameObject[] moles;
    public HolePositioner holePositioner;
    [SerializeField] StartZoneController startZone;

    List<int> previousMoles = new List<int>();
    int moleIndex;
    float minDelay = 1.5f;
    float maxDelay = 3f;

    public void StartSpawnSequence()
	{
        float randomizedDelay = Random.Range(minDelay, maxDelay);
        StartCoroutine(DelayedSpawn(randomizedDelay));
	}

    public void StopSpawning()
    {
        StopAllCoroutines();
    }

    IEnumerator DelayedSpawn(float delay)
    {        
        yield return new WaitForSeconds(delay);

        GameObject randomMole = (GameObject)Instantiate(moles[Shuffle(moles.Length)]); // Instantiate a random mole
        MoleController mole = randomMole.GetComponent<MoleController>();
        Vector3 randomHole = holePositioner.holes[holePositioner.Shuffle()]; 

        randomMole.transform.parent = transform; // Gather under common transform
        randomMole.transform.position = randomHole; // Place in a random hole
        
        if (mole != null && mole.state == MoleState.BELOW_GROUND)
        {
            mole.SetState(MoleState.UP); // Bring mole to the surface
            startZone.SetState(StartZoneState.GO); // Notify start zone
        }
    }

    public int Shuffle(int molesCount)
    {
        int lastMole = UnityEngine.Random.Range(0, molesCount);
        moleIndex = UnityEngine.Random.Range(0, molesCount);

        // // Always pick a map different from all the previously used ones (infinite loop bug needs fixing)
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


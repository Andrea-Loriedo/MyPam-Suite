using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoleManager : MonoBehaviour
{
    [Tooltip("Mole prefabs to be randomly instantiated")]
    public GameObject[] moles;
    public HolePositioner holePositioner;
    [HideInInspector] public bool spawn;
    [SerializeField] StartZoneController startZone;

    List<int> previousMoles = new List<int>();
    int moleIndex;

    public void SpawnRandom()
	{
        StartCoroutine(Spawn());
	}

    public void StopSpawning()
    {
        StopAllCoroutines();
    }

    IEnumerator Spawn()
    {
        GameObject randomMole = (GameObject)Instantiate(moles[Shuffle(moles.Length)]); // Instantiate a random mole
        Vector3 randomHole = holePositioner.holes[holePositioner.Shuffle()]; 
        randomMole.transform.parent = transform; // Gather under common transform
        randomMole.transform.position = randomHole; // Place in a random hole
        MoleController controller = randomMole.GetComponent<MoleController>();
        
        if (controller != null)
            controller.Up();
        
        yield return null;
        
        startZone.SetState(StartZoneState.WAITING);
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


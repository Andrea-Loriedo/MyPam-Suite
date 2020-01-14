using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoleManager : MonoBehaviour
{
    [Tooltip("Mole prefabs to be randomly instanced")]
    [SerializeField] public GameObject[] moles;
    [SerializeField] public HolePositioner holePositioner;
    List<int> previousMoles = new List<int>();
    int moleIndex;
    float spawnFrequency = 4f;
    bool spawn;

    void Start()
    {
        spawn = true;
        StartCoroutine(SpawnRandom(spawnFrequency));
    }

    void Update()
    {

    }

    IEnumerator SpawnRandom(float frequency)
	{
        while(spawn)
        {
            // Instantiate a random mole
            GameObject randomMole = (GameObject)Instantiate(moles[Shuffle(moles.Length)]);
            Vector3 randomHole = holePositioner.holes[holePositioner.Shuffle()];
            randomMole.transform.parent = transform;
            // Place in a random hole
            randomMole.transform.position = randomHole;
            MoleController controller = randomMole.GetComponent<MoleController>();
            if (controller != null)
                controller.Up();
            yield return new WaitForSeconds (frequency);
        }

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
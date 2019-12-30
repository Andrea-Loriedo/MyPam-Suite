using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;

public class MarbleTests
{
    // [UnityTest]
    // public IEnumerator GameIsInitialisedCorrectly() {
    //     SetupScene();
    //     yield return new WaitForSeconds(1);
    // }

    // [UnityTest]
    // public IEnumerator NewMazeIsGeneratedOnHoleCollision()
    // {
    //     SetupScene();
    //     Transform hole = GameObject.Find("Hole(Clone)").transform;
    //     Transform marble = GameObject.Find("Marble").transform;

    //     marble.transform.position = new Vector3(hole.position.x, hole.position.y, hole.position.z - 0.15f);

    //     yield return new WaitForSeconds(0.1f);
        
    //     if(hole.GetComponent<TilemapGenerator>().LevelComplete() == true)
    //     {
    //         yield break;
    //     }
    // }

    // void SetupScene()
    // {
    //     SceneManager.LoadScene("Marble");   
    // }
}

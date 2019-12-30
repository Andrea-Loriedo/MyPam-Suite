using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System;
using System.IO;

namespace Tests
{
    public class HoleCollisionDetection
    {

        [UnityTest]     
        public IEnumerator GameIsInitialisedCorrectly() {
            SetupScene();
            yield return new WaitForSeconds(1);
        }

        [UnityTest] 
        public IEnumerator InstantitesHoleFromPrefab()
        {
            SetupScene();

            LogAssert.ignoreFailingMessages = true;

            GameObject hole = Resources.Load("Tests/Hole") as GameObject;
            var tilemapGenerator = new GameObject().AddComponent<TilemapGenerator>();
            tilemapGenerator.Construct(hole);

            yield return null;

            var spawnedHole = tilemapGenerator.transform.Find("Hole(Clone)");
            var prefabOfTheSpawnedHole = PrefabUtility.GetCorrespondingObjectFromSource(spawnedHole);

            Assert.AreEqual(spawnedHole, prefabOfTheSpawnedHole);
        }
        
        [UnityTest]
        public IEnumerator MarbleFallTriggersHole()
        {
            SetupScene();

            LogAssert.ignoreFailingMessages = true;

            bool fall = false;
            GameObject hole = Resources.Load("Tests/Hole") as GameObject;
            var collision = new GameObject().AddComponent<HoleCollisionCheck>();
            var marble = new GameObject().AddComponent<MarbleController>();
            var collider = hole.GetComponent<BoxCollider>();

            collision.Construct(fall);

            hole.transform.position = Vector3.zero;
            marble.transform.position = collider.center;
            
            yield return new WaitForFixedUpdate(); 

            Assert.IsFalse(fall);
        }

        [TearDown]
        public void AfterEveryTest()
        {
            foreach (var gameObject in GameObject.FindGameObjectsWithTag("Hole"))
            {
                GameObject.Destroy(gameObject);
            }
            foreach (var gameObject in GameObject.FindObjectsOfType<TilemapGenerator>())
            {
                GameObject.Destroy(gameObject);
            }
        }

        void SetupScene()
        {
            SceneManager.LoadScene("Marble");   
        }
    }
}

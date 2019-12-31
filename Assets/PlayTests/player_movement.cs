using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using NSubstitute;
using UnityEngine.SceneManagement;

public class player_movement {

    // [UnityTest]
    // public IEnumerator with_positive_vertical_input_moves_forward() 
    // {
    //     SetupScene(); 

    //     yield return new WaitForSeconds(1);

    //     GameObject player = new GameObject("Player");
    //     Rigidbody rb = player.AddComponent<Rigidbody>();
    //     TilemapGenerator map = player.AddComponent<TilemapGenerator>();
    //     MarbleController marble = player.AddComponent<MarbleController>();
    //     marble.PlayerInput = Substitute.For<IPlayerInput>();
    //     marble.PlayerInput.Input.y.Returns(1f);

    //     player.transform.position = Vector3.zero;

    //     yield return new WaitForSeconds(.3f);

    //     Assert.IsTrue(player.transform.position.y > 0f);
    //     Assert.AreEqual(0, player.transform.position.x);
    //     Assert.AreEqual(0, player.transform.position.z);
    // }

    // void SetupScene()
    // {
    //     SceneManager.LoadScene("Marble");   
    // }
}

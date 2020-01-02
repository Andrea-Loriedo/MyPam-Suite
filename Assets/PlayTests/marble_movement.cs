using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using NSubstitute;
using UnityEngine.SceneManagement;

public class marble_movement 
{
	[UnityTest]
	public IEnumerator with_positive_vertical_input_moves_forward_wrt_isocam()
	{
		// ARRANGE
		SetupScene();
		GameObject playerGameObject = new GameObject("Player");
		Player player = playerGameObject.AddComponent<Player>();
		player.PlayerInput = Substitute.For<IPlayerInput>();
		MyPamSessionManager session = playerGameObject.AddComponent<MyPamSessionManager>();
		MarbleController marble = playerGameObject.AddComponent<MarbleController>();
		marble.rb.useGravity = false;

		// ACT
		MyPamSessionManager.Instance.player.PlayerInput.Input.Returns(new Vector2(1,0));
		var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		sphere.transform.SetParent(player.transform);
		sphere.transform.localPosition = Vector3.zero;

		yield return new WaitForSeconds(1f);

		// ASSERT
        Assert.IsTrue(playerGameObject.transform.position.x > 0f);
		Assert.IsTrue(playerGameObject.transform.position.z > 0f);
        Assert.AreEqual(0, player.transform.position.y);
	}

	void SetupScene()
    {
		var cam = new GameObject("Cam").AddComponent<Camera>();
		cam.tag = "MainCamera";
    }
}
